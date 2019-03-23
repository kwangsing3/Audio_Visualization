using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;




[CustomEditor(typeof(_SimpleSpectrum))]
[CanEditMultipleObjects]
public class _SimpleSpectrumEditor:Editor
{

    SerializedProperty _propertyBaramount;


    bool FoldOutOpen_SampleSetting = true;
    bool FoldOutOpen_BarsSetting = true;
    bool FoldOutOpen_ColorSetting = true;
    bool FoldOutOpen_Spectrum = true;
    void OnEnable()
    {

        _propertyBaramount = serializedObject.FindProperty("barAmount"); //排定數值
    }







    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        // FoldOutOpen_Spectrum= EditorGUILayout.Foldout(FoldOutOpen_Spectrum, "Spectrum Setting");
        _SimpleSpectrum _script = (_SimpleSpectrum)this.target;
        if (GUILayout.Button("Rebuild Spectrum"))
        {
            _script.RebuildSpectrum();
        }



        FoldOutOpen_BarsSetting = EditorGUILayout.Foldout( FoldOutOpen_BarsSetting,"Bar Settings");
        if (FoldOutOpen_BarsSetting)  // 如果是True 才繪製欄位
        {
            EditorGUILayout.PropertyField(_propertyBaramount);
        }

    

    }












}