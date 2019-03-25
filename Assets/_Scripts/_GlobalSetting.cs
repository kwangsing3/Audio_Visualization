﻿using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[RequireComponent(typeof(_GlobalSetting))]
public class _GlobalSetting : MonoBehaviour
{


    public float ForwardScale = 70;
   // public GameObject Cubes512;

    public Color _AddThemeColor=Color.blue;
    public Color _AddThemeColor2 = Color.blue;


    public float EmissionStrength =15;
    public float _BaseLightScale;
    public float _Bandlimit;
    public float _startScale=45,_scaleMultiplier = 4500;
    public float _limit =20;
    public float _LerpSpeed=0.4f;

    Camera _camera;
    //public FFTWindow _FFTWindowMode;
  
  
    public  FFTWindow _FFTWindowMode;
    public Color _MainBackColor = Color.white;


    private void Awake()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }


    void Start()
    {
        RecreateCubes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RecreateCubes ()
    {

            if(GameObject.Find("512_Cubes")!=null)
            {
                Destroy(GameObject.Find("512_Cubes"));
            }
        //    GameObject _new= Instantiate(Cubes512);
        //    _new.transform.name= "512_Cubes";
           // if (Cubes512.GetComponent<Sc_InstantiateCube>() != null)    Cubes512.GetComponent<Sc_InstantiateCube>().enabled = true;
        

    }






}
