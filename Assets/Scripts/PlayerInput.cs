using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class PlayerInput
{
    public enum Button {
        A,
        B,
        X,
        Y,
        Count
    }

    public enum Stick
    {
        Left,
        Right
    }

    public enum KeyboardLayout
    {
        NumberRow,
        NumPad
    }

    private class InputButtonState
    {
        public Button Button;
        public bool Down;
        public bool Up;
        public bool Pressed;

        public InputButtonState(Button button)
        {
            this.Button = button;
            this.Down = false;
            this.Up = false;
            this.Pressed = false;
        }

        public void Reset()
        {
            this.Down = false;
            this.Up = false;
        }
    }

    private PlayerIndex PlayerIndex;
    private KeyboardLayout Layout;

    private InputButtonState[] InputButtonStates;

    private const string InputHorizontal = "Horizontal";
    private const string InputVertical = "Vertical";
    private const string InputSanta1 = "Santa 1";
    private const string InputSanta2 = "Santa 2";
    private const string InputSanta3 = "Santa 3";
    private const string InputSanta4 = "Santa 4";
    private const string InputFrost1 = "Frost 1";
    private const string InputFrost2 = "Frost 2";
    private const string InputFrost3 = "Frost 3";
    private const string InputFrost4 = "Frost 4";

    public PlayerInput(PlayerIndex index, KeyboardLayout layout)
    {
        this.PlayerIndex = index;
        this.Layout = layout;
        this.Initialize();
    }

    public void Initialize()
    {
        this.InputButtonStates = new InputButtonState[(int)Button.Count];

        for (int i = 0; i < this.InputButtonStates.Length; i++)
        {
            this.InputButtonStates[i] = new InputButtonState((Button)i);
        }
        
        this.ResetInput();
    }

    public void UpdateInput()
    {
        GamePadState state = GamePad.GetState(this.PlayerIndex);

        for (int i = 0; i < this.InputButtonStates.Length; i++)
        {
            InputButtonState buttonState = this.InputButtonStates[i];
            bool pressed = false;

            switch (this.InputButtonStates[i].Button)
            {
                case Button.A:
                    pressed = (state.Buttons.A == ButtonState.Pressed);
                    switch (this.Layout)
                    {
                        case KeyboardLayout.NumberRow: pressed = pressed || Input.GetButton(PlayerInput.InputSanta1); break;
                        case KeyboardLayout.NumPad: pressed = pressed || Input.GetButton(PlayerInput.InputFrost1); break;
                    }
                    break;
                case Button.B:
                    pressed = (state.Buttons.B == ButtonState.Pressed);
                    switch (this.Layout)
                    {
                        case KeyboardLayout.NumberRow: pressed = pressed || Input.GetButton(PlayerInput.InputSanta2); break;
                        case KeyboardLayout.NumPad: pressed = pressed || Input.GetButton(PlayerInput.InputFrost2); break;
                    }
                    break;
                case Button.X:
                    pressed = (state.Buttons.X == ButtonState.Pressed);
                    switch (this.Layout)
                    {
                        case KeyboardLayout.NumberRow: pressed = pressed || Input.GetButton(PlayerInput.InputSanta3); break;
                        case KeyboardLayout.NumPad: pressed = pressed || Input.GetButton(PlayerInput.InputFrost3); break;
                    }
                    break;
                case Button.Y:
                    pressed = (state.Buttons.Y == ButtonState.Pressed);
                    switch (this.Layout)
                    {
                        case KeyboardLayout.NumberRow: pressed = pressed || Input.GetButton(PlayerInput.InputSanta4); break;
                        case KeyboardLayout.NumPad: pressed = pressed || Input.GetButton(PlayerInput.InputFrost4); break;
                    }
                    break;
            }

            if (pressed)
            {
                if (!buttonState.Pressed)
                {
                    buttonState.Down = true;
                }
            }
            else
            {
                if (buttonState.Pressed)
                {
                    buttonState.Up = true;
                }
            }

            buttonState.Pressed = pressed;
        }
    }

    public void ResetInput()
    {
        for (int i = 0; i < this.InputButtonStates.Length; i++)
        {
            this.InputButtonStates[i].Reset();
        }
    }

    public bool GetButton(Button button)
    {
        return this.InputButtonStates[(int)button].Pressed;
    }

    public bool GetButtonDown(Button button)
    {
        return this.InputButtonStates[(int)button].Down;
    }

    public bool GetButtonUp(Button button)
    {
        return this.InputButtonStates[(int)button].Up;
    }

    public float GetHorizontalAxis(Stick stick)
    {
        float value = Input.GetAxis(PlayerInput.InputHorizontal);

        GamePadState state = GamePad.GetState(this.PlayerIndex);

        switch (stick)
        {
            case Stick.Left: value += state.ThumbSticks.Left.X; break;
            case Stick.Right: value += state.ThumbSticks.Right.X; break;
        }

        return Mathf.Clamp(value, -1, 1);
    }

    public float GetVerticalAxis(Stick stick)
    {
        float value = Input.GetAxis(PlayerInput.InputVertical);

        GamePadState state = GamePad.GetState(this.PlayerIndex);

        switch (stick)
        {
            case Stick.Left: value += state.ThumbSticks.Left.Y; break;
            case Stick.Right: value += state.ThumbSticks.Right.Y; break;
        }

        return Mathf.Clamp(value, -1, 1);
    }
}
