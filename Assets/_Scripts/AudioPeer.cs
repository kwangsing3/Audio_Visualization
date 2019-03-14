using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent (typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    public AudioSource _audiosource;
    public static int static_TotalSamples = 512;
    public static int static_SimpleSamples = 8;
    public int _TotalSamples;
    public int _SimpleSamples = 8;

    public static float[] _samples;
    float[] _freBand;
    float[] _frebandbuffer;
    float[] _bufferDecrease ;

    float[] _freqbandHighest ;
    public static float[] _audioband;
    public static float[] _audiobandBuffer;
    public static float _Amplitude=1, _AmplitudeBuffer=1;
    float _AmplitudeHighest=1;

    // Mic input
    public bool _useMic;
    public AudioClip _DefultClip;
    public string _selectedDevice;
    public float _AudioProfile;
    private _GlobalSetting _myGlo;

    private void Awake()
    {
        static_TotalSamples = _TotalSamples;
        static_SimpleSamples = _SimpleSamples;
        static_TotalSamples = Checkfre_Even(static_TotalSamples);


        _myGlo = GameObject.Find("_GlobalSetting").GetComponent<_GlobalSetting>();
    }





    void Start()
    {
        _TotalSamples = static_TotalSamples;
        _samples = new float[static_TotalSamples];
        _freBand = new float[_SimpleSamples];
        _frebandbuffer = new float[_freBand.Length];
        _freqbandHighest = new float[_freBand.Length];
        _bufferDecrease = new float[_freBand.Length];
        _audioband = new float[_SimpleSamples];
        _audiobandBuffer = new float[_SimpleSamples];
        int i = 0;
        while(i<_samples.Length)
        {
            _samples[i] = 0; i++;
        }


        //----------------------------------------------------------------------------------------
        _audiosource = GetComponent<AudioSource>();

        if (_useMic)
        {
            if (Microphone.devices.Length > 0)
            {
                _selectedDevice = Microphone.devices[0].ToString();
                _audiosource.clip = Microphone.Start(_selectedDevice, true, 1, AudioSettings.outputSampleRate);
            }
            else
            {
                _useMic = false;
            }
        }
        else
        {
            _audiosource.clip = _DefultClip;
        }
        AudioInit(_AudioProfile);
        _audiosource.Play();

    }
    public void _useMicrophone()
    {

        _useMic = !_useMic;
        if (_useMic)
        {
            if (Microphone.devices.Length > 0)
            {
                _selectedDevice = Microphone.devices[0].ToString();
                Destroy(_audiosource);

                
                
                 _audiosource = gameObject.AddComponent<AudioSource>();
                _audiosource.volume = 0.3f;
                _audiosource.loop = false;
                _audiosource.clip = Microphone.Start(_selectedDevice, true, 10, AudioSettings.outputSampleRate);
                _audiosource.Play();
            }
            else
            {
                _useMic = false;
                _audiosource.clip = _DefultClip;
            }
        }
        else
        {
            _audiosource.clip = _DefultClip;
        }
       
        _audiosource.Play();
    }



    // Update is called once per frame
    void Update()
    {
        getAudioSpectrum();
        MakeFrequencyBands();
        Bandbuffer();
        CreateAudioBands();
        GetAmplitude();
    }

    void AudioInit(float _aduP)
    {
        for (int i = 0; i < _freqbandHighest.Length;i++)
        {
            _freqbandHighest[i] = _aduP;
        }
    }

    void GetAmplitude()
    {
        float _CurrentAmp = 1;
        float _CurrentAmpBuffer = 1;

        for (int i = 0; i < _audiobandBuffer.Length; i++)
        {
            if (_audioband[i] > 0)
            {
                _CurrentAmp += _audioband[i];
                _CurrentAmpBuffer += _audiobandBuffer[i];
            }
           
        }
        if (_CurrentAmp > _AmplitudeHighest)
            _AmplitudeHighest = _AmplitudeBuffer;

        _Amplitude = _CurrentAmp / _AmplitudeHighest;
        _AmplitudeBuffer = _CurrentAmpBuffer / _AmplitudeHighest;
    }


    void getAudioSpectrum()     //取得光譜
    {

        _audiosource.GetSpectrumData(_samples,0, _myGlo._FFTWindowMode);
       
    }

    void CreateAudioBands()
    {
        for (int i = 0; i < _audiobandBuffer.Length; i++)
        {
            if (_freBand[i] > _freqbandHighest[i])
                _freqbandHighest[i] = _freBand[i];

            _audioband[i] = (_freBand[i]/_freqbandHighest[i]);
            _audiobandBuffer[i] = (_frebandbuffer[i] / _freqbandHighest[i]);
        }
    }
    void Bandbuffer()
    {
        for (int i = 0; i < _freBand.Length; i++)
        {
            if (_freBand[i] > _frebandbuffer[i])
            {
                _frebandbuffer[i] = _freBand[i];
                _bufferDecrease[i] =0.005f;
            }
            if (_freBand[i] < _frebandbuffer[i])
            {
                _frebandbuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f;
            }
        }
    }


    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < _freBand.Length; i++)
        {
            float average = 0;

            int sampleCount = (int)Mathf.Pow(2, i)*2;     // _SimpleSample 的取樣幅度
           
           
           
                for (int j = 0; j < sampleCount; j++)
                {
                    average += _samples[count];
                if (count > _samples.Length - 2)
                    continue;
                else
                    count++;
                }
            


            average /= count;
            _freBand[i] = average*10;
        }
    }


    protected int Checkfre_Even(int index)
    {

        if (index < 64)
            index = 64;
        else if (index > 8192)
            index = 8192;


       while((index&index-1)!=0)// 如果是0，就是2的次方
        {
            index++;
        }
        return index;
    }


}
