using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEmitterCollection
{
  private int _nextIndex = 0;
  private List<AudioCueKey> _emittersKey;
  private List<SoundEmitter[]> _emittersList;

  public SoundEmitterCollection()
  {
    _emittersKey = new List<AudioCueKey>();
    _emittersList = new List<SoundEmitter[]>();
  }

  public AudioCueKey GetKey(AudioCueSO audioCue)
  {
    return new AudioCueKey(_nextIndex++, audioCue);
  }

  public void Add(AudioCueKey key, SoundEmitter[] emitters)
  {
    _emittersKey.Add(key);
    _emittersList.Add(emitters);
  }

  public AudioCueKey Add(AudioCueSO audioCue, SoundEmitter[] emitters)
  {
    AudioCueKey key = GetKey(audioCue);
    Add(key, emitters);

    return key;
  }

  public bool Get(AudioCueKey key, out SoundEmitter[] emitters)
  {
    int index = _emittersKey.FindIndex(x => x == key);
    if (index < 0)
    {
      emitters = null;
      return false;
    }

    emitters = _emittersList[index];
    return true;
  }

  public bool Remove(AudioCueKey key)
  {
    int index = _emittersKey.FindIndex(x => x == key);
    return RemoveAt(index);
  }

  private bool RemoveAt(int index)
  {
    if (index < 0)
    {
      return false;
    }

    _emittersKey.RemoveAt(index);
    _emittersList.RemoveAt(index);

    return true;
  }
}
