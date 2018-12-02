using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class FrostComponent : MonoBehaviour
{
    private PlayerInput PlayerInput;

    public FrostSkills Skills { get; private set; }

    public void Start()
    {
        if (GameControl.Player1Santa)
        {
            this.PlayerInput = new PlayerInput(PlayerIndex.Two, PlayerInput.KeyboardLayout.NumPad);
        }
        else
        {
            this.PlayerInput = new PlayerInput(PlayerIndex.One, PlayerInput.KeyboardLayout.NumberRow);
        }

        GameControl.instance.Frost = this;

        this.Skills = this.GetComponent<FrostSkills>();
    }

    public void Update()
    {
        this.PlayerInput.UpdateInput();
    }

    public void FixedUpdate()
    {
        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.Start)) 
        {
            GameControl.instance.ReturnToMenu();
            return;
        }

        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.A))
        {
            if (GameControl.instance.gameOver)
            {
                GameControl.instance.RestartRound();
                return;
            }
            else
            {
                this.Skills.LaunchSpikes();
            }
        }

        if (GameControl.instance.gameOver)
        {
            return;
        }

        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.B))
        {
            this.Skills.StartBlizzard();
        }

        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.X))
        {
            
        }

        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.Y))
        {
            
        }

        // Reset input
        this.PlayerInput.ResetInput();
    }
}
