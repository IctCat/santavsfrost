using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class FrostComponent : MonoBehaviour
{
    private PlayerInput PlayerInput;

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
    }

    public void Update()
    {
        this.PlayerInput.UpdateInput();
    }

    public void FixedUpdate()
    {
        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.A))
        {
            
        }

        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.B))
        {
            
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
