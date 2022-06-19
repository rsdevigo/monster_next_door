using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns
{
  public interface IPoolable
  {
    IObjectPool Origin { get; set; }
    void ReturnToPool();
    void PrepareToUse();
  }
}

