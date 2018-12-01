using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleighUnitComponent : MonoBehaviour
{
    public Transform Transform { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public Collider2D Collider { get; private set; }

    public Transform LineAnchor;

    public virtual void Initialize()
    {
        this.Transform = this.GetComponent<Transform>();
        this.Rigidbody = this.GetComponent<Rigidbody2D>();
        this.Collider = this.GetComponent<Collider2D>();
    }
}
