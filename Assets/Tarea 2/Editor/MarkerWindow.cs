using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MarkerWindow : EditorWindow
{
    private static bool _toogleCotainsText = true;
    private static bool _toogleSameText;
    private static bool _toogleChangeName;
    private static string _name;
    private static string _newName;
    private static Color _color;
    
    private static readonly GUIStyle _subStyle = new GUIStyle(EditorStyles.label);
    private static readonly GUIStyle _titleStyle = new GUIStyle(EditorStyles.label);
    private static readonly GUIStyle _style = new GUIStyle(EditorStyles.label);

    [MenuItem("CustomTools/Marker")]
    public static void OpenWindow()
    {
        MarkerWindow window = GetWindow<MarkerWindow>();

        window.wantsMouseMove = true;
        
        window.minSize = new Vector2(360, 310);
        window.maxSize = new Vector2(360, 310);

        _subStyle.fontSize = 15;
        _subStyle.fontStyle = FontStyle.Bold;
        _titleStyle.fontSize = 20;
        _titleStyle.fontStyle = FontStyle.Bold;
    }
    
    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUI.LabelField(new Rect(position.width / 2 - 80, 10, 200, 200), "Object Marker", _titleStyle);
        EditorGUILayout.EndHorizontal();;
        
        EditorGUILayout.BeginHorizontal();
        GUI.Label(new Rect(10, 50, 200,25),"Object Name: ",_style);
        _name = EditorGUI.TextField(new Rect(150 , 50, 200,15), _name);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUI.Label(new Rect(10, 80, 200,25),"(Optional) New Name: ",_style);
        _newName = EditorGUI.TextField(new Rect(150 , 80, 200,15), _newName);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUI.Label(new Rect(10, 110, 200,25),"Change Name: ",_style);
        _toogleChangeName = EditorGUI.Toggle(new Rect(150, 110, 200, 15), _toogleChangeName);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUI.Label(new Rect(10, 140, 200,25),"Set Color: ", _style);
        _color = EditorGUI.ColorField(new Rect(150, 140, 200, 15), _color);
        EditorGUILayout.EndHorizontal();

        DrawUILine(Color.gray, 2, 320);
        
        EditorGUILayout.BeginHorizontal();
        GUI.Label(new Rect(10, 170, 200,25),"Find Parameters", _subStyle);
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUI.Label(new Rect(10, 200, 200,25),"Contains Text: ",_style);
        if (EditorGUI.Toggle(new Rect(150, 200, 200, 15), _toogleCotainsText))
        {
            _toogleCotainsText = true;
            _toogleSameText = false;
        }
        EditorGUILayout.EndHorizontal();
        
        EditorGUILayout.BeginHorizontal();
        GUI.Label(new Rect(10, 230, 200,25),"Same Text: ",_style);
        if (EditorGUI.Toggle(new Rect(150, 230, 200, 15), _toogleSameText))
        {
            _toogleCotainsText = false;
            _toogleSameText = true;
        }
        EditorGUILayout.EndHorizontal();
        
        DrawUILine(Color.gray, 2, -150);
        
        if (GUI.Button(new Rect(10, 260, 340, 50), "Mark Objects"))
            MarkObjects(GetObjects());
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
    
    private void MarkObjects(List<GameObject> objs)
    {
        if (objs == null || objs.Count == 0)
            return;
        
        foreach (GameObject obj in objs)
        {
            MeshRenderer renderer = obj.GetComponent<MeshRenderer>();
            
            if (renderer.sharedMaterial.name.Contains("Default-Material"))
                continue;

            renderer.sharedMaterial.color = _color;
        }
        
        Debug.Log("Objects marked");
        
        if (_toogleChangeName)
            RenameObjects(objs);
    }

    private void RenameObjects(List<GameObject> objs)
    {
        foreach (GameObject obj in objs)
        {
            obj.name = _newName;
        }
        
        Debug.Log("Objects renamed");
    }
    
    private List<GameObject> GetObjects()
    {
        GameObject[] objs = FindObjectsOfType<GameObject>();

        List<GameObject> objsFiltered = null;

        if (string.IsNullOrEmpty(_name))
        {
            Debug.LogError("Object Name value is not assigned!");
            return null;
        }

        if (_toogleCotainsText)
            objsFiltered = objs.Where(x => x.name.Contains(_name) && x.GetComponent<MeshRenderer>() != null).ToList();
        else if (_toogleSameText)
            objsFiltered = objs.Where(x => x.name == _name && x.GetComponent<MeshRenderer>() != null).ToList();

        if (objsFiltered == null || objsFiltered.Count == 0)
            Debug.LogWarning("No objects finded with the name " + "'" + _name + "'");
        
        return objsFiltered;
    }
}
