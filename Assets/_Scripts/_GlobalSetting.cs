using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;


[RequireComponent(typeof(_GlobalSetting))]
public class _GlobalSetting : MonoBehaviour
{


    public float ForwardScale = 70;
    public GameObject Cubes512;
    Camera _camera;
    //public FFTWindow _FFTWindowMode;
    public enum _Theme
    { 
        Theme_1,Theme_2,Theme_3
    }
    [HideInInspector]
    public _Theme _CurrentTheme;
    public  FFTWindow _FFTWindowMode;



    private void Awake()
    {
        _camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }


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

    public void SwitchTheme()
    {

        switch(_CurrentTheme)
        {
            case _Theme.Theme_1:
                print("Switch Theme to :"+_CurrentTheme);
                break;
            case _Theme.Theme_2:
                print("Switch Theme to :"+ _CurrentTheme);
                break;
            case _Theme.Theme_3:
                print("Switch Theme to :"+ _CurrentTheme);
                break;
            default:
                print("_GlobalSetting設定錯啦（笑)");
                break;

        }
    }





}
