using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;








[CustomEditor(typeof(_GlobalSetting))]
public class ObjectBuilderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        //   DrawDefaultInspector();

        _GlobalSetting _script = (_GlobalSetting)target;
        if(GUILayout.Button("Recreate Cubes"))
        {
            _script.RecreateCubes();
        }

    }
}
