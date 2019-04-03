using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(_GlobalSetting))]
public class _GlobalSetting : MonoBehaviour
{
    public enum _Theme
    {
        Theme_1, Theme_2, Theme_3
    }
    public enum _ColorMode
    {
        Each, Clamp
    }

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


    public static _Theme _CurrentTheme = _Theme.Theme_1;
    public _Theme _CT
    {
        get { return _CurrentTheme; }
        set
        {
            _CurrentTheme = value;
            _SwitchTheme();
        }
    }

    public static _ColorMode _currentColorMode = _ColorMode.Each;

    public int _DebugTheme = 0;

    public GameObject[] ThemeObject = new GameObject[3];

    Camera _camera;

    private FileManager _fm;
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
       
        _camera = GameObject.Find("MainCamera").GetComponent<Camera>();

        _GUIsty.fontSize = 10;
        _GUIsty.normal.textColor = Color.white;
        RecreateCubes();
        _SwitchTheme(_DebugTheme);
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

    private Vector2 scrollViewVector = Vector2.zero;
    int n,m;
    private string[] _scrollTheme = {"Theme 1", "Theme 2", "Theme 3" };//add the rest
    private string[] _scrollColor = {"Clamp", "Each" };//add the rest
    private void OnGUI()
    {
        
        if (_ShowGUI) //if(_ShowGUI)
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

            GUILayout.BeginVertical(GUI.skin.box);

            GUILayout.Box("Color Setting");
            //GUILayout.Box();
            GUILayout.Label( "ColorUpSpeed :", _GUIsty);
            _ColorUpLerpTime = GUILayout.HorizontalSlider( _ColorUpLerpTime, 0.0f, 20.0f);
            GUILayout.Label( "ColorDownSpeed :", _GUIsty);
            _ColorDownLerpTime = GUILayout.HorizontalSlider(_ColorDownLerpTime, 0.0f, 20.0f);
            GUILayout.Label("BarUpSpeed :", _GUIsty);
            _UpSpeed = GUILayout.HorizontalSlider(_UpSpeed, 0.0f, 20.0f);
            GUILayout.Label("BarDownSpeed :", _GUIsty);
            _DownSpeed = GUILayout.HorizontalSlider(_DownSpeed, 0.0f, 20.0f);


            if (GUILayout.Button("Theme_Setting"))
            {
                if (n == 0) n = 1;
                else n = 0;
            }
            if (n == 1)
            {
                scrollViewVector = GUILayout.BeginScrollView(scrollViewVector);
                GUILayout.Box(_CT.ToString());
                for (int i = 0; i < _scrollTheme.Length; i++)
                {
                    if (GUILayout.Button(_scrollTheme[i]))
                    {
                        _CT = (_Theme)i;
                    }        
                }
                GUI.EndScrollView();
            }
            else
            {
                GUILayout.Label("Theme :"+_CT.ToString());
            }
            if (GUILayout.Button("Color Mode"))
            {
                if (m == 0) m = 1;
                else m = 0;
            }
            if (m == 1)
            {
                scrollViewVector = GUILayout.BeginScrollView(scrollViewVector);
                GUILayout.Box(_currentColorMode.ToString());
                for (int i = 0; i < _scrollColor.Length; i++)
                {
                    if (GUILayout.Button(_scrollColor[i]))
                    {
                        _currentColorMode = (_ColorMode)i;
                    }

                }
                GUI.EndScrollView();
            }
            else
            {
                GUILayout.Label("Mode :" + _currentColorMode.ToString());
            }
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
        _cacheColoP.GetComponent<ColorPicker>().CurrentColor = _ColorButtons[_colorindex].GetComponent<Image>().material.color;


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



    public void _SwitchTheme()
    {

        Destroy(GameObject.Find("ThemePrefab"));
        GameObject _themePrefab = null;

        switch (_CT)
        {
            case _GlobalSetting._Theme.Theme_1:
                //_CurrentTheme = _Theme.Theme_1;
                _themePrefab = Instantiate(ThemeObject[0]);
                _camera.orthographic = true;
                print("Switch Theme to :1");
                break;
            case _GlobalSetting._Theme.Theme_2:
                // _CurrentTheme = _Theme.Theme_2;
                _themePrefab = Instantiate(ThemeObject[1]);
                _camera.orthographic = true;
                print("Switch Theme to :2");
                break;
            case _GlobalSetting._Theme.Theme_3:
                // _CurrentTheme = _Theme.Theme_3;
                _themePrefab = Instantiate(ThemeObject[2]);
                _camera.orthographic = false;
                print("Switch Theme to :3");
                break;
            default:
                print("_設定錯啦（笑)");
                break;
        }


        _themePrefab.name = "ThemePrefab";
    }
    public void _SwitchTheme(int index)
    {

        Destroy(GameObject.Find("ThemePrefab"));
        GameObject _themePrefab = null;

        switch (index)
        {
            case 0:
                _GlobalSetting._CurrentTheme = _GlobalSetting._Theme.Theme_1;
                _themePrefab = Instantiate(ThemeObject[0]);
                _camera.orthographic = true;

                print("Switch Theme to :1");
                break;
            case 1:
                _GlobalSetting._CurrentTheme = _GlobalSetting._Theme.Theme_2;
                _themePrefab = Instantiate(ThemeObject[1]);
                print("Switch Theme to :2");
                _camera.orthographic = true;
                break;
            case 2:
                _GlobalSetting._CurrentTheme = _GlobalSetting._Theme.Theme_3;
                _themePrefab = Instantiate(ThemeObject[2]);
                print("Switch Theme to :3");
                _camera.orthographic = false;

                break;
            default:
                print("_設定錯啦（笑)");
                break;

        }


        _themePrefab.name = "ThemePrefab";
    }



}
