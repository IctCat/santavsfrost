using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {
    static SFXPlayer _instance;
    public static SFXPlayer Instance
    {
        get
        {
            if (_instance == null)
            {
                Camera.main.GetComponent<SFXPlayer>();
            }
            return _instance;
        }
        set { _instance = value; }
    }
    AudioSource _src;
    private void Start()
    {
        Instance = this;
        _src = GetComponent<AudioSource>();
    }
    public void Play(AudioClip clp)
    {
        if(_src != null) _src.PlayOneShot(clp);
    }
}
