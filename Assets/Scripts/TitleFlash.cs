using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleFlash : MonoBehaviour {
    public float FlashSpeed = 2.0f;
    Image _img;
    Color _c;
    private void Awake()
    {
        _img = GetComponent<Image>();
        _c = Color.white;
    }
    // Update is called once per frame
    void Update () {
        _c.a = Mathf.Pow(Mathf.Cos(Time.time * FlashSpeed), 2);        
        _img.color = _c;
	}
}
