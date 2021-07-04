using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Weapon : MonoBehaviour
{
    public WeaponConfig config;

    private void Start()
    {
        if (config != null)
            SetWeapon();
    }

    private void SetWeapon()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        MeshFilter filter = GetComponent<MeshFilter>();

        if (config.Material != null)
            renderer.material = config.Material;
        
        if (config.Mesh != null)
            filter.mesh = config.Mesh;
        
        gameObject.name = config.Name;
    }
}
