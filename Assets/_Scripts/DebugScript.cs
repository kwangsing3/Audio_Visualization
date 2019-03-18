using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScript : MonoBehaviour
{
    Text _BandBUffer_Text;
    public float[] _bandbuffer;
    // Start is called before the first frame update
    private void Awake()
    {
        if(transform.Find("Text").GetComponent<Text>()!=null)
        _BandBUffer_Text = transform.Find("Text").GetComponent<Text>();  
    }
    void Start()
    {
        _bandbuffer = new float[AudioPeer.static_SimpleSamples];


    }

    // Update is called once per frame
    void Update()
    {
        _BandBUffer_Text.text = "";
        if (_BandBUffer_Text!=null)
        { 
            for(int i=0;i<AudioPeer.static_SimpleSamples;i++)
            {
                _BandBUffer_Text.text += "Band"+i.ToString()+" :"+((int)(AudioPeer._audiobandBuffer[i]*10000)).ToString()+"\n";
                _bandbuffer[i] = AudioPeer._audiobandBuffer[i];
            }
        }
    }



}
