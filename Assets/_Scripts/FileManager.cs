﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TagLib;
using SFB;
using System;

public class FileManager : MonoBehaviour
{
    
   
    public AudioSource _audioSource;
    public Button _playButton;
    public Slider _VolumeSlider;
    private bool _volumeBool = false;
    private bool isPlaying;
    public Canvas _advCanvas;
    // Start is called before the first frame update
    void Start()
    {
        _VolumeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck();});
        _VolumeSlider.value = 0.3f;
        _audioSource.volume = _VolumeSlider.value;
        isPlaying = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

 
/*
    IEnumerator loadMusic(string input_path)
    {
        ///   檢查副檔名
        string _check="";
        int i = input_path.Length-1;
        while (i>0)
        {
            if (input_path[i] == '.')
            {
                    _check += input_path[i];
                break;
            }
            if (input_path[i] > 64 && input_path[i] < 91)
            {
                _check +=Convert.ToChar(input_path[i]+32);
            }
            else if(input_path[i] > 96 && input_path[i] < 123)
                _check += Convert.ToChar(input_path[i]);
            else if(input_path[i] > 47 && input_path[i] < 58)
                _check += Convert.ToChar(input_path[i]);
            i--;
        }
        _check= Reverse(_check);
        ///
        if (_check == ".mp3")
        {
            WWW _music = new WWW(input_path);
            yield return _music;
            // _audioSource.clip = _www.GetAudioClip(false);

            AudioClip _clip = NAudioPlayer.FromMp3Data(_music.bytes);

            _audioSource.clip = _clip;
            _audioSource.Play(); 
        }
        else
        {
            UnityWebRequest _music = UnityWebRequestMultimedia.GetAudioClip(input_path,AudioType.WAV);
            yield return _music.Send();

            AudioClip _clip = DownloadHandlerAudioClip.GetContent(_music);
            _audioSource.clip = _clip;
            _audioSource.Play();
        }
    }*/
    IEnumerator loadMusic(string input_path)
    {
       

       File _newfile = TagLib.File.Create(input_path);
        string _newfileTitle = _newfile.Tag.Title;
        TimeSpan duration = _newfile.Properties.Duration;


        //change title in the file

        _newfile.Mode = File.AccessMode.Read;
       byte[] _newByt = _newfile.ReadBlock((int)_newfile.Length).Data;


        

        AudioClip _clip = NAudioPlayer.FromMp3Data(_newByt);
        _audioSource.clip= _clip;
        _audioSource.Play();

        _newfile.Save();
       
      yield return null;
    }


    public void ButtonOnClick()    //瀏覽檔案...
    {
        var extensions = new[] {
                new ExtensionFilter("支援的檔案","wav" ),
                new ExtensionFilter("All Files", "*" ),
            };

         string _path = WriteResult(StandaloneFileBrowser.OpenFilePanel("Open File","",extensions,false));   //System.Windows.Form
        
         StartCoroutine(loadMusic(_path));
    }


    public string WriteResult(string[] input_path)
    {
        string _path ="";
        if (input_path.Length == 0) return _path;
        foreach (string _text in input_path)
        {
            _path +=_text;
        }
        return _path;
    }

    public void _PlayButton()
    {
        if (isPlaying)
            _audioSource.Pause();
        else
            _audioSource.UnPause();
        isPlaying = !isPlaying;
    }
    public void _SettingButton()
    {
        _advCanvas.enabled = !_advCanvas.enabled;


    }

    public void _VolumeButton()
    {
        _volumeBool = !_volumeBool;

        if (_volumeBool)
            _VolumeSlider.GetComponent<Animator>().Play("Ani_Slider_FlyIn");
        else
            _VolumeSlider.GetComponent<Animator>().Play("Ani_Slider_FlyOut");
    }

    public void ValueChangeCheck()
    {
        _audioSource.volume = _VolumeSlider.value;
    }






    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

}
