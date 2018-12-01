﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SantaComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject ReindeerPrefab;

    [SerializeField]
    private int InitialReindeerCount = 4;

    [SerializeField]
    private float HorizontalMovementRange = 3;

    [System.NonSerialized]
    public PlayerInput PlayerInput;

    private Transform Transform;
    private Vector2 TargetPosition;

    private SleighComponent Sleigh;
    private List<ReindeerComponent> Reindeers;

    public void Start()
    {
        this.PlayerInput = new PlayerInput();

        this.InitialReindeerCount = Mathf.Clamp(this.InitialReindeerCount, 1, 4);

        this.Transform = this.GetComponent<Transform>();
        this.TargetPosition = Vector2.zero;
        this.Sleigh = this.GetComponentInChildren<SleighComponent>();
        this.Sleigh.Initialize();

        this.Reindeers = new List<ReindeerComponent>(this.InitialReindeerCount);

        for (int i = 0; i < this.InitialReindeerCount; i++)
        {
            GameObject go = Object.Instantiate<GameObject>(this.ReindeerPrefab, this.Transform, false);
            go.transform.localPosition = this.Sleigh.transform.localPosition + new Vector3(i + 2, 0);
            ReindeerComponent reindeer = go.GetComponent<ReindeerComponent>();
            reindeer.Initialize();

            // Connect this reindeer to the previous reindeer.
            if (i > 0)
            {
                reindeer.SpringJoint.connectedBody = this.Reindeers[i - 1].Rigidbody;
            }

            this.Reindeers.Add(reindeer);
        }

        // Connect the first reindeer to the sleigh.
        this.Reindeers[0].SpringJoint.connectedBody = this.Sleigh.Rigidbody;
    }

    public void Update()
    {
        this.PlayerInput.UpdateInput();
    }

    public void FixedUpdate()
    {
        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.A))
        {
            this.ActiveReindeerJump(0);
        }

        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.B))
        {
            this.ActiveReindeerJump(1);
        }

        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.X))
        {
            this.ActiveReindeerJump(2);
        }

        if (this.PlayerInput.GetButtonDown(PlayerInput.Button.Y))
        {
            this.ActiveReindeerJump(3);
        }

        // Horizontal movement
        float acceleration = 5f;
        float x = this.PlayerInput.GetHorizontalAxis(PlayerInput.Stick.Left);

        Vector2 targetTarget = new Vector2(x, 0);
        this.TargetPosition = Vector2.MoveTowards(this.TargetPosition, targetTarget, acceleration * Time.fixedDeltaTime);

        float maxSpeed = 0.5f;
        float speed = Mathf.Min(maxSpeed, maxSpeed * Vector2.Distance(this.Transform.position, this.TargetPosition));

        Vector2 target = this.HorizontalMovementRange * this.TargetPosition;

        this.Transform.position = Vector2.MoveTowards(this.Transform.position, target, speed * Time.fixedDeltaTime);

        // Vertical force (downwards)
        float y = this.PlayerInput.GetVerticalAxis(PlayerInput.Stick.Left);

        if (y < 0)
        {
            this.Sleigh.AddVerticalForce(y * Time.fixedDeltaTime);
        }

        // Reset input
        this.PlayerInput.ResetInput();
    }

    private void ActiveReindeerJump(int index)
    {
        if ((index >= 0) && (index < this.Reindeers.Count))
        {
            this.Reindeers[index].Jump();
        }
    }
}
