﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class SantaComponent : MonoBehaviour
{
    [SerializeField]
    private GameObject ReindeerPrefab;

    [SerializeField]
    private int InitialReindeerCount = 4;

    [SerializeField]
    private float HorizontalMovementRange = 3;

    [SerializeField]
    private float HorizontalAcceleration = 4f;

    [SerializeField]
    private float MaximumHorizontalSpeed = 1f;

    private PlayerInput PlayerInput;

    private Transform Transform;

    private Vector2 AnchorPosition;
    private Vector2 TargetOffset;

    private SleighComponent Sleigh;
    private List<ReindeerComponent> Reindeers;

    public void Start()
    {
        this.PlayerInput = new PlayerInput(PlayerIndex.One, PlayerInput.KeyboardLayout.NumberRow);

        this.InitialReindeerCount = Mathf.Clamp(this.InitialReindeerCount, 1, 4);

        this.Transform = this.GetComponent<Transform>();
        this.AnchorPosition = this.Transform.position;
        this.TargetOffset = Vector2.zero;
        this.Sleigh = this.GetComponentInChildren<SleighComponent>();
        this.Sleigh.Initialize();

        this.Reindeers = new List<ReindeerComponent>(this.InitialReindeerCount);

        for (int i = 0; i < this.InitialReindeerCount; i++)
        {
            this.AddReindeer();
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
        float x = this.PlayerInput.GetHorizontalAxis(PlayerInput.Stick.Left);

        Vector2 targetOffset = new Vector2(x, 0);
        this.TargetOffset = Vector2.MoveTowards(this.TargetOffset, targetOffset, this.HorizontalAcceleration * Time.fixedDeltaTime);

        float speed = Mathf.Min(this.MaximumHorizontalSpeed, this.MaximumHorizontalSpeed * Vector2.Distance(this.AnchorPosition, this.TargetOffset));

        Vector2 offset = this.HorizontalMovementRange * this.TargetOffset;

        this.Transform.position = Vector2.MoveTowards(this.Transform.position, this.AnchorPosition + offset, speed * Time.fixedDeltaTime);

        // Vertical force (downwards)
        float y = this.PlayerInput.GetVerticalAxis(PlayerInput.Stick.Left);

        if (y < 0)
        {
            this.Sleigh.AddVerticalForce(y * Time.fixedDeltaTime);
        }

        for (int i = 0; i < this.Reindeers.Count; i++)
        {
            // Reindeer horizontal movement
            Vector2 position = this.Reindeers[i].Transform.localPosition;
            Vector2 target = this.ReindeerTargetPosition(i, false);
            this.Reindeers[i].Transform.localPosition = Vector2.MoveTowards(position, target, Time.fixedDeltaTime);

            // Update line renderers.
            Vector2 endPoint;

            if (i == 0)
            {
                endPoint = this.Sleigh.LineAnchor.position;
            }
            else
            {
                endPoint = this.Reindeers[i - 1].LineAnchor.position;
            }

            endPoint -= (Vector2)this.Reindeers[i].Transform.position;
            
            this.Reindeers[i].LineRenderer.SetPosition(1, endPoint);
        }


        // Reset input
        this.PlayerInput.ResetInput();
    }

    public void AddReindeer()
    {
        GameObject go = Object.Instantiate<GameObject>(this.ReindeerPrefab, this.Transform, false);
        ReindeerComponent reindeer = go.GetComponent<ReindeerComponent>();
        reindeer.Initialize();

        // Connect this reindeer to the previous reindeer.
        if (this.Reindeers.Count > 0)
        {
            reindeer.SpringJoint.connectedBody = this.Reindeers[this.Reindeers.Count - 1].Rigidbody;
        }
        else
        {
            // Connect the first reindeer to the sleigh.
            reindeer.SpringJoint.connectedBody = this.Sleigh.Rigidbody;
        }

        reindeer.Attached = true;

        this.Reindeers.Add(reindeer);

        // Set initial position.
        reindeer.Transform.localPosition = this.ReindeerTargetPosition(this.Reindeers.Count - 1, true);
    }

    public void RemoveReindeer(ReindeerComponent reindeer)
    {
        for (int i = 0; i < this.Reindeers.Count; i++)
        {
            if (this.Reindeers[i] == reindeer)
            {
                this.RemoveReindeer(i);
                return;
            }
        }
    }

    public void RemoveReindeer(int index)
    {
        if ((index < 0) || (index >= this.Reindeers.Count))
        {
            return;
        }

        if (index < this.Reindeers.Count - 1)
        {
            if (index == 0)
            {
                // Connect the reindeer to the sleigh.
                this.Reindeers[index + 1].SpringJoint.connectedBody = this.Sleigh.Rigidbody;
            }
            else
            {
                // Connect the reindeer to the previous reindeer.
                this.Reindeers[index + 1].SpringJoint.connectedBody = this.Reindeers[index - 1].Rigidbody;
            }
        }

        // Release the reindeer from the chain.
        this.ReleaseReindeer(this.Reindeers[index]);
        this.Reindeers.RemoveAt(index);
    }

    private void ReleaseReindeer(ReindeerComponent reindeer)
    {
        reindeer.Attached = false;

        reindeer.Rigidbody.freezeRotation = false;
        reindeer.Rigidbody.constraints = RigidbodyConstraints2D.None;
        reindeer.SpringJoint.enabled = false;
        reindeer.Rigidbody.gravityScale = 1;
        reindeer.Rigidbody.AddTorque(-Random.Range(1.5f, 2.5f), ForceMode2D.Impulse);
        reindeer.Transform.SetParent(null);
        reindeer.gameObject.layer = GameControl.DebrisLayer;
        reindeer.LineRenderer.enabled = false;
    }

    public void ReleaseSleigh()
    {
        this.Sleigh.Rigidbody.freezeRotation = false;
        this.Sleigh.Rigidbody.constraints = RigidbodyConstraints2D.None;
        this.Sleigh.Rigidbody.gravityScale = 1;
        this.Sleigh.Rigidbody.AddTorque(Random.Range(7.5f, 12.5f), ForceMode2D.Impulse);
        this.Sleigh.Transform.SetParent(null);
        this.Sleigh.gameObject.layer = GameControl.DebrisLayer;

        while (this.Reindeers.Count > 0)
        {
            this.ReleaseReindeer(this.Reindeers[0]);
            this.Reindeers.RemoveAt(0);
        }
    }

    private Vector2 ReindeerTargetPosition(int index, bool initializeY)
    {
        float x = this.Sleigh.Transform.localPosition.x + (index * 1.25f) + 2;
        float y = initializeY ? this.Sleigh.Transform.localPosition.y : this.Reindeers[index].Transform.localPosition.y;
        return new Vector2(x, y);
    }

    private void ActiveReindeerJump(int index)
    {
        if ((index >= 0) && (index < this.Reindeers.Count))
        {
            this.Reindeers[index].Jump();
        }
    }
}
