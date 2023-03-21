using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
  public abstract class SingletonOnDemand<T> : MonoBehaviour where T : MonoBehaviour
  {
    private static T instance;
    private static bool singletonDestroyed = false;
    public static T Instance
    {
      get
      {
        if (singletonDestroyed)
        {
          return null;
        }

        if (!instance)
        {
          new GameObject(typeof(T).ToString()).AddComponent<T>();
          DontDestroyOnLoad(instance);
        }

        return instance;
      }
    }

    protected void Awake()
    {
      if (instance == null && !singletonDestroyed)
      {
        instance = this as T;
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
      singletonDestroyed = true;
      instance = null;
    }
  }
}

