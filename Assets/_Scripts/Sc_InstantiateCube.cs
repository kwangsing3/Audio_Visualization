using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sc_InstantiateCube : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject _sampleCubePrefab;
    GameObject[] _sampleCub= new GameObject[AudioPeer.static_TotalSamples];
    public float RotateSpeed=0;

    public float _Bandscale;
    _GlobalSetting _GlobalS;
    GameObject _MainPivot;


    void Start()
    {
         _GlobalS = GameObject.Find("_GlobalSetting").GetComponent<_GlobalSetting>();
         _MainPivot = this.transform.GetChild(0).gameObject;

        CreateCubs();

        _MainPivot.transform.Rotate(90,0,0);
        transform.rotation =new Quaternion(0,0,0,0);
    }

    // Update is called once per frame
    void Update()
    {

       transform.eulerAngles += new Vector3(0, 0, -Time.deltaTime * RotateSpeed);

            for (int i = 0; i < _sampleCub.Length; i++)
            {
                _sampleCub[i].transform.localScale = new Vector3(_sampleCub[i].transform.localScale.x, (AudioPeer._samples[i]) * _Bandscale, _sampleCub[i].transform.localScale.z);
            }
        
    }
    void CreateCubs()
    {

            int i = 0;
            while (i < _sampleCub.Length)
            {
                GameObject _instanceSampleCub = (GameObject)Instantiate(_sampleCubePrefab);
                _instanceSampleCub.transform.position = this.transform.position;
                _instanceSampleCub.transform.parent =  _MainPivot.transform;
                _instanceSampleCub.name = "Simple" + i;
                this.transform.eulerAngles = new Vector3(0.0f, -(360.0f/_sampleCub.Length)*i, 0.0f);
                _instanceSampleCub.transform.position = Vector3.forward * _GlobalS.ForwardScale;    //先克隆在原地，然後決定好角度之後往前移動
                _instanceSampleCub.transform.eulerAngles = new Vector3(90f, 0.0f, 0.0f);
                _sampleCub[i] = _instanceSampleCub;
                i++;
            }
        
   
    }

}
