﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostSkills : MonoBehaviour {
    public static FrostSkills Instance;
    public bool Blizzard = false;
    public bool ManualLaunch = false;

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

    public float ThrowForce = 30.0f;
    public GameObject SantaPlayer;
    public float BlizzardInterval = 0.9f;
    public float BlizzardDuration = 4.6f;
    Rigidbody2D[] _sleighBodies;
    float _blizzardTimer = 0;
    float _blizzardDurationTimer = 0;
    private void Awake()
    {
        Instance = this;
        SantaPlayer = GameObject.FindGameObjectWithTag("SantaPlayer");
        _sleighBodies = SantaPlayer.GetComponentsInChildren<Rigidbody2D>();
    }
    // Use this for initialization
    void Start () {
		
	}
	
    public void LaunchSpikes()
    {
        foreach(FrostSpike spike in _spikes)
        {
            if(spike != null) spike.Launch();
        }
        _spikes.Clear();
    }

	// Update is called once per frame
	void Update () {
        if (Blizzard)
        {
            _blizzardTimer -= Time.deltaTime;
            _blizzardDurationTimer += Time.deltaTime;

            if (_blizzardTimer < 0)
            {
                foreach (Rigidbody2D rb in _sleighBodies)
                {
                    rb.AddForce(Vector2.up * (UnityEngine.Random.value * 2 - 1) * ThrowForce, ForceMode2D.Impulse);
                }
                _blizzardTimer = BlizzardInterval;
            }

            if(_blizzardDurationTimer > BlizzardDuration)
            {
                _blizzardDurationTimer = 0;
                Blizzard = false;
            }
        }
        if(ManualLaunch)
        {
            LaunchSpikes();
            ManualLaunch = false;
        }
	}
}
