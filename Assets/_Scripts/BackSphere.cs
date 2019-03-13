using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackSphere : MonoBehaviour
{
    public GameObject _BackSpherePrefab;
    public Transform[] _Point_Pos;
    public float _Spawnlimit=0.1f;
    public  bool[] CantSpawn;
    public float _WaitforSecond;
    float[] _bandChache;

    // Start is called before the first frame update
    void Start()
    {
        CantSpawn = new bool[8];
        _bandChache = new float[8];

    }

    IEnumerator WaitToSpawn(int index)
    {
        CantSpawn[index] = true;
        yield return new WaitForSeconds(_WaitforSecond);
        CantSpawn[index] = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        for (int i = 0; i < (AudioPeer._audiobandBuffer.Length)*2; i=i+2)
        {
            int j = i >= 8 ? i % 8 : i;


          //  if ( AudioPeer._audioband[j] > _bandChache[j])
            //{
                if (AudioPeer._audioband[j] > _Spawnlimit)
                {
                    GameObject _SpawnSphere = Instantiate(_BackSpherePrefab, _Point_Pos[i]);
                    GameObject _SpawnSphere1 = Instantiate(_BackSpherePrefab, _Point_Pos[i+1]);
                    _SpawnSphere.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",PickColor._Color3);
                    _SpawnSphere1.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", PickColor._Color4);
                    _SpawnSphere.transform.SetParent(_Point_Pos[i]);
                    _SpawnSphere1.transform.SetParent(_Point_Pos[i+1]);

                    Destroy(_SpawnSphere, 3f); Destroy(_SpawnSphere1, 3f);
                    
                }
               
          //  }
          //  StartCoroutine(WaitToSpawn(j));
            _bandChache[j] = AudioPeer._audioband[j];
        }
        
      
    }
}
