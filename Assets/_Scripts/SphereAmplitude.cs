using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAmplitude : MonoBehaviour
{


    public int _band;
    public float _startScale, _scaleMultiplier;
    public bool Buffer;

    public float[] CurrentAmplitude;
    public float _EmissionStrength = 5;
    public Color _color;
    Material _material;
    public float _BandBuffer;
    public bool _Scale = true;
    // Start is called before the first frame update
    void Start()
    {


        _material =GetComponent<MeshRenderer>().material;
        if (_material == null)
            print("Not found");
       

        CurrentAmplitude = new float[AudioPeer.static_SimpleSamples];

    }

    // Update is called once per frame
    void Update()
    {
        _BandBuffer = AudioPeer._audiobandBuffer[_band];
     if (Buffer&&AudioPeer._AmplitudeBuffer>0&& _Scale)
        transform.localScale = new Vector3((AudioPeer._AmplitudeBuffer * _scaleMultiplier) + _startScale, (AudioPeer._AmplitudeBuffer * _scaleMultiplier) + _startScale, (AudioPeer._AmplitudeBuffer * _scaleMultiplier) + _startScale);
        
       Color _Finalcolor=new Color(
           AudioPeer._audiobandBuffer[_band]*_EmissionStrength, 
           AudioPeer._audiobandBuffer[_band]* _EmissionStrength, 
           AudioPeer._audiobandBuffer[_band]* _EmissionStrength);
        for(int i=0;i<CurrentAmplitude.Length;i++)
        {
            CurrentAmplitude[i] = AudioPeer._audiobandBuffer[i];
        }
        if(_material!=null)
            _material.SetVector("_EmissionColor", _color + _Finalcolor );
    }





}
