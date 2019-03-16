using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocalScale : MonoBehaviour
{


    public int _band;
    public float _startScale, _scaleMultiplier;
    public bool Buffer;
    public Color _currentColor;
    float _CacheBand = 0; 
   
    private _GlobalSetting _myGlo;

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
        if (Buffer && AudioPeer._audiobandBuffer[_band] > 0)
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._audiobandBuffer[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
        Color _Finalcolor = Color.white;
       

        switch (Random.Range(1,3))
        {
            case 1:
                _Finalcolor = new Color(
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * 0.8f - _myGlo._AddThemeColor.r,
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * 0.8f - _myGlo._AddThemeColor.g,
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * 0.8f - _myGlo._AddThemeColor.b);
                break;
            case 2:
                _Finalcolor = new Color(
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * 0.8f - _myGlo._AddThemeColor2.r,
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * 0.8f - _myGlo._AddThemeColor2.g,
                     AudioPeer._audiobandBuffer[_band] * _myGlo._BaseLightScale * 0.8f - _myGlo._AddThemeColor2.b);
                break;
        }
        if(_CacheBand< AudioPeer._audiobandBuffer[_band])
        _material.SetVector("_EmissionColor", _myGlo._MainBackColor - (_Finalcolor * _myGlo.EmissionStrength));
        _CacheBand = AudioPeer._audiobandBuffer[_band];

    }





}
