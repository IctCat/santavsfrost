using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleighComponent : SleighUnitComponent
{
    [SerializeField]
    private float DiveForce = 10;

    public Transform GiftDropAnchor;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        this.Collision(collider.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        this.Collision(collision.gameObject);
    }

    private void Collision(GameObject go)
    {
        if (!GameControl.instance.gameOver)
        {
            ObstacleComponent obstacle = go.GetComponent<ObstacleComponent>();

            if (obstacle != null && obstacle.tag != "GiftTree")
            {
                SFXPlayer.Instance.Play(GameControl.instance.Santa.DeathSound);
                GameControl.instance.Santa.ReleaseSleigh();
                GameControl.instance.SantaDied();
                FrostCloud.Triggered = true;
            }
            else if (obstacle != null && obstacle.tag == "GiftTree")
            {
                GameControl.instance.Santa.RestockGifts();
                SFXPlayer.Instance.Play(GameControl.instance.Santa.RefillSound);
            }
        }
    }

    public void AddVerticalForce(float force)
    {
        this.Rigidbody.AddForce(Vector2.up * force * this.DiveForce, ForceMode2D.Force);
    }
}
