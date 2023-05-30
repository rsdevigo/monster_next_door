using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
  public class GenericPoolableObject : MonoBehaviour, IPoolable
  {
    public IObjectPool Origin { get; set; }

    public virtual void PrepareToUse()
    {
    }

    public virtual void ReturnToPool()
    {
      Origin.ReturnToPool(this);
    }
  }
}

