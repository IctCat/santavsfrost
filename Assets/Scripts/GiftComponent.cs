using UnityEngine;
using System.Collections;

public class GiftComponent : MonoBehaviour
{
    public SpriteRenderer BoxRenderer;
    public SpriteRenderer WrappingRenderer;
    public Transform Transform;
    public Rigidbody2D Rigidbody;
    public Collider2D Collider;

    private Transform TargetTransform;

    public void Start()
    {
        this.TargetTransform = null;
    }

    public void FixedUpdate()
    {
        if (this.TargetTransform != null)
        {
            if (Mathf.Abs(this.Transform.position.x - this.TargetTransform.position.x) > 0.01f)
            {
                Vector2 position = Vector2.MoveTowards(this.Transform.position, this.TargetTransform.position, Time.fixedDeltaTime);
                position.y = this.Transform.position.y;
                this.Transform.position = position;
            }
            else if (this.WrappingRenderer.enabled && (this.Transform.position.y < this.TargetTransform.position.y - 0.5f))
            {
                this.WrappingRenderer.enabled = false;
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == GameControl.GroundLayer)
        {
            if (this.TargetTransform != null)
            {
                Object.Destroy(this.gameObject);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.TargetTransform != null)
        {
            return;
        }

        ChimneyComponent chimney = collision.gameObject.GetComponent<ChimneyComponent>();

        if (chimney != null)
        {
            GameControl.instance.AddSantaScore();
            GameControl.instance.UpdateScoreTexts();

            this.Rigidbody.velocity = Vector2.zero;
            this.TargetTransform = collision.GetComponent<Transform>();
            this.gameObject.layer = GameControl.IgnoreObstaclesLayer;
        }
    }
}
