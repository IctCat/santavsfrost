using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aurora : MonoBehaviour {
    public bool Showing = false;
    public float Chance = 0.2f;
    public float Tick = 2.0f;
    public float AppearSpeed = 0.1f;
    public float AppearDuration = 0.9f;
    public float MoveSpeed = 0.01f;
    float _chanceTimer = 0.0f;
    SpriteRenderer _r;
    Color _c;
    private void Awake()
    {
        _r = GetComponent<SpriteRenderer>();
        _chanceTimer = Tick;
    }
    // Update is called once per frame
    void Update () {
        _chanceTimer -= Time.deltaTime;
		if(!Showing && _chanceTimer <= 0 && Random.value < Chance)
        {
            Vector3 pos = transform.position;
            pos.x = Random.value * 16.0f;
            transform.position = pos;
            StartCoroutine(ShowAurora());
        }
        if (Showing)
        {
            transform.Translate(Vector3.left * MoveSpeed);
        }
	}

    IEnumerator ShowAurora()
    {
        Showing = true;
        _c = _r.color;

        while(_c.a < 1.0f)
        {
            _c.a += AppearSpeed;
            _r.color = _c;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(AppearDuration);
        while(_c.a > 0)
        {
            _c.a -= AppearSpeed;
            _r.color = _c;
            yield return new WaitForEndOfFrame();
        }
        _chanceTimer = Tick;
        Showing = false;
    }
}
