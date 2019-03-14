using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAmplitude : MonoBehaviour
{


    public int _band;
    public float _startScale, _scaleMultiplier;
    public bool Buffer;

    public float[] CurrentAmplitude;
    public Color _color;
    Material _material;
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
     if(Buffer&&AudioPeer._AmplitudeBuffer>0)
        transform.localScale = new Vector3((AudioPeer._AmplitudeBuffer * _scaleMultiplier) + _startScale, (AudioPeer._AmplitudeBuffer * _scaleMultiplier) + _startScale, (AudioPeer._AmplitudeBuffer * _scaleMultiplier) + _startScale);
        
       Color _Finalcolor=new Color(AudioPeer._audiobandBuffer[_band]*_color.r, AudioPeer._audiobandBuffer[_band] * _color.g, AudioPeer._audiobandBuffer[_band] * _color.b);
        for(int i=0;i<CurrentAmplitude.Length;i++)
        {
            CurrentAmplitude[i] = AudioPeer._audiobandBuffer[i];
        }
        if(_material!=null)
            _material.SetVector("_EmissionColor", _Finalcolor * 5f);
    }





}
