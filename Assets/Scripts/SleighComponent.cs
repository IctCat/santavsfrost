using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleighComponent : SleighUnitComponent
{
    [SerializeField]
    private float DiveForce = 10;

    public Transform GiftDropAnchor;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameControl.instance.gameOver)
        {
            GameControl.instance.Santa.ReleaseSleigh();
            GameControl.instance.SantaDied();
        }
    }

    public void AddVerticalForce(float force)
    {
        this.Rigidbody.AddForce(Vector2.up * force * this.DiveForce, ForceMode2D.Force);
    }
}
