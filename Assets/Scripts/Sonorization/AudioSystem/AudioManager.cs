using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
  [SerializeField] [Range(0f, 1f)] private float _sfxVolume = 1f;
  [SerializeField] [Range(0f, 1f)] private float _musicVolume = 1f;
  [SerializeField] [Range(0f, 1f)] private float _masterVolume = 1f;
  [SerializeField] private FloatEventChannelSO _sfxVolumeChannel;
  [SerializeField] private FloatEventChannelSO _musicVolumeChannel;
  [SerializeField] private FloatEventChannelSO _masterVolumeChannel;

  [SerializeField] private AudioMixer _audioMixer;

  private SoundEmitterCollection _soundEmitterCollection;
  [SerializeField] private SoundEmitterPool _soundEmitterPool;
  private SoundEmitter _musicSoundEmitter;

  [SerializeField] private AudioCueEventChannelSO _sfxAudioChannel;
  [SerializeField] private AudioCueEventChannelSO _musicAudioChannel;

  void Start()
  {
    _soundEmitterCollection = new SoundEmitterCollection();
  }

  public AudioCueKey PlayAudioCue(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 position)
  {
    AudioClip[] audioClips = audioCue.GetClips();
    SoundEmitter[] soundEmitters = new SoundEmitter[audioClips.Length];
    AudioCueKey key = _soundEmitterCollection.GetKey(audioCue);

    for (int i = 0; i < soundEmitters.Length; i++)
    {
      soundEmitters[i] = _soundEmitterPool.GetPrefabInstance();
      if (soundEmitters[i] != null)
      {
        soundEmitters[i].key = key;
        soundEmitters[i].PlayAudioClip(audioClips[i], audioConfiguration, audioCue.looping, position);
        if (!audioCue.looping)
        {
          soundEmitters[i].OnSoundFinishedPlaying += StopAndCleanEmitter;
        }
      }
    }

    _soundEmitterCollection.Add(key, soundEmitters);
    return key;
  }

  void OnValidate()
  {
    if (Application.isPlaying)
    {
      SetGroupVolume("MasterVolume", _masterVolume);
      SetGroupVolume("SFXVolume", _sfxVolume);
      SetGroupVolume("MusicVolume", _musicVolume);
    }
  }

  public bool StopAudioCue(AudioCueKey key)
  {
    bool found = _soundEmitterCollection.Get(key, out SoundEmitter[] emitters);
    if (found)
    {
      for (int i = 0; i < emitters.Length; i++)
      {
        StopAndCleanEmitter(emitters[i], key);
      }
    }

    return found;
  }

  public bool FinishAudioCue(AudioCueKey key)
  {
    bool found = _soundEmitterCollection.Get(key, out SoundEmitter[] emitters);
    if (found)
    {
      for (int i = 0; i < emitters.Length; i++)
      {
        emitters[i].Finish();
        emitters[i].OnSoundFinishedPlaying += StopAndCleanEmitter;
      }
    }

    return found;
  }

  public void StopAndCleanEmitter(SoundEmitter emitter, AudioCueKey key)
  {
    if (!emitter.IsLooping())
    {
      emitter.OnSoundFinishedPlaying -= StopAndCleanEmitter;
    }

    emitter.Stop();
    emitter.ReturnToPool();

    bool found = _soundEmitterCollection.Get(key, out SoundEmitter[] emitters);

    if (found)
    {
      bool allEmittersIsFinished = true;
      for (int i = 0; i < emitters.Length; i++)
      {
        if (emitters[i].IsPlaying())
        {
          allEmittersIsFinished = false;
        }
      }

      if (allEmittersIsFinished)
      {
        _soundEmitterCollection.Remove(key);
      }
    }
  }

  public AudioCueKey PlayMusicTrack(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 position)
  {
    if (_musicSoundEmitter != null && _musicSoundEmitter.IsPlaying())
    {
      AudioClip songToPlay = audioCue.GetClips()[0];
      if (songToPlay == _musicSoundEmitter.GetClip())
      {
        return AudioCueKey.invalid;
      }
      else
      {
        _musicSoundEmitter.Stop();
        _musicSoundEmitter.ReturnToPool();
      }
    }

    _musicSoundEmitter = _soundEmitterPool.GetPrefabInstance();
    _musicSoundEmitter.PlayAudioClip(audioCue.GetClips()[0], audioConfiguration, true, position);
    _musicSoundEmitter.key = AudioCueKey.invalid;
    return AudioCueKey.invalid;
  }

  public bool StopMusicTrack(AudioCueKey key)
  {
    if (_musicSoundEmitter != null && _musicSoundEmitter.IsPlaying())
    {
      _musicSoundEmitter.Stop();
      _musicSoundEmitter.ReturnToPool();
      _musicSoundEmitter = null;
      return true;
    }
    else
    {
      return false;
    }
  }

  void OnEnable()
  {
    _sfxAudioChannel.OnAudioCuePlayRequested += PlayAudioCue;
    _sfxAudioChannel.OnAudioCueStopRequested += StopAudioCue;
    _sfxAudioChannel.OnAudioCueFinishRequested += FinishAudioCue;

    _musicAudioChannel.OnAudioCuePlayRequested += PlayMusicTrack;
    _musicAudioChannel.OnAudioCueStopRequested += StopMusicTrack;

    _sfxVolumeChannel.OnChangeFloat += ChangeSFXVolume;
    _musicVolumeChannel.OnChangeFloat += ChangeMusicVolume;
    _masterVolumeChannel.OnChangeFloat += ChangeMasterVolume;
  }

  void OnDestroy()
  {
    _sfxAudioChannel.OnAudioCuePlayRequested -= PlayAudioCue;
    _sfxAudioChannel.OnAudioCueStopRequested -= StopAudioCue;
    _sfxAudioChannel.OnAudioCueFinishRequested -= FinishAudioCue;

    _musicAudioChannel.OnAudioCuePlayRequested -= PlayMusicTrack;
    _musicAudioChannel.OnAudioCueStopRequested -= StopMusicTrack;

    _sfxVolumeChannel.OnChangeFloat -= ChangeSFXVolume;
    _musicVolumeChannel.OnChangeFloat -= ChangeMusicVolume;
    _masterVolumeChannel.OnChangeFloat -= ChangeMasterVolume;
  }

  public void ChangeMasterVolume(float newVolume)
  {
    _masterVolume = newVolume;
    SetGroupVolume("MasterVolume", _masterVolume);
  }

  public void ChangeMusicVolume(float newVolume)
  {
    _musicVolume = newVolume;
    SetGroupVolume("MusicVolume", _musicVolume);
  }

  public void ChangeSFXVolume(float newVolume)
  {
    _sfxVolume = newVolume;
    SetGroupVolume("SFXVolume", _sfxVolume);
  }

  public void SetGroupVolume(string parameterName, float normalizedVolume)
  {
    bool volumeSet = _audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVolume));
    if (!volumeSet)
    {
      Debug.LogError("O parametro não foi encontrado");
    }
  }

  private float NormalizedToMixerValue(float normalizedValue)
  {
    return (normalizedValue - 1) * 80f;
  }

  private float MixerValueToNormalized(float mixerValue)
  {
    return 1f + (mixerValue / 80f);
  }

  public float GetGroupVolume(string parameterName)
  {
    if (_audioMixer.GetFloat(parameterName, out float rawVolume))
    {
      return MixerValueToNormalized(rawVolume);
    }
    else
    {
      Debug.LogError("O parametro não foi encontrado");
      return 0f;
    }
  }
}
