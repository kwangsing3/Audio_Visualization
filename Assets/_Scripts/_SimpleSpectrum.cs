using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _SimpleSpectrum : MonoBehaviour
{
    public bool isEnabled = true;
    public enum SourceType
    {
        AudioSource, AudioListener, MicroophoneInput, SteroMix, Custom
    }

    #region SAMPLING PROPERTIES
    public SourceType _sourcetype = SourceType.AudioSource;
    public AudioSource _audiosource;

    [Tooltip("Sample採樣的數量，一定要是2的次方")]
    public int _numOfSamples = 256;
    public int _sampleChannel = 0;

    [Tooltip("FFTWindow的取樣模式")]
    public FFTWindow _FFTWindowType = FFTWindow.BlackmanHarris;

    public float _FrequencyLimitLow = 0;
    public float _FrequencyLimitHigh = 22050;
    public bool multiplyByFrequency = false;
    public bool _UseLogarithmicFrequency = false;
    #endregion


    #region BAR_SETTING


 

    public GameObject barPrefab;
    public int barAmount =32;
    [Range(0f, 20f)]
    public float _UpSpeed = 5f, _DownSpeed = 5f;

    public float _barScale_X= 1;
    public float _barXSpacing = 0; //Bar之間的間隔
  
    public float _barMinScale_Y=0.1f;
    public float _barScale_Y = 50f;

    public bool Is_Circle = false;
    [Range(0f, 360f)]
    public float _BarRotationX = 0.0f;
    #endregion

    #region COLOR_SETTINGS
   

    public AnimationCurve _colorValueCurve =new AnimationCurve(new Keyframe[] {new Keyframe(0,0),new Keyframe(1,1) });
   


    public bool _ChangeColor = true;
   



    #endregion

    public enum _ColorMode
    {
        Each, Clamp
    }
    public static _ColorMode _currentColorMode = _ColorMode.Each;



    Transform[] bars;
    Material[] barMaterials;
    public static float[] _OldScale_Y;
    float[] oldColorValues;
    float frequencyScaleFactor, highestLogFreq;
    // Start is called before the first frame update


    private void Awake()
    {
        _audiosource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }
    void Start()
    {
        
        if (_audiosource == null && _sourcetype == SourceType.AudioSource)
            Debug.LogError("沒有或是沒有找到AudioSource，請重新擺放一個");
        RebuildSpectrum();


    }

    bool MaterialColorCouldBeUsed = true;
    // Update is called once per frame
    public float _ForwardLength=10;

    public float[] _spectrum;

    public void RebuildSpectrum()
    {
       // Vector3 CachePos = this.transform.position;
        Quaternion CacheQue = this.transform.rotation;
        transform.eulerAngles = Vector3.zero;

        isEnabled = false;

        // clear child first
        int _childscount =transform.childCount;
        for(int i=0;i<_childscount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        _numOfSamples = Mathf.ClosestPowerOfTwo(_numOfSamples);



        // 重置相關陣列
        _spectrum = new float[_numOfSamples];  //儲存的光譜陣列
        bars = new Transform[barAmount];
        barMaterials = new Material[barAmount];
        _OldScale_Y = new float[barAmount];
        oldColorValues = new float[barAmount];
        




        float _SpectrumLength =barAmount*(1+_barXSpacing); //加上間隔後的總長度
        float _midPoint =_SpectrumLength /2;  //中間的位置是多少
        Vector3 _curveCenter = Vector3.zero;
       

        // About Clone
        for (int i=0;i<barAmount;i++)
        {
            GameObject _Barclone = Instantiate(barPrefab);
            if (Is_Circle)
            {
                _Barclone.transform.localPosition += Vector3.back * _ForwardLength;
                _Barclone.transform.parent = this.transform;
                _Barclone.transform.LookAt(transform);
                transform.eulerAngles += new Vector3(0,(360/barAmount), 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0,0,0);
                _Barclone.transform.parent = this.transform;
                _Barclone.transform.localPosition = new Vector3(i * (1 + _barXSpacing) - _midPoint, 0, 0);
            }
           
            _Barclone.transform.localScale = new Vector3(_barScale_X,_barMinScale_Y,1);

            bars[i] = _Barclone.transform;

            if(_BarRotationX>0)
            {
                bars[i].eulerAngles +=new Vector3(_BarRotationX, 0,0);
            }

            Renderer _rend = _Barclone.transform.GetChild(0).GetComponent<Renderer>();
            if (_rend != null)
            {
                barMaterials[i] = _rend.material;
                MaterialColorCouldBeUsed = true;
            }
            else
            {
                Debug.LogWarning("找不到Bar的Material");
                MaterialColorCouldBeUsed = false;
            }



        }

        frequencyScaleFactor=1.0f/(AudioSettings.outputSampleRate/2)*_numOfSamples;
        highestLogFreq =Mathf.Log(barAmount+1,2);

        isEnabled = true;
        this.transform.rotation = CacheQue;
        //this.transform.position = CachePos;
    }


 


    void Update()    
    {

        if(isEnabled)
        { 
            if(_sourcetype!=SourceType.Custom)
            {

                _audiosource.GetSpectrumData(_spectrum, _sampleChannel, _FFTWindowType);
            }


            // 讀取光譜後應用在Scale上
            for(int i=0;i<bars.Length;i++)
            {
                Transform bar = bars[i];

                float _value =0;
                float trueSampleIndex;

                if(_UseLogarithmicFrequency)
                {
                    trueSampleIndex = Mathf.Lerp(_FrequencyLimitLow,_FrequencyLimitHigh,(highestLogFreq-Mathf.Log(barAmount+1-i,2))/ highestLogFreq)* frequencyScaleFactor;
                }
                else
                // if else
                trueSampleIndex =Mathf.Lerp(_FrequencyLimitLow,_FrequencyLimitHigh,((float)i)/barAmount) * frequencyScaleFactor;

                int sampleIndexFloor = Mathf.FloorToInt(trueSampleIndex);
                sampleIndexFloor = Mathf.Clamp(sampleIndexFloor,0,_spectrum.Length-1);
                _value = Mathf.SmoothStep(_spectrum[sampleIndexFloor],_spectrum[sampleIndexFloor+1],trueSampleIndex-sampleIndexFloor);

                //if else
                if (multiplyByFrequency)
                {
                    _value = _value * (trueSampleIndex + 1);
                }
                _value =Mathf.Sqrt(_value);  //平方根

                // trueSampleIndex = Mathf.Lerp();
                float oldScaleY =_OldScale_Y[i];
                float newScale_Y;
                if (_value*_barScale_Y>oldScaleY)
                 newScale_Y = Mathf.Lerp(oldScaleY, Mathf.Max(_value*_barScale_Y,_barMinScale_Y), _UpSpeed*Time.deltaTime);
                else
                 newScale_Y=  Mathf.Lerp(oldScaleY, Mathf.Max(_value * _barScale_Y,_barMinScale_Y), _DownSpeed*Time.deltaTime);

                bar.localScale = new Vector3(_barScale_X,newScale_Y,1);

                _OldScale_Y[i] = newScale_Y;
                 
                /////////////////////////    About Color

                float newColorVal = _colorValueCurve.Evaluate(_value);
                float oldColorVal = oldColorValues[i];
                if (MaterialColorCouldBeUsed && _ChangeColor)
                {

                    switch(_currentColorMode)

                    {
                        case _ColorMode.Clamp:
                            if (MaterialColorCouldBeUsed && _ChangeColor)
                            {
                                if (newColorVal > oldColorVal)
                                {
                                    newColorVal = Mathf.Lerp(oldColorVal, newColorVal, _GlobalSetting._ColorUpLerpTime * Time.deltaTime);
                                }
                                else
                                {
                                    newColorVal = Mathf.Lerp(oldColorVal, newColorVal, _GlobalSetting._ColorDownLerpTime * Time.deltaTime);
                                }



                                Color newColor =
                                    new Color(
                                    Mathf.Clamp(newColorVal, _GlobalSetting._ThemeColor[0].r, _GlobalSetting._ThemeColor[1].r),
                                    Mathf.Clamp(newColorVal, _GlobalSetting._ThemeColor[0].g, _GlobalSetting._ThemeColor[1].g),
                                    Mathf.Clamp(newColorVal, _GlobalSetting._ThemeColor[0].b, _GlobalSetting._ThemeColor[1].b)
                                        );

                                // barMaterials[i].SetColor("_Color", newColor); 
                                barMaterials[i].color = newColor;




                                oldColorValues[i] = newColorVal;
                            }
                            break;
                        case _ColorMode.Each:
                            if (newColorVal > oldColorVal)
                            {



                                barMaterials[i].color = Color.Lerp(barMaterials[i].color, _GlobalSetting._ThemeColor[Random.Range(1, 4)], _GlobalSetting._ColorUpLerpTime * Time.deltaTime);
                            }
                            else
                                _GlobalSetting._ThemeColor[1] = Color.white;

                            barMaterials[i].color = Color.Lerp(barMaterials[i].color, _GlobalSetting._ThemeColor[Random.Range(1, 4)], _GlobalSetting._ColorDownLerpTime * Time.deltaTime);
                            break;
                    }


                  
                }

            }

        }
        else
        { 
            foreach(Transform bar in bars)
            {
                bar.localScale = Vector3.Lerp(bar.localScale,new Vector3(1, _barMinScale_Y, 1),_DownSpeed); 
            }
        }

    }





}
