using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReindeerComponent : SleighUnitComponent
{
    public SpringJoint2D SpringJoint { get; private set; }
    public LineRenderer LineRenderer { get; private set; }

    [System.NonSerialized]
    public bool Attached;

    [SerializeField]
    private float JumpForce = 10;

    [SerializeField]
    private float JumpCooldown = 1;

    [SerializeField]
    private float MaximumJumpStartHeight = 5;

    private float JumpCooldownTimestamp;

    public override void Initialize()
    {
        base.Initialize();
        this.SpringJoint = this.GetComponent<SpringJoint2D>();
        this.LineRenderer = this.GetComponentInChildren<LineRenderer>();
    }

    public void Start()
    {
        this.JumpCooldownTimestamp = Time.time;
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        if (this.Attached)
        {
            GameControl.instance.Santa.RemoveReindeer(this);
        }
    }

    public void Jump()
    {
        if (this.JumpCooldownTimestamp <= Time.time)
        {
            float scale;

            if (this.Transform.position.y > 0)
            {
                if (this.Transform.position.y < this.MaximumJumpStartHeight)
                {
                    scale = 1 - Mathf.Pow(this.Transform.position.y / this.MaximumJumpStartHeight, 2);
                }
                else
                {
                    scale = 0;
                }
            }
            else
            {
                scale = 1;
            }

            this.Rigidbody.AddForce(Vector2.up * this.JumpForce * scale * Time.fixedDeltaTime, ForceMode2D.Impulse);
            this.JumpCooldownTimestamp = Time.time + this.JumpCooldown;
        }
    }
}
