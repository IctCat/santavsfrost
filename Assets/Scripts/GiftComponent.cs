using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

        List<Color> colors = new List<Color>();
        colors.Add(new Color(1f, 0.5f, 0.5f));
        colors.Add(new Color(0.5f, 1f, 0.5f));
        colors.Add(new Color(0.5f, 0.5f, 1f));
        colors.Add(new Color(1f, 1f, 0.5f));
        colors.Add(new Color(1f, 0.5f, 1f));
        colors.Add(new Color(0.5f, 1f, 1f));

        colors.Add(new Color(1f, 0.75f, 0.5f));
        colors.Add(new Color(1f, 0.5f, 0.75f));
        colors.Add(new Color(0.5f, 1f, 0.75f));
        colors.Add(new Color(0.75f, 1f, 0.5f));
        colors.Add(new Color(0.75f, 0.5f, 1f));
        colors.Add(new Color(0.5f, 0.75f, 1f));

        int a = Random.Range(0, colors.Count);
        Color box = colors[a];
        colors.RemoveAt(a);

        int b = Random.Range(0, colors.Count);
        Color wrapping = colors[b];
        colors.RemoveAt(b);

        this.BoxRenderer.color = box;
        this.WrappingRenderer.color = wrapping;
    }

    public void FixedUpdate()
    {
        if (this.Rigidbody == null)
        {
            return;
        }

        if (this.TargetTransform != null)
        {
            if (Mathf.Abs(this.Transform.position.x - this.TargetTransform.position.x) > 0.01f)
            {
                Vector2 position = Vector2.MoveTowards(this.Transform.position, this.TargetTransform.position, 5f * Time.fixedDeltaTime);
                position.y = this.Transform.position.y;
                this.Transform.position = position;
            }

            if (this.WrappingRenderer.enabled && (this.Transform.position.y < this.TargetTransform.position.y - 1f))
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
            this.Rigidbody.angularVelocity = 0;
            this.TargetTransform = collision.GetComponent<Transform>();
            this.gameObject.layer = GameControl.IgnoreObstaclesLayer;
        }
    }
}
