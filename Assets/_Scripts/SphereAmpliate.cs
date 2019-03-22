using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAmpliate : MonoBehaviour
{
    public int _band;
    public float _bandScaleStrength=10.0f, _EmissionStrength=2.0f;
    public float _leprspeed=0.15f;
    public Color _minColor=Color.black;
    public Color _maxColor = Color.white;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_band>_SimpleSpectrum._OldScale_Y.Length)
              _band = _SimpleSpectrum._OldScale_Y.Length-1;

        Vector3 _newscale= new Vector3(
        _SimpleSpectrum._OldScale_Y[_band],
        _SimpleSpectrum._OldScale_Y[_band], 
        _SimpleSpectrum._OldScale_Y[_band])* _bandScaleStrength;
        transform.localScale = Vector3.Lerp(transform.localScale,_newscale, _leprspeed);


        transform.GetComponent<Renderer>().material.SetVector("_EmissionColor",
      (new Color(Mathf.Clamp(_SimpleSpectrum._OldScale_Y[_band], _minColor.r, _maxColor.r),
                 Mathf.Clamp(_SimpleSpectrum._OldScale_Y[_band], _minColor.g, _maxColor.g),
                 Mathf.Clamp(_SimpleSpectrum._OldScale_Y[_band], _minColor.b, _maxColor.b)))* _EmissionStrength
                 );
        
            

    }
}
