using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _SimpleSpectrum : MonoBehaviour
{
    public bool isEnabled = true;
    public enum SourceType
    { 
         AudioSource,AudioListener,MicroophoneInput,SteroMix,Custpm
    }

    #region SAMPLING PROPERTIES
    public SourceType _sourcetype = SourceType.AudioSource;
    public AudioSource _audiosource;

    [Tooltip("Sample採樣的數量，一定要是2的次方")]
    public int _numOfSamples = 256;


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        if (_audiosource == null && _sourcetype == SourceType.AudioSource)
            Debug.LogError("沒有或是沒有找到AudioSource，請重新擺放一個");
        RebuildSpectrum();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    float[] _spectrum;
    public void RebuildSpectrum()
    {

        // clear child first
        int _childscount =transform.childCount;
        for(int i=0;i<_childscount;i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        _numOfSamples = Mathf.ClosestPowerOfTwo(_numOfSamples);

        // 重置相關陣列
        _spectrum = new float[_numOfSamples];




    }
}
