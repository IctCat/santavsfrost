using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReindeerComponent : SleighUnitComponent
{
    public SpringJoint2D SpringJoint { get; private set; }

    [SerializeField]
    private float JumpForce = 10;

    [SerializeField]
    private float JumpCooldown = 1;

    private float JumpCooldownTimestamp;

    public override void Initialize()
    {
        base.Initialize();
        this.SpringJoint = this.GetComponent<SpringJoint2D>();
    }

    public void Start()
    {
        this.JumpCooldownTimestamp = Time.time;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameControl.instance.Santa.RemoveReindeer(this);
    }

    public void Jump()
    {
        if (this.JumpCooldownTimestamp <= Time.time)
        {
            this.Rigidbody.AddForce(Vector2.up * this.JumpForce * Time.fixedDeltaTime, ForceMode2D.Impulse);
            this.JumpCooldownTimestamp = Time.time + this.JumpCooldown;
        }
    }
}
