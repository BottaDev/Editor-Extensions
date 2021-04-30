using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplacerWindow : EditorWindow
{
    private GameObject _objectReplace;
    
    // Rotation
    private bool _useRotation = false;
    private bool _useRotX = true;
    private bool _useRotZ = true;
    private bool _useRotY = true;
    
    // Scale
    private bool _useScale = false;
    private bool _useScaX = true;
    private bool _useScaZ = true;
    private bool _useScaY = true;

    [MenuItem("CustomTools/Replacer")]
    public static void OpenWindow()
    {
        ReplacerWindow window = GetWindow<ReplacerWindow>();

        window.wantsMouseMove = true;
        window.minSize = new Vector2(300, 230);
    }
    
    private void OnGUI()
    {
        _objectReplace = EditorGUILayout.ObjectField("Object Replace: ", _objectReplace, typeof(GameObject), true) as GameObject;

        _useRotation = EditorGUILayout.Toggle("Use Rotation", _useRotation);
        if (_useRotation)
        {
            GUILayout.BeginVertical ("box");
            _useRotX = EditorGUILayout.Toggle("Use X", _useRotX);
            _useRotY = EditorGUILayout.Toggle("Use Y", _useRotY);
            _useRotZ = EditorGUILayout.Toggle("Use Z", _useRotZ);
            GUILayout.EndVertical();
        }

        _useScale = EditorGUILayout.Toggle("Use Scale", _useScale);
        if (_useScale)
        {
            GUILayout.BeginVertical ("box");
            _useScaX = EditorGUILayout.Toggle("Use X", _useScaX);
            _useScaY = EditorGUILayout.Toggle("Use Y", _useScaY);
            _useScaZ = EditorGUILayout.Toggle("Use Z", _useScaZ);
            GUILayout.EndVertical();
        }
        
        if (GUILayout.Button("Replace selected objects", GUILayout.Height(50)) && _objectReplace != null)
            ReplaceObjects();
    }

    private void ReplaceObjects()
    {
        if (Selection.gameObjects.Length == 0)
        {
            Debug.LogWarning("No objects selected to replace!");
            return;
        } 
        
        foreach (GameObject obj in Selection.gameObjects)
        {
            GameObject newObject = Instantiate(_objectReplace, obj.transform.position, _objectReplace.transform.rotation);

            if (_useRotation)
            {
                newObject.transform.rotation = new Quaternion(
                    _useRotX ? obj.transform.rotation.x : newObject.transform.rotation.x,
                    _useRotY ? obj.transform.rotation.y : newObject.transform.rotation.y,
                    _useRotZ ? obj.transform.rotation.z : newObject.transform.rotation.z,
                    obj.transform.rotation.w);
            }

            if (_useScale)
            {
                newObject.transform.localScale = new Vector3(
                    _useScaX ? obj.transform.localScale.x : newObject.transform.localScale.x,
                    _useScaY ? obj.transform.localScale.y : newObject.transform.localScale.y,
                    _useScaZ ? obj.transform.localScale.z : newObject.transform.localScale.z);
            }

            DestroyImmediate(obj);
        }
    }
}
