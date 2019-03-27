using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InpuManager : MonoBehaviour
{
    private bool MouseIsMoving = false;
    private float _EventTimer = 5f;
    _GlobalSetting _glo;
    // Start is called before the first frame update
    void Start()
    {
        _glo = GameObject.Find("_GlobalSetting").GetComponent<_GlobalSetting>();
    }

    private void FixedUpdate()
    {
        if(Input.GetAxis("Mouse X") >0 || Input.GetAxis("Mouse Y")>0)
        {
            MouseIsMoving = true;
            _EventTimer = 5f;
                
      
            _UIFadeIn();

        }
        if (_EventTimer > 0)
        {
            _EventTimer -= Time.deltaTime * 1.5f;
        }
        else if(MouseIsMoving)
        {
            MouseIsMoving = false;
            _UIFadeOut();
        }



    }

    void _UIFadeIn()
    {
        GameObject.Find("Btn_Setting").GetComponent<Animator>().Play("Ani_FadeIn");
        GameObject.Find("Btn_Volume").GetComponent<Animator>().Play("Ani_FadeIn");
        GameObject.Find("Btn_Start").GetComponent<Animator>().Play("Ani_FadeIn");
    }
    void _UIFadeOut()
    {
        GameObject.Find("Btn_Setting").GetComponent<Animator>().Play("Ani_FadeOut");
        GameObject.Find("Btn_Volume").GetComponent<Animator>().Play("Ani_FadeOut");
        GameObject.Find("Btn_Start").GetComponent<Animator>().Play("Ani_FadeOut");
    }


}
