using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleighComponent : SleighUnitComponent
{
    [SerializeField]
    private float DiveForce = 10;

    public Transform GiftDropAnchor;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (!GameControl.instance.gameOver)
        {
            ObstacleComponent obstacle = collision.gameObject.GetComponent<ObstacleComponent>();

            if (obstacle != null)
            {
                GameControl.instance.Santa.ReleaseSleigh();
                GameControl.instance.SantaDied();
            }
        }
    }

    public void AddVerticalForce(float force)
    {
        this.Rigidbody.AddForce(Vector2.up * force * this.DiveForce, ForceMode2D.Force);
    }
}
