using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadCell : MovingCell
{
    public override void UpdateData()
    {
        base.UpdateData();
        grid.instance.headPosition = (position[0], position[1]);
        mayMove = true;
    }
}