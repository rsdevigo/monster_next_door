using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
  public class GenericObjectPool<T> : MonoBehaviour, IObjectPool<T> where T : MonoBehaviour, IPoolable
  {
    [SerializeField]
    private T prefab;

    private Stack<T> reusableInstances = new Stack<T>();
    public T GetPrefabInstance()
    {
      T inst;
      if (reusableInstances.Count > 0)
      {
        inst = reusableInstances.Pop();
        inst.transform.parent = null;
        inst.transform.localPosition = Vector3.zero;
        inst.transform.localScale = Vector3.one;
        inst.transform.localEulerAngles = Vector3.one;
        inst.gameObject.SetActive(true);
      }
      else
      {
        inst = Instantiate(prefab);
      }
      inst.Origin = this;
      inst.PrepareToUse();
      return inst;
    }

    public void ReturnToPool(T instance)
    {
      instance.gameObject.SetActive(false);
      instance.transform.SetParent(transform);
      instance.transform.localPosition = Vector3.zero;
      instance.transform.localScale = Vector3.one;
      instance.transform.localEulerAngles = Vector3.one;

      reusableInstances.Push(instance);
    }

    public void ReturnToPool(object instance)
    {
      if (instance is T)
      {
        ReturnToPool(instance as T);
      }
    }
  }
}

