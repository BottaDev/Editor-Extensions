using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ReplacerWindow : EditorWindow
{
    private static GameObject _objectReplace;
    
    // Rotation
    private static bool _useRotation = false;
    private static bool _useRotX = true;
    private static bool _useRotZ = true;
    private static bool _useRotY = true;

    // Scale 
    private static bool _useScale = false;
    private static bool _useScaX = true;
    private static bool _useScaZ = true;
    private static bool _useScaY = true;
    
    private bool _objectError = false;
    private bool _selectionError = false;
    
    [MenuItem("CustomTools/Replacer")]
    public static void OpenWindow()
    {
        ReplacerWindow window = GetWindow<ReplacerWindow>();

        window.wantsMouseMove = true;
        window.minSize = new Vector2(300, 400);
        window.maxSize = new Vector2(300, 400);
    }
    
    private void OnGUI()
    {
        EditorGUILayout.HelpBox("Select objects in the scene to replace", MessageType.Info, true);
        
        _objectReplace = EditorGUILayout.ObjectField("Object Replace: ", _objectReplace, typeof(GameObject), true) as GameObject;

        if (_objectError)
            EditorGUILayout.HelpBox("Object field is empty!", MessageType.Error, true);
        
        DrawUILine(Color.gray, 2, 10);

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
        
        DrawUILine(Color.gray, 2, 10);
        
        if (_selectionError)
            EditorGUILayout.HelpBox("There's no selected objects in the scene!", MessageType.Error, true);
        
        if (GUILayout.Button("Replace Selected Objects", GUILayout.Height(50)))
            ReplaceObjects();
    }

    private void ReplaceObjects()
    {
        if (_objectReplace == null)
            _objectError = true;
        else
            _objectError = false;

        if (Selection.gameObjects.Length == 0)
            _selectionError = true;
        else
            _selectionError = false;
        
        if (_objectError || _selectionError)
            return;

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
        
        Debug.Log("Objects replaces successfully!");
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
