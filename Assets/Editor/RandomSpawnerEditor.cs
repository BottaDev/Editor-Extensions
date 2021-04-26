using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RandomSpawner))]
public class RandomSpawnerEditor : Editor
{
    private RandomSpawner _spawner;
    
    private void OnEnable()
    {
        _spawner = (RandomSpawner) target;
    }
    
    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();

        var serializedObject = new SerializedObject(target);
        var property = serializedObject.FindProperty("objectsToSpawn");
        serializedObject.Update();
        EditorGUILayout.PropertyField(property, true);
        serializedObject.ApplyModifiedProperties();
        
        _spawner.objectsAmount = EditorGUILayout.IntField("Object Count", _spawner.objectsAmount);
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Spawn", GUILayout.Height(30)))
            _spawner.SpawnObjects();
        
        EditorGUILayout.Space();
        
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        if (GUILayout.Button("Destroy all objects", GUILayout.Height(20), GUILayout.Width(200)))
            _spawner.DestroyAllObjects();
        
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(target);
        }
    }
}
