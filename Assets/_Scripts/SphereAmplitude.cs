using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAmplitude : MonoBehaviour
{


    public int _band;
    public float _startScale, _scaleMultiplier;
    public bool Buffer;
   
   
    
    Material _material;
    // Start is called before the first frame update
    void Start()
    {
        _material =GetComponent<MeshRenderer>().material;
        if (_material == null)
            print("Not found");

        

    }

    // Update is called once per frame
    void Update()
    {
    if(Buffer&&AudioPeer._AmplitudeBuffer>0)
        transform.localScale = new Vector3((AudioPeer._AmplitudeBuffer * _scaleMultiplier) + _startScale, (AudioPeer._AmplitudeBuffer * _scaleMultiplier) + _startScale, (AudioPeer._AmplitudeBuffer * _scaleMultiplier) + _startScale);
 
        Color _color=new Color(AudioPeer._audiobandBuffer[_band], AudioPeer._audiobandBuffer[_band], AudioPeer._audiobandBuffer[_band]);


        _material.SetColor("_EmissionColor", _color);
    }





}
