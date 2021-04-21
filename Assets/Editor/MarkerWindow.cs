using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class MarkerWindow : EditorWindow
{
    private bool _toogleCotainsText = true;
    private bool _toogleSameText;
    private string _name;
    private Color _color;

    [MenuItem("CustomTool/Marker")]
    public static void OpenWindow()
    {
        MarkerWindow window = GetWindow<MarkerWindow>();

        window.wantsMouseMove = true;
        window.minSize = new Vector2(250, 170);
    }
    
    private void OnGUI()
    {
        GUILayout.BeginHorizontal ("box"); 
        GUILayout.FlexibleSpace();
        EditorGUILayout.LabelField("Write the name of the objects", EditorStyles.boldLabel);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal ();
        
        _name = EditorGUILayout.TextField("Name:", _name);

        if (EditorGUILayout.Toggle("Contains Text", _toogleCotainsText))
        {
            _toogleCotainsText = true;
            _toogleSameText = false;
        }

        if (EditorGUILayout.Toggle("Same Text", _toogleSameText))
        {
            _toogleCotainsText = false;
            _toogleSameText = true;
        }
        
        _color = EditorGUILayout.ColorField("Set Color", _color);

        if (GUILayout.Button("Rename Objects"))
            NominatorWindow.OpenWindow(_name);
        
        if (GUILayout.Button("Mark Objects", GUILayout.Height(50)) && !string.IsNullOrEmpty(_name))
            MarkObjects(GetObjects());
    }
    
    private void MarkObjects(List<GameObject> objs)
    {
        if (objs == null)
            return;
        
        foreach (GameObject obj in objs)
        {
            obj.GetComponent<MeshRenderer>().sharedMaterial.color = _color;
        }
    }

    private List<GameObject> GetObjects()
    {
        GameObject[] objs = FindObjectsOfType<GameObject>();

        List<GameObject> objsFiltered = null;
        
        if (_toogleCotainsText)
            objsFiltered = objs.Where(x => x.name.Contains(_name) && x.GetComponent<MeshRenderer>() != null).ToList();
        else if (_toogleSameText)
            objsFiltered = objs.Where(x => x.name == _name && x.GetComponent<MeshRenderer>() != null).ToList();

        return objsFiltered;
    }
}
