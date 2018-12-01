using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPlayer : MonoBehaviour {
    public static SFXPlayer Instance;
    AudioSource _src;
    private void Awake()
    {
        Instance = this;
        _src = GetComponent<AudioSource>();
    }
    public void Play(AudioClip clp)
    {
        _src.PlayOneShot(clp);
    }
}
