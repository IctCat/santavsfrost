using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingStaticObject : MonoBehaviour {
    Renderer _r;
    public Vector2 ScrollSpeed = Vector2.right * 0.1f;

    private void Awake()
    {
        _r = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update () {
        _r.material.mainTextureOffset += ScrollSpeed / 1000.0f;
	}
}
