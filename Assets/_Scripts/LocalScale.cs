using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocalScale : MonoBehaviour
{


    public int _band;


    public Color _currentColor;
    public float _Limit;

   
    Color _cacheColor;
    private _GlobalSetting _myGlo;

    float _TargetScaleY;


    Material _material;
    // Start is called before the first frame update
    void Start()
    {
        _material =GetComponent<MeshRenderer>().material;
        if (_material == null)
            print("Not found");

        _myGlo = GameObject.Find("_GlobalSetting").GetComponent<_GlobalSetting>();


    }

    // Update is called once per frame
    void Update()
    {

        Fun_Scale();


        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(transform.localScale.x, _TargetScaleY, transform.localScale.z), _myGlo._LerpSpeed);
       // transform.localScale = new Vector3(transform.localScale.x, _TargetScaleY, transform.localScale.z);

        // 顏色相關
        Color _Finalcolor = Color.white;
        switch (Random.Range(1, 3))
        {
            case 1:
                _Finalcolor = new Color(
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * (0.8f - _myGlo._AddThemeColor.r),
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * (0.8f - _myGlo._AddThemeColor.g),
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * (0.8f - _myGlo._AddThemeColor.b));
                break;
            case 2:
                _Finalcolor = new Color(
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * 0.8f - _myGlo._AddThemeColor2.r,
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * 0.8f - _myGlo._AddThemeColor2.g,
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * 0.8f - _myGlo._AddThemeColor2.b);
                break;
        }
        _cacheColor = Color.Lerp(_material.GetColor("_EmissionColor"),_myGlo._MainBackColor - (_Finalcolor * _myGlo.EmissionStrength) , Time.deltaTime * _myGlo._LerpSpeed);

        _material.SetVector("_EmissionColor", _cacheColor);


    }

    protected void Fun_Scale()
    {
        //縮放相關
        if (_TargetScaleY > _myGlo._limit)
            _TargetScaleY = AudioPeer._audiobandBuffer[_band] * _myGlo._scaleMultiplier + _myGlo._startScale;
        else
            _TargetScaleY = _myGlo._startScale;
    }



}
