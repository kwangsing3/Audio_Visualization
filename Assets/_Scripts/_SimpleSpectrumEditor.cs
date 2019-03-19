using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




[CustomEditor(typeof(_SimpleSpectrum))]
[CanEditMultipleObjects]
public class _SimpleSpectrumEditor:Editor
{

    bool FoldOutOpen_Spectrum = true;

    void OnEnable()
    {


    }







    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        
      // FoldOutOpen_Spectrum= EditorGUILayout.Foldout(FoldOutOpen_Spectrum, "Spectrum Setting");


    }












}