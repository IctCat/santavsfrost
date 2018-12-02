using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour {
    public const float INTRO_TIMESTAMP = 6.54f;
    public const float INTRO_END_TIMESTAMP = 8.72f;
    public const float MAIN_LOOP_START = 10.87f;
    public const float SONG_END = 91.5f;
    public AudioClip Song;
    public static int Stage = 0;
    AudioSource _as;
    public float FadeInRate = 0.01f;

    private void Awake()
    {
        _as = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        DontDestroyOnLoad(gameObject);
        _as.clip = Song;
        _as.Play();
        _as.volume = 0.3f;
        StartCoroutine(FadeIn());
	}

    IEnumerator FadeIn()
    {
        while (_as.volume < 0.8f)
        {
            _as.volume += FadeInRate;
            yield return new WaitForEndOfFrame();
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (Stage == 2)
        {
            if(_as.time > SONG_END)
            {
                _as.time = MAIN_LOOP_START;
            }
        } else if (Stage == 1)
        {
            _as.time = INTRO_END_TIMESTAMP;
            Stage = 2;
        } else if (Stage == 0 && _as.time > INTRO_TIMESTAMP)
        {
            _as.time = 0;
        }

    }
}
