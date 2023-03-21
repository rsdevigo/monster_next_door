using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newAudioCueEventChannel", menuName = "Audio Event Channel")]
public class AudioCueEventChannelSO : ScriptableObject
{
  public AudioCuePlayAction OnAudioCuePlayRequested;
  public AudioCueStopAction OnAudioCueStopRequested;
  public AudioCueFinishAction OnAudioCueFinishRequested;
  public AudioCueKey RaisePlayEvent(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 position = default)
  {
    if (OnAudioCuePlayRequested != null)
    {
      return OnAudioCuePlayRequested.Invoke(audioCue, audioConfiguration, position);
    }
    else
    {
      Debug.LogWarning("Evento de tocar foi lançado, mas ninguem capturou-o");
    }
    return AudioCueKey.invalid;
  }

  public bool RaiseStopEvent(AudioCueKey key)
  {
    if (OnAudioCueStopRequested != null)
    {
      return OnAudioCueStopRequested.Invoke(key);
    }
    else
    {
      Debug.LogWarning("Evento de parar um som foi lançado, mas ninguem capturou-o");
    }
    return false;
  }

  public bool RaiseFinishEvent(AudioCueKey key)
  {
    if (OnAudioCueFinishRequested != null)
    {
      return OnAudioCueFinishRequested.Invoke(key);
    }
    else
    {
      Debug.LogWarning("Evento de finalizar um som foi lançado, mas ninguem capturou-o");
    }
    return false;
  }
}


public delegate AudioCueKey AudioCuePlayAction(AudioCueSO audioCue, AudioConfigurationSO audioConfiguration, Vector3 position);
public delegate bool AudioCueStopAction(AudioCueKey key);
public delegate bool AudioCueFinishAction(AudioCueKey key);