using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "newAudioCue", menuName = "Audio Cue")]
public class AudioCueSO : ScriptableObject
{
  public bool looping = false;
  [SerializeField] private AudioClipsGroup[] _audioClipsGroups = default;

  public AudioClip[] GetClips()
  {
    int numberOfClips = _audioClipsGroups.Length;
    AudioClip[] _audioClips = new AudioClip[numberOfClips];

    for (int i = 0; i < numberOfClips; i++)
    {
      _audioClips[i] = _audioClipsGroups[i].GetNextClip();
    }

    return _audioClips;
  }

}

[Serializable]
public class AudioClipsGroup
{
  public SequenceMode sequenceMode = SequenceMode.Random;
  public AudioClip[] audioClips;

  private int _nextClipToPlay = -1;
  private int _lastClipPlayed = -1;

  public AudioClip GetNextClip()
  {
    if (audioClips.Length == 1)
    {
      return audioClips[0];
    }

    if (_nextClipToPlay == -1)
    {
      _nextClipToPlay = (sequenceMode == SequenceMode.Sequential) ? 0 : UnityEngine.Random.Range(0, audioClips.Length);
    }
    else
    {
      switch (sequenceMode)
      {
        case SequenceMode.Random:
          _nextClipToPlay = UnityEngine.Random.Range(0, audioClips.Length);
          break;
        case SequenceMode.RandomNoImmediateRepeat:
          do
          {
            _nextClipToPlay = UnityEngine.Random.Range(0, audioClips.Length);
          } while (_nextClipToPlay == _lastClipPlayed);
          break;
        case SequenceMode.Sequential:
          _nextClipToPlay = (int)Mathf.Repeat(++_nextClipToPlay, audioClips.Length);
          break;
      }
    }
    _lastClipPlayed = _nextClipToPlay;
    return audioClips[_nextClipToPlay];
  }

}

public enum SequenceMode
{
  Random,
  RandomNoImmediateRepeat,
  Sequential
}