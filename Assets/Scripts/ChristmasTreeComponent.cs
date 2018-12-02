using UnityEngine;
using System.Collections;

public class ChristmasTreeComponent : ObstacleComponent
{
    public GameObject GiftPrefab;

    public Transform GiftAnchor;

    public override void Reset()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Object.Instantiate<GameObject>(this.GiftPrefab, this.GiftAnchor, false);
            float x = -1.5f + 3 * (i / 5f);
            float y = -0.3f * (i % 2) + Random.Range(-0.1f, 0.1f) - 0.25f;
            go.transform.position = (Vector2)this.GiftAnchor.position + new Vector2(x, y);
            go.transform.rotation = Quaternion.AngleAxis(Random.Range(-30f, 30f), Vector3.forward);
        }

        base.Reset();
    }

    public GiftComponent[] GetGifts()
    {
        return this.GetComponentsInChildren<GiftComponent>();
    }
}
