using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : Patterns.GenericPoolableObject
{
  private AudioSource _audioSource;
  public AudioCueKey key;
  public UnityAction<SoundEmitter, AudioCueKey> OnSoundFinishedPlaying;

  void Awake()
  {
    _audioSource = GetComponent<AudioSource>();
    _audioSource.playOnAwake = false;
  }


  public void PlayAudioClip(AudioClip audioClip, AudioConfigurationSO audioConfiguration, bool hasLoop, Vector3 position)
  {
    _audioSource.clip = audioClip;
    audioConfiguration.ApplyTo(_audioSource);
    _audioSource.transform.position = position;
    transform.position = position;
    _audioSource.loop = hasLoop;
    _audioSource.time = 0f;
    _audioSource.Play();

    if (!hasLoop)
    {
      StartCoroutine(FinishedPlaying(audioClip.length));
    }
  }

  public void Resume()
  {
    _audioSource.Play();
  }

  public void Pause()
  {
    _audioSource.Pause();
  }

  public void Stop()
  {
    _audioSource.Stop();
  }

  public void Finish()
  {
    if (_audioSource.loop)
    {
      _audioSource.loop = false;
      float timeRemaining = _audioSource.clip.length - _audioSource.time;
      StartCoroutine(FinishedPlaying(timeRemaining));
    }
  }

  IEnumerator FinishedPlaying(float clipLength)
  {
    yield return new WaitForSeconds(clipLength);
    NotifyBeingDone();
  }

  public void NotifyBeingDone()
  {
    OnSoundFinishedPlaying(this, key);
  }

  public bool IsPlaying()
  {
    return _audioSource.isPlaying;
  }

  public bool IsLooping()
  {
    return _audioSource.loop;
  }

  public AudioClip GetClip()
  {
    return _audioSource.clip;
  }
}
