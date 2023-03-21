using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "newFloatEventChannel", menuName = "Float Event Channel")]
public class FloatEventChannelSO : ScriptableObject
{
  public ChangeFloatAction OnChangeFloat;

  public void RaiseChangeEvent(float newValue)
  {
    if (OnChangeFloat != null)
    {
      OnChangeFloat.Invoke(newValue);
    }
  }

}

public delegate void ChangeFloatAction(float newValue);
