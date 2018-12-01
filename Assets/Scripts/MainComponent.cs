using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainComponent : MonoBehaviour
{
    public static MainComponent Instance;

    [System.NonSerialized]
    public SantaComponent Santa;

    public void Start()
    {
        MainComponent.Instance = this;
        this.Santa = Object.FindObjectOfType<SantaComponent>();
    }
}
