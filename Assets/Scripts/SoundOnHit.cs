using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundOnHit : MonoBehaviour {
    public AudioClip Clip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlaySound();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlaySound();
    }

    void PlaySound()
    {
        SFXPlayer.Instance.Play(Clip);
    }
}
