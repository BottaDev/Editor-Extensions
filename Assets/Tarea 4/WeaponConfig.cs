using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponConfig")]
public class WeaponConfig : ScriptableObject
{
    public new string Name;
    public Quality QualityLevel;
    public float Damage;
    public float Recoil;
    public Mesh Mesh;
    public Material Material;

    public enum Quality
    {
        Common,
        Rare,
        Epic,
        Legendary
    }
}
