using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;
using UnityEngine.SceneManagement;

public class MainMenuControls : MonoBehaviour {
    public RectTransform Instructions;
    public RectTransform Credits;
    public RectTransform PressToStart;
    int _stage = 0;
    PlayerInput _pi;
	// Use this for initialization
	void Start () {
        _pi = new PlayerInput(PlayerIndex.One, PlayerInput.KeyboardLayout.NumberRow);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
    }
    void Update ()
    {
        _pi.UpdateInput();
        if (_pi.GetButtonDown(PlayerInput.Button.A))
        {
            switch (_stage)
            {
                case 0:
                    PressToStart.gameObject.SetActive(false);
                    Instructions.gameObject.SetActive(true);
                    _stage = 1;
                    break;
                case 1:
                    Instructions.gameObject.SetActive(false);
                    Credits.gameObject.SetActive(true);
                    _stage = 2;
                    break;
                case 2:
                    Credits.gameObject.SetActive(false);
                    MusicPlayer.Stage = 1;
                    SceneManager.LoadScene("Level 1");
                    _stage = 3;
                    break;
            }
        }
        _pi.ResetInput();

    }
}
