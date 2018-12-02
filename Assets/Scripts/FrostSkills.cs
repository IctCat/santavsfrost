using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostSkills : MonoBehaviour {
    public bool Blizzard = false;
    public bool ManualLaunch = false;
    public AudioClip BlizzardSound;

    bool _soundPlayed = false;

    List<FrostSpike> _spikes = new List<FrostSpike>();

    public void SpikeAvailable(FrostSpike frostSpike)
    {
        _spikes.Add(frostSpike);
    }

    public void RemoveSpike(FrostSpike spike)
    {
        if (_spikes.Contains(spike))
        {
            _spikes.Remove(spike);
        }
    }

    float ThrowForce = 5.0f;
    public float BlizzardInterval = 0.9f;
    public float BlizzardDuration = 4.6f;
    public float BlizzardCooldown = 10.0f;
    float _blizzardTimer = 0;
    float _blizzardDurationTimer = 0;
    float _blizzardCooldownTimer = 0;
    bool _blizzardAvailable = true;

    // Use this for initialization
    void Start () {
		
	}
	
    public void LaunchSpikes()
    {
        foreach(FrostSpike spike in _spikes)
        {
            if ((spike != null) && (spike.transform.position.x < 12))
            {
                spike.Launch();
            }
        }
    }

    void EnableBlizzard()
    {
        Debug.Log("Blizzard enabled!");
        _blizzardAvailable = true;
    }

	// Update is called once per frame
	void Update () {

        if (Blizzard)
        {
            FrostCloud.MoodAngry = true;
            if (!_soundPlayed)
            {
                SFXPlayer.Instance.Play(BlizzardSound);
                _soundPlayed = true;
            }
            _blizzardTimer -= Time.deltaTime;
            _blizzardDurationTimer += Time.deltaTime;

            if (_blizzardTimer < 0)
            {
                foreach (Rigidbody2D rb in GameControl.instance.Santa.SleighBodies)
                {
                    if (rb != null)
                    {
                        rb.AddForce(Vector2.up * (UnityEngine.Random.value * 0.5f + 0.5f) * (UnityEngine.Random.value < 0.5f ? 1.0f : -1.0f) * ThrowForce, ForceMode2D.Impulse);
                    }
                }
                _blizzardTimer = BlizzardInterval;
            }

            if(_blizzardDurationTimer > BlizzardDuration)
            {
                _blizzardDurationTimer = 0;
                Blizzard = false;
                _soundPlayed = false;
                FrostCloud.MoodAngry = false;
                Invoke("EnableBlizzard", BlizzardCooldown);
            }
        }
        if(ManualLaunch)
        {
            LaunchSpikes();
            ManualLaunch = false;
        }
	}

    public void StartBlizzard()
    {
        if (_blizzardAvailable)
        {
            Blizzard = true;
            _blizzardAvailable = false;
        }
    }
}
