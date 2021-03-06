using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractToParent : MonoBehaviour
{
    public Rigidbody2D rg;
    private int tick = 99;
    public Transform TargetPosition;
    //
    private void FixedUpdate()
    {
        rg.velocity = 5 * (new Vector2((-transform.localPosition.x + TargetPosition.position.x), -transform.localPosition.y + TargetPosition.position.y)) * (Vector2.Distance(TargetPosition.position, transform.position));
        tick++;
        if (tick == 100)
        {
            tick = 0;
            rg.AddForce(new Vector2(Random.Range(0, 2), Random.Range(0, 2)));
        }
    }
}