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
    [Range(0, 20)]
    public static float _UpSpeed = 5f, _DownSpeed = 5f;

    //public FFTWindow _FFTWindowMode;
    public Button[] _ColorButtons;
    [HideInInspector]
    public static Color[] _ThemeColor;
    public  FFTWindow _FFTWindowMode;
    GUIStyle _GUIsty = new GUIStyle();

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
        
        _GUIsty.fontSize = 10;
        _GUIsty.normal.textColor = Color.white;
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


    float _SceenX = Screen.width;
    float _SceenY = Screen.height;
<<<<<<< HEAD
    private Vector2 scrollViewVector = Vector2.zero;
    int n, i, wichcountry;
    private string[] countrys = { "Any country", "Afghanistan", "Albania", "Algeria" };//add the rest
=======
    
>>>>>>> 48c12b14f3bc360cdd4861c80fb6ca2829216047
    private void OnGUI()
    {
        float _nextPosY = 0;
        if (true) //if(_ShowGUI)
        {
            if (GUILayout.Button("Rebuild Spectrum"))
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

            _nextPosY += (_SceenY / 2) * 2 / 10;


            GUILayout.BeginVertical(GUI.skin.box);

            GUILayout.Box("Color Setting");
            //GUILayout.Box();
<<<<<<< HEAD
            GUILayout.Label( "ColorUpLerp :", _GUIsty);
            _ColorUpLerpTime = GUILayout.HorizontalSlider( _ColorUpLerpTime, 0.0f, 20.0f);
            GUILayout.Label("ColorDownLerp :", _GUIsty);
            _ColorDownLerpTime = GUILayout.HorizontalSlider(_ColorDownLerpTime, 0.0f, 20.0f);
            GUILayout.Label("BarUpLerp :", _GUIsty);
            _UpSpeed = GUILayout.HorizontalSlider(_UpSpeed, 0.0f, 20.0f);
            GUILayout.Label("BarDownLerp :", _GUIsty);
            _DownSpeed = GUILayout.HorizontalSlider(_DownSpeed, 0.0f, 20.0f);


            if (GUILayout.Button("Theme_Setting"))
            {
                if (n == 0) n = 1;
                else n = 0;
            }

            if (n == 1)
            {
                scrollViewVector = GUI.BeginScrollView(new Rect(25, 50, 100, 115), scrollViewVector, new Rect(0, 0, 300, 500));
                GUI.Box(new Rect(0, 0, 300, 500), "");
                for (i = 0; i < 4; i++)
                {
                    if (GUI.Button(new Rect(0, i * 25, 300, 25), ""))
                    {
                        n = 0; wichcountry = i;
                    }
                    GUI.Label(new Rect(5, i * 25, 300, 25), countrys[i]);
                }
                GUI.EndScrollView();
            }
            else
            {
                GUI.Label(new Rect(30, 50, 300, 25), countrys[wichcountry]);
            }



=======
            GUILayout.Label( "ColorUpSpeed :", _GUIsty);
            _ColorUpLerpTime = GUILayout.HorizontalSlider( _ColorUpLerpTime, 0.0f, 20.0f);
            GUILayout.Label( "ColorDownSpeed :", _GUIsty);
            _ColorDownLerpTime = GUILayout.HorizontalSlider(_ColorDownLerpTime, 0.0f, 20.0f);
            GUILayout.Label("BarUpSpeed :", _GUIsty);
            _UpSpeed = GUILayout.HorizontalSlider(_UpSpeed, 0.0f, 20.0f);
            GUILayout.Label("BarDownSpeed :", _GUIsty);
            _DownSpeed = GUILayout.HorizontalSlider(_DownSpeed, 0.0f, 20.0f);
>>>>>>> 48c12b14f3bc360cdd4861c80fb6ca2829216047
            GUILayout.EndVertical();
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
