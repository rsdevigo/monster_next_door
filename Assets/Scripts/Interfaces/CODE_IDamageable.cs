using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface CODE_IDamageable : CODE_IInteractive
{
    public void Damage(int power);
}
