using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAmpliate : MonoBehaviour
{
    public int _band;
    public float _scale=0;
    [Range(0,15)]
    public float _baseScaleY = 2.0f;
    [Range(0, 2)]
    public float _bandScaleStrength=2.0f, _EmissionStrength=2.0f;
    public float _leprspeed=0.15f;
    public Color _minColor=Color.black;
    public Color _maxColor = Color.white;
    public List<Material> _allMat;
    // Start is called before the first frame update
    void Start()
    {
        if(transform.GetComponent<Renderer>()!=null)
        {
            _allMat.Add(transform.GetComponent<SpriteRenderer>().material);
        }
        else
        {
            int i = 0;
            while(transform.GetChild(i) !=null)
            {
                if (transform.GetChild(i).GetComponent<SpriteRenderer>() != null)
                {
                    _allMat.Add(transform.GetChild(i).GetComponent<SpriteRenderer>().material);
                }
                if (i + 1 < transform.childCount)
                    i++;
                else
                    break;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        _scale = _SimpleSpectrum._OldScale_Y[_band];
        if (_band>_SimpleSpectrum._OldScale_Y.Length)
              _band = _SimpleSpectrum._OldScale_Y.Length-1;

        Vector3 _newscale= new Vector3(
        _SimpleSpectrum._OldScale_Y[_band]+ _baseScaleY,
        _SimpleSpectrum._OldScale_Y[_band]+ _baseScaleY, 
        _SimpleSpectrum._OldScale_Y[_band]+ _baseScaleY) * _bandScaleStrength;
        transform.localScale = Vector3.Lerp(transform.localScale,_newscale, _leprspeed);

        if(_allMat.Count>0)
             for(int i = 0; i < _allMat.Count; i++) { 


                 _allMat[i].SetColor("_Color",
                 (new Color(Mathf.Clamp(_SimpleSpectrum._OldScale_Y[_band], _minColor.r, _maxColor.r),
                 Mathf.Clamp(_SimpleSpectrum._OldScale_Y[_band], _minColor.g, _maxColor.g),
                 Mathf.Clamp(_SimpleSpectrum._OldScale_Y[_band], _minColor.b, _maxColor.b)))* _EmissionStrength
                 );

        }

    }
}
