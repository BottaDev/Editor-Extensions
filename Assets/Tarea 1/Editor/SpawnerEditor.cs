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
        
        DrawUILine(Color.gray, 2, 10);
        
        DrawTimerProperties();

        DrawObjectPreview();
        
        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }

    private void DrawObjectPreview()
    {
        if (_spawner.objectToSpawn != null)
        {
            DrawUILine(Color.gray, 2, 10);
            
            EditorGUILayout.LabelField("OBJECT PREVIEW", EditorStyles.boldLabel);
            
            texture = AssetPreview.GetAssetPreview(_spawner.objectToSpawn);

            if (texture != null)
                EditorGUI.DrawTextureTransparent(GUILayoutUtility.GetRect(100, 100), texture, ScaleMode.ScaleToFit);
        }
    }

    private void DrawObjectProperties()
    {
        EditorGUILayout.Space();

        EditorGUILayout.LabelField("OBJECT", EditorStyles.boldLabel);

        _spawner.spawnerName = EditorGUILayout.TextField("Spawner Name:", _spawner.spawnerName);
        
        if (_spawner.spawnerName == "" || _spawner.spawnerName == string.Empty)
            EditorGUILayout.HelpBox("Warning: The spawner should have a name", MessageType.Warning, true);
        
        _spawner.objectToSpawn = EditorGUILayout.ObjectField("Object to Spawn:", _spawner.objectToSpawn, typeof(GameObject), true) as GameObject;
    }
    
    private void DrawTimerProperties()
    {
        EditorGUILayout.LabelField("TIMER", EditorStyles.boldLabel);
            
        _spawner.spawnTime = EditorGUILayout.FloatField("Spawn Time:", _spawner.spawnTime);
        
        if (_spawner.spawnTime < 0)
            EditorGUILayout.HelpBox("Error: Time should be higher than 0", MessageType.Error, true);
        
        _spawner.useSphereRadius = EditorGUILayout.Toggle("Use Sphere Radius:", _spawner.useSphereRadius);

        if (!_spawner.useSphereRadius)
        {
            _spawner.radius = EditorGUILayout.FloatField("Radius:", _spawner.radius);
            if (_spawner.radius < 0)
                EditorGUILayout.HelpBox("Error: Radius should be higher than 0", MessageType.Error, true);
        }
        
        EditorGUILayout.Space();
        
        EditorGUI.ProgressBar(GUILayoutUtility.GetRect(1,15), _spawner.currentSpawnTime / _spawner.spawnTime, "Next spawn in: " + _spawner.currentSpawnTime.ToString("00.00") + " seconds");
    }
    
    private void DrawUILine(Color color, int thickness = 2, int padding = 10)
    {
        Rect r = EditorGUILayout.GetControlRect(GUILayout.Height(padding+thickness));
        r.height = thickness;
        r.y+=padding/2;
        r.x-=2;
        r.width +=6;
        EditorGUI.DrawRect(r, color);
    }
}
