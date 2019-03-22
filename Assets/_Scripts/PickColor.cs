using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickColor : MonoBehaviour {


    public ColorPicker _colorPicker;
    public GameObject _FrontCusbes;
    public GameObject _BackCusbes;
    
  
    private bool _PickerIsOpen=false;
    private int _index;
    public Button[] Btn_color;
    public Slider _lightslider;
    public static Color _Color1, _Color2, _Color3, _Color4;
    public static float _lightscale;


    // Use this for initialization
    void Start () {
        
        _colorPicker.gameObject.SetActive(false);
       
        _lightslider.onValueChanged.AddListener(delegate { SliderVaule(); });
        _lightslider.value = 0.3f;

    }
	
	// Update is called once per frame
	void Update () {


      
	}

    public void Btn_Color_1()
    {
        OpenColorPicker(Btn_color[0].transform,0);
    }
    public void Btn_Color_2()
    {
        OpenColorPicker(Btn_color[1].transform,1);
    }
    public void Btn_Color_3()
    {
        OpenColorPicker(Btn_color[2].transform,2);
    }
    public void Btn_Color_4()
    {
        OpenColorPicker(Btn_color[3].transform,3);
    }

    void OpenColorPicker(Transform _position,int _index)
    {
        if (_PickerIsOpen)
        {
            _colorPicker.gameObject.SetActive(true);
            _colorPicker.GetComponent<ColorPicker>().onValueChanged.AddListener(delegate { ChangeColor(_index);});
        }
        else if (!_PickerIsOpen )
        {
            _colorPicker.gameObject.SetActive(false);
            _colorPicker.GetComponent<ColorPicker>().onValueChanged.RemoveAllListeners();
        }

        _colorPicker.gameObject.transform.position =new Vector3(_position.position.x-(60f), _position.position.y+80f, _position.position.z);
        _PickerIsOpen = !_PickerIsOpen;

    }
    void SliderVaule()
    {
        _lightscale = _lightslider.value;
    }

    void ChangeColor(int index)
    {
        _index = index;
        Color _currentColor = _colorPicker.CurrentColor;
        Btn_color[_index].GetComponent<Image>().color = _currentColor;
        

        switch (_index)
        {
            case 0:
                _Color1 = _currentColor;
              
                break;
            case 1:
                _Color2 = _currentColor;
              
                break;
            case 2:
                _Color3 = _currentColor;
                break;
            case 3:
                _Color4 = _currentColor ;
                break;

            default: print("Switch設定出錯");　　break;
        }



      
    }

}
