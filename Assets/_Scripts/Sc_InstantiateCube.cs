using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sc_InstantiateCube : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCub = new GameObject[512];


    public float _scale;


    void Start()
    {
        int i=0;
        while (i<_sampleCub.Length)
        {
            GameObject _instanceSampleCub = (GameObject)Instantiate(_sampleCubePrefab);
            _instanceSampleCub.transform.position = this.transform.position;
            _instanceSampleCub.transform.parent = this.transform;
            _instanceSampleCub.name = "Simple" + i;
            this.transform.eulerAngles = new Vector3(0.0f,-360/512f*i,0.0f);
            _instanceSampleCub.transform.position = Vector3.forward * 80;    //先克隆在原地，然後決定好角度之後往前移動
            _sampleCub[i] = _instanceSampleCub;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.eulerAngles+=new Vector3(0.0f,360/60f*Time.deltaTime,0.0f);

        for (int i = 0; i < _sampleCub.Length; i++)
        {
            _sampleCub[i].transform.localScale = new Vector3(_sampleCub[i].transform.localScale.x, (AudioPeer._samples[i]) *_scale, _sampleCub[i].transform.localScale.z);
        }

    }
}
