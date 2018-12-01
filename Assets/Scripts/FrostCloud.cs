﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostCloud : MonoBehaviour {
    float _hover1 = 0;
    float _hover2 = 0;
    public float HoverSpeed = 0.01f;
    public bool MoodAngry = false;
    public Transform FrostParent;
    Vector3 _offset = new Vector3();
    public Transform AngryCloud;
    public Transform NormalCloud;
    
    // Update is called once per frame
    void Update () {
        _hover1 += Random.value * HoverSpeed;
        _hover1 = _hover1 % Mathf.PI;
        _hover2 += Random.value * HoverSpeed;
        _hover2 = _hover2 % Mathf.PI;
        _offset.x = Mathf.Sin(_hover1*2);
        _offset.y = Mathf.Sin(_hover2);
        FrostParent.localPosition = _offset;

        if (MoodAngry && !AngryCloud.gameObject.activeSelf)
        {
            AngryCloud.gameObject.SetActive(true);
            NormalCloud.gameObject.SetActive(false);
        } else if (!MoodAngry && !NormalCloud.gameObject.activeSelf)
        {
            AngryCloud.gameObject.SetActive(false);
            NormalCloud.gameObject.SetActive(true);
        }
	}
}