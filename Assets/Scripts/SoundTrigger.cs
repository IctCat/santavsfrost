using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTrigger : MonoBehaviour {
    public AudioClip Clip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Sound triggered: " + Clip.name);
        if(collision.transform.parent.tag == "SantaPlayer")
        {
            SFXPlayer.Instance.Play(Clip);
            Destroy(gameObject);
        }
    }
}
