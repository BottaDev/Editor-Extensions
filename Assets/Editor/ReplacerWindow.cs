using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplacerWindow : EditorWindow
{
    private GameObject _objectReplace;
    private bool _useRotation = true;
    private bool _useScale = true;

    [MenuItem("CustomTools/Replacer")]
    public static void OpenWindow()
    {
        ReplacerWindow window = GetWindow<ReplacerWindow>();

        window.wantsMouseMove = true;
        window.minSize = new Vector2(300, 110);
    }
    
    private void OnGUI()
    {
        _objectReplace = EditorGUILayout.ObjectField("Object Replace: ", _objectReplace, typeof(GameObject), true) as GameObject;

        _useRotation = EditorGUILayout.Toggle("Use Rotation", _useRotation);
        _useScale = EditorGUILayout.Toggle("Use Scale", _useScale);
        
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

            newObject.transform.rotation = _useRotation ? obj.transform.rotation : newObject.transform.rotation;
            newObject.transform.localScale = _useScale ? obj.transform.localScale : newObject.transform.localScale;

            DestroyImmediate(obj);
        }
    }
}
