using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Patterns
{
  public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
  {
    private static T instance;
    public static T Instance => instance;

    protected void Awake()
    {
      if (instance == null)
      {
        instance = this as T;
        DontDestroyOnLoad(instance);
      }
      else if (instance != this)
      {
        Destroy(this);
      }
    }

    protected void OnDestroy()
    {
      if (instance != this)
      {
        return;
      }

      instance = null;
    }
  }
}

