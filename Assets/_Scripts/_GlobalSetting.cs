using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(_GlobalSetting))]
public class _GlobalSetting : MonoBehaviour
{


    public float ForwardScale = 70;
    // public GameObject Cubes512;

   
    public static bool _ShowGUI=false;

    public float EmissionStrength = 15;
    public float _BaseLightScale;
    public float _Bandlimit;
    public float _startScale = 45, _scaleMultiplier = 4500;
    public float _limit = 20;
    public float _LerpSpeed = 0.4f;
    public GameObject _ColoP;
    [Range(0, 20)]
    public static float _ColorUpLerpTime = 5f, _ColorDownLerpTime = 5f;
  

    //public FFTWindow _FFTWindowMode;
    public Button[] _ColorButtons;
    [HideInInspector]
    public static Color[] _ThemeColor;
    public  FFTWindow _FFTWindowMode;


    /*    _ThemeColor[0]:  MinColor
     *    _ThemeColor[1]:  MaxColor
     *    _ThemeColor[2]:  BeatColor1
     *    _ThemeColor[3]:  BeatColor2
     */


    private void Awake()
    {

    }


    void Start()
    {
        _ThemeColor = new Color[_ColorButtons.Length];
       for(int i=0;i<_ThemeColor.Length;i++)
        {
            _ThemeColor[i] = _ColorButtons[i].GetComponent<Image>().material.color;
        }

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
   

    private void OnGUI()
    {
        if(_ShowGUI)
        {
            if (GUI.Button(new Rect(10, 10, 160, 50), "Rebuild Spectrum"))
            {
                if (GameObject.Find("ThemePrefab").GetComponent<_SimpleSpectrum>() != null)
                    GameObject.Find("ThemePrefab").GetComponent<_SimpleSpectrum>().RebuildSpectrum(); // error
                else
                {
                    for (int i = 0; i < GameObject.Find("ThemePrefab").transform.childCount; i++)
                    {
                        if (GameObject.Find("ThemePrefab").transform.GetChild(i).GetComponent<_SimpleSpectrum>() != null)
                        {
                            GameObject.Find("ThemePrefab").transform.GetChild(i).GetComponent<_SimpleSpectrum>().RebuildSpectrum();
                        }
                    }
                }
            }


            GUI.BeginGroup(new Rect(10,60,120,140));

            GUI.Box(new Rect(10,70,200,200),"Color Setting");
            GUI.Label(new Rect(10, 80, 120, 50), "ColorUpSpeed :");

            _ColorUpLerpTime = GUI.HorizontalSlider(new Rect(10, 100, 160, 10), _ColorUpLerpTime, 0.0f, 20.0f);
            GUI.Label(new Rect(10, 110, 120, 50), "ColorDownSpeed :");
            _ColorDownLerpTime = GUI.HorizontalSlider(new Rect(10, 140, 160, 10), _ColorDownLerpTime, 0.0f, 20.0f);
            GUI.EndGroup();

        }


    }

 

    bool _picker_isopen = false;
    public int _colorindex = 0;
    GameObject _cacheColoP = null;
    public void _ColorChangedOnClink()
    {
        if (_picker_isopen)
            Destroy(_cacheColoP);
        else
         { 
        _cacheColoP = Instantiate(_ColoP,GameObject.Find("Canvas").transform);
        _cacheColoP.GetComponent<ColorPicker>().onValueChanged.AddListener(delegate { this._PickColorChanged(); } );

       
        }
        _picker_isopen = !_picker_isopen;
    }
    public void _callColor0() { _colorindex = 0; }
    public void _callColor1() { _colorindex = 1; }
    public void _callColor2() { _colorindex = 2; }
    public void _callColor3() { _colorindex = 3; }

    public  void _PickColorChanged()
    {
        if( _cacheColoP!=null)
        _ColorButtons[_colorindex].gameObject.GetComponent<Image>().material.color = _cacheColoP.GetComponent<ColorPicker>().CurrentColor ;
        _ThemeColor[_colorindex] = _cacheColoP.GetComponent<ColorPicker>().CurrentColor;

    }




   

}
