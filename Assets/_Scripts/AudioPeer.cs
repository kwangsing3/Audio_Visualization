using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent (typeof(AudioSource))]
public class AudioPeer : MonoBehaviour
{
    public AudioSource _audiosource;
   public static float[] _samples=new float[512];
    float[] _freBand =new float[8];
    float[] _bandbuffer = new float[8];
    float[] _bufferDecrease = new float[8];

    float[] _freqbandHighest = new float[8];
    public static float[] _audioband =new float[8];
    public static float[] _audiobandBuffer = new float[8];
    public static float _Amplitude=1, _AmplitudeBuffer=1;
    float _AmplitudeHighest=1;

    // Mic input
    public bool _useMic;
    public AudioClip _DefultClip;
    public string _selectedDevice;

    void Start()
    {
       
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


    void GetAmplitude()
    {
        float _CurrentAmp = 1;
        float _CurrentAmpBuffer = 1;

        for (int i = 0; i < 8; i++)
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


    void getAudioSpectrum()
    {
        _audiosource.GetSpectrumData(_samples,0,FFTWindow.Blackman);
    }

    void CreateAudioBands()
    {
        for (int i = 0;i< 8; i++)
        {
            if (_freBand[i] > _freqbandHighest[i])
                _freqbandHighest[i] = _freBand[i];

            _audioband[i] = (_freBand[i]/_freqbandHighest[i]);
            _audiobandBuffer[i] = (_bandbuffer[i] / _freqbandHighest[i]);
        }
    }
    void Bandbuffer()
    {
        for (int i = 0; i < 8; i++)
        {
            if (_freBand[i] > _bandbuffer[i])
            {
                _bandbuffer[i] = _freBand[i];
                _bufferDecrease[i] =0.005f;
            }
            if (_freBand[i] < _bandbuffer[i])
            {
                _bandbuffer[i] -= _bufferDecrease[i];
                _bufferDecrease[i] *= 1.2f;
            }
        }
    }


    void MakeFrequencyBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;

            int sampleCount = (int)Mathf.Pow(2, i)*2;
            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += _samples[count]*(count+1);
                count++;
            }

            average /= count;
            _freBand[i] = average*10;
        }
    }


}
