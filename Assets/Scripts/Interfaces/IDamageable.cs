using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable : IInteractive
{
    public void Damage(int power);
}
