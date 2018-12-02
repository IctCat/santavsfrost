using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleighComponent : SleighUnitComponent
{
    [SerializeField]
    private float DiveForce = 10;

    public Transform GiftDropAnchor;
    public Transform SackTransform;

    public GiftComponent[] TreeGifts;

    public void OnTriggerEnter2D(Collider2D collider)
    {
        this.Collision(collider.gameObject);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        this.Collision(collision.gameObject);
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < this.TreeGifts.Length; i++)
        {
            if (this.TreeGifts[i] == null)
            {
                continue;
            }

            this.TreeGifts[i].Transform.SetParent(null, true);
            Vector2 position = this.TreeGifts[i].transform.position;
            position = Vector2.Lerp(position, this.SackTransform.position, 3f * Time.fixedDeltaTime);
            position = Vector2.MoveTowards(position, this.SackTransform.position, 10f * Time.fixedDeltaTime);

            if (Vector2.Distance(this.SackTransform.position, position) < 0.1f)
            {
                SFXPlayer.Instance.Play(GameControl.instance.Santa.RefillSound);
                Object.Destroy(this.TreeGifts[i].gameObject);
            }
            else
            {
                this.TreeGifts[i].transform.position = position;
            }
        }
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
                ChristmasTreeComponent tree = obstacle.GetComponent<ChristmasTreeComponent>();
                this.TreeGifts = tree.GetGifts();

                GameControl.instance.Santa.RestockGifts();
            }
        }
    }

    public void AddVerticalForce(float force)
    {
        this.Rigidbody.AddForce(Vector2.up * force * this.DiveForce, ForceMode2D.Force);
    }
}
