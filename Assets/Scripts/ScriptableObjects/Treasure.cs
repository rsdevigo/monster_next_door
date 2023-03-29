using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreasureItem", menuName = "Scriptable Objects/Treasure Items")]
public class Treasure : ScriptableObject
{
    public Sprite icon;
    public Sprite item;
    public TreasureType treasureType;
    public string name;
    public string description;
    public int bonus;
}
