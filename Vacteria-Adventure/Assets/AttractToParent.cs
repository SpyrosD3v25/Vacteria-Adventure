using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractToParent : MonoBehaviour
{
    public Rigidbody2D rg;
    private int tick = 99;

    private void FixedUpdate()
    {
        rg.AddForce(new Vector2(-transform.localPosition.x, -transform.localPosition.y).normalized);
        tick++;
        if (tick == 100)
        {
            tick = 0;
            rg.AddForce(new Vector2(Random.Range(0, 2), Random.Range(0, 2)));
        }
    }
}