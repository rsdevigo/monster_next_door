using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCue : MonoBehaviour
{

  [SerializeField] private bool _playOnStart = false;
  [SerializeField] private AudioCueSO _audioCue;
  [SerializeField] private AudioConfigurationSO _audioConfiguration;
  [SerializeField] private AudioCueEventChannelSO _audioCueEventChannel;
  private AudioCueKey controlKey = AudioCueKey.invalid;
  void Start()
  {
    if (_playOnStart)
    {
      StartCoroutine(WaitToPlay());
    }
  }

  private void OnDisable()
  {
    _playOnStart = false;
    StopAudioCue();
  }

  IEnumerator WaitToPlay()
  {
    yield return new WaitForSeconds(1f);
    if (_playOnStart)
      PlayAudioCue();
  }

  public void PlayAudioCue()
  {
    controlKey = _audioCueEventChannel.RaisePlayEvent(_audioCue, _audioConfiguration, transform.position);
  }

  public void StopAudioCue()
  {
    if (controlKey != AudioCueKey.invalid)
    {
      if (!_audioCueEventChannel.RaiseStopEvent(controlKey))
      {
        controlKey = AudioCueKey.invalid;
      }
    }
  }

  public void FinishAudioCue()
  {
    if (controlKey != AudioCueKey.invalid)
    {
      if (!_audioCueEventChannel.RaiseFinishEvent(controlKey))
      {
        controlKey = AudioCueKey.invalid;
      }
    }
  }
}
