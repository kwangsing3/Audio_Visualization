using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalScale : MonoBehaviour
{


    public int _band;
    public float _startScale, _scaleMultiplier;
    public bool Buffer;
    public Color _currentColor;
    public float _BaseLightScale ;
    
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
        if (Buffer && AudioPeer._audiobandBuffer[_band] > 0)
            transform.localScale = new Vector3(transform.localScale.x, (AudioPeer._audiobandBuffer[_band] * _scaleMultiplier) + _startScale, transform.localScale.z);
 
        Color _color=new Color(
            AudioPeer._audiobandBuffer[_band]* _currentColor.r* PickColor._lightscale* _BaseLightScale, 
            AudioPeer._audiobandBuffer[_band]* _currentColor.g* PickColor._lightscale* _BaseLightScale, 
            AudioPeer._audiobandBuffer[_band]* _currentColor.b* PickColor._lightscale* _BaseLightScale);

        _material.SetColor("_EmissionColor", _color);
    }





}
