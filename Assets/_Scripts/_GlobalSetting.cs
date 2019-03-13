using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(_GlobalSetting))]
public class _GlobalSetting : MonoBehaviour
{


    public float ForwardScale=70;
    public GameObject Cubes512;

    // Start is called before the first frame update
    void Start()
    {
        RecreateCubes();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void RecreateCubes ()
    {

            if(GameObject.Find("512_Cubes")!=null)
            {
                Destroy(GameObject.Find("512_Cubes"));
            }
            GameObject _new= Instantiate(Cubes512);
            _new.transform.name= "512_Cubes";
           // if (Cubes512.GetComponent<Sc_InstantiateCube>() != null)    Cubes512.GetComponent<Sc_InstantiateCube>().enabled = true;
        

    }
}
