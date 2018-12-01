using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleighUnitComponent : MonoBehaviour
{
    public Rigidbody2D Rigidbody { get; private set; }

    public virtual void Initialize()
    {
        this.Rigidbody = this.GetComponent<Rigidbody2D>();
    }
}
