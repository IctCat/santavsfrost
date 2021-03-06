﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostSpike : ObstacleComponent
{
    public float LaunchVelocity = 10;
    bool _launched;
    Rigidbody2D _rb;
	// Use this for initialization
	void Start () {
        _rb = GetComponent<Rigidbody2D>();
        _rb.simulated = true;
        GameControl.instance.Frost.Skills.SpikeAvailable(this);
	}
	
	// Update is called once per frame
	void Update () {
        if (_launched)
        {
            _rb.AddForce(Vector2.up*(LaunchVelocity*(LaunchVelocity*Random.value+0.1f))/100, ForceMode2D.Impulse);
        }
	}

    public void Launch()
    {
        if (!_launched)
        {
            _rb.isKinematic = false;
            _rb.velocity.Set(0, LaunchVelocity);
            _launched = true;
        }
    }

    public override void Reset()
    {
        if (_rb != null)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 0);
            _rb.isKinematic = true;
            _launched = false;
        }

        base.Reset();
    }
}
