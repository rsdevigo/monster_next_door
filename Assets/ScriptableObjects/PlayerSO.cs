using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Animations;

[CreateAssetMenu(fileName ="NewPlayerSO", menuName ="Monster Next Door/PlayerSO")]
public class PlayerSO : ScriptableObject
{
    public int level = 1;
    public int bonus = 0;
}
