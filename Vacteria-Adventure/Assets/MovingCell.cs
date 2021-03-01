using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCell : Cell
{
    public bool disable = false;
    public bool mayMove = false;

    public void Move(int[] pos)
    {
        int[] old = position;
        if (grid.instance.isPositionValid((pos[0], pos[1]), (old[0], old[1])))
        {
            position = pos;
            UpdateKey();
            grid.instance.listOfCells.Remove((old[0], old[1]));
        }
    }
}