using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "newAudioConfiguration", menuName = "Audio Configuration")]
public class AudioConfigurationSO : ScriptableObject
{
  public AudioMixerGroup outputAudioMixerGroup;
  public bool mute = false;
  [Range(0f, 1f)] public float volume = 1f;
  [Range(-3f, 3f)] public float pitch = 1f;
  [Range(-1f, 1f)] public float panStereo = 0f;
  [Range(0f, 1.1f)] public float reverbZoneMix = 1f;
  [Range(0f, 1f)] public float spatialBlend = 1f;
  public AudioRolloffMode rolloffMode = AudioRolloffMode.Logarithmic;
  [Range(0.01f, 5f)] public float minDistance = 0.1f;
  [Range(5f, 100f)] public float maxDistance = 50f;
  [Range(0, 360)] public int spread = 0;
  [Range(0f, 5f)] public float dopplerLevel = 1f;
  public bool bypassEffects = false;
  public bool bypassListenerEffects = false;
  public bool bypassReverbZones = false;
  public bool ignoreListenerVolume = false;
  public bool ignoreListenerPause = false;
  [SerializeField] private PriorityLevel _priorityLevel = PriorityLevel.Standard;
  private enum PriorityLevel
  {
    Highest = 0,
    High = 64,
    Standard = 128,
    Low = 194,
    VeryLow = 256
  }

  public void ApplyTo(AudioSource audioSource)
  {
    audioSource.outputAudioMixerGroup = this.outputAudioMixerGroup;
    audioSource.mute = this.mute;
    audioSource.bypassEffects = this.bypassEffects;
    audioSource.bypassListenerEffects = this.bypassListenerEffects;
    audioSource.bypassReverbZones = this.bypassReverbZones;
    audioSource.priority = (int)_priorityLevel;
    audioSource.volume = this.volume;
    audioSource.pitch = this.pitch;
    audioSource.panStereo = this.panStereo;
    audioSource.spatialBlend = this.spatialBlend;
    audioSource.reverbZoneMix = this.reverbZoneMix;
    audioSource.dopplerLevel = this.dopplerLevel;
    audioSource.spread = this.spread;
    audioSource.rolloffMode = this.rolloffMode;
    audioSource.minDistance = this.minDistance;
    audioSource.maxDistance = this.maxDistance;
    audioSource.ignoreListenerVolume = this.ignoreListenerVolume;
    audioSource.ignoreListenerPause = this.ignoreListenerPause;
  }
}
