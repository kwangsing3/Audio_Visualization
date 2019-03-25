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


    public float EmissionStrength = 15;
    public float _BaseLightScale;
    public float _Bandlimit;
    public float _startScale = 45, _scaleMultiplier = 4500;
    public float _limit = 20;
    public float _LerpSpeed = 0.4f;
    public GameObject _ColoP;
    [Range(0, 20)]
    public static float _ColorUpLerpTime = 5f, _ColorDownLerpTime = 5f;
    public static Color _MinColor = Color.grey;
    public static Color _TargetColor = Color.yellow;
    public Color _targetColor = Color.white;
    public Color _minColor = Color.yellow;
    Camera _camera;
    //public FFTWindow _FFTWindowMode;
    public Button[] _ColorButtons;
  
    public  FFTWindow _FFTWindowMode;



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
   

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 160, 50), "Rebuild Spectrum"))
        {
            GameObject.Find("ThemePrefab").GetComponent<_SimpleSpectrum>().RebuildSpectrum(); // error
        }


        _ColorUpLerpTime = GUI.HorizontalSlider(new Rect(10, 70, 160, 10), _ColorUpLerpTime, 0.0f, 20.0f);

        _ColorDownLerpTime = GUI.HorizontalSlider(new Rect(10, 90, 160, 10), _ColorDownLerpTime, 0.0f, 20.0f);


    }

   public void _OnColorPickerChanged()
    {
        _TargetColor = _ColoP.GetComponent<ColorPicker>().CurrentColor;
        _targetColor = _TargetColor;
    }

    bool _picker_isopen = false;
    public   int _colorindex = 0;
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
            
    }

}
