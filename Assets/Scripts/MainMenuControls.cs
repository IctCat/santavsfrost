using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour {
    PlayerInput _pi;
	// Use this for initialization
	void Start () {
        _pi = new PlayerInput(PlayerIndex.One, PlayerInput.KeyboardLayout.NumberRow);
    }
	
	// Update is called once per frame
	void Update ()
    {
        _pi.UpdateInput();
        if (_pi.GetButtonDown(PlayerInput.Button.A) && MusicPlayer.Stage == 0)
        {
            MusicPlayer.Stage = 1;
            SceneManager.LoadScene("Level 1");
        }

    }
}
