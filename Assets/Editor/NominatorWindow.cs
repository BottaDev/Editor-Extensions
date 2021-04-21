using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;
using UnityEngine;

public class NominatorWindow : EditorWindow
{
    private string _name;
    private string _newName;
    private bool _toogleCotainsText = true;
    private bool _toogleSameText;
    
    public static void OpenWindow(string name)
    {
        NominatorWindow window = GetWindow<NominatorWindow>();
        
        window._name = name;
        
        window.wantsMouseMove = true;
        window.minSize = new Vector2(250, 100);
    }

    private void OnGUI()
    {
        _name = EditorGUILayout.TextField("Old Name:", _name);
        _newName = EditorGUILayout.TextField("New Name:", _newName);
        
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

        if (GUILayout.Button("Rename"))
            RenameObjects(GetObjects());
    }

    private void RenameObjects(List<GameObject> objs)
    {
        foreach (GameObject obj in objs)
        {
            obj.name = _newName;
        }
    }
    
    private List<GameObject> GetObjects()
    {
        GameObject[] objs = FindObjectsOfType<GameObject>();

        List<GameObject> objsFiltered = null;
        
        if (_toogleCotainsText)
            objsFiltered = objs.Where(x => x.name.Contains(_name)).ToList();
        else if (_toogleSameText)
            objsFiltered = objs.Where(x => x.name == _name).ToList();

        return objsFiltered;
    }
}
