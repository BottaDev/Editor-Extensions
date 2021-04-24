using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(BaseSpawner))]
public class SpawnerEditor : Editor
{
    private BaseSpawner _spawner;
    private Texture2D texture;

    private void OnEnable()
    {
        _spawner = (BaseSpawner) target;
    }

    public override void OnInspectorGUI()
    {
        DrawObjectProperties();
        DrawTimerProperties();
        
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

    private void DrawObjectProperties()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("OBJECT", EditorStyles.boldLabel);

        _spawner.spawnerName = EditorGUILayout.TextField("Spawner Name:", _spawner.spawnerName);
        
        _spawner.objectToSpawn = EditorGUILayout.ObjectField("Object to Spawn:", _spawner.objectToSpawn, typeof(GameObject), true) as GameObject;

        if (_spawner.objectToSpawn != null)
        {
            texture = AssetPreview.GetAssetPreview(_spawner.objectToSpawn);
            
            if (texture != null)
                EditorGUI.DrawTextureTransparent(GUILayoutUtility.GetRect(150, 150), texture, ScaleMode.ScaleToFit);
        }
    }
    
    private void DrawTimerProperties()
    {
        GUILayout.Space(10);

        EditorGUILayout.LabelField("TIMER", EditorStyles.boldLabel);
            
        _spawner.spawnTime = EditorGUILayout.FloatField("Spawn Time:", _spawner.spawnTime);
        _spawner.useSphereRadius = EditorGUILayout.Toggle("Use Sphere Radius:", _spawner.useSphereRadius);
        
        if (!_spawner.useSphereRadius)
            _spawner.radius = EditorGUILayout.FloatField("Radius:", _spawner.radius);
        
        _spawner.currentSpawnTime = EditorGUILayout.Slider(_spawner.currentSpawnTime, 0, _spawner.spawnTime);
        EditorGUI.ProgressBar(GUILayoutUtility.GetRect(1,15), _spawner.currentSpawnTime / _spawner.spawnTime, "Time to spawn: " + _spawner.currentSpawnTime);
    }
}
