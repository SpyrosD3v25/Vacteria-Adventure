using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCell : Cell
{
    public bool disable = false;
    public bool mayMove = true;
    public bool isMoving = false;

    public virtual void Move(int[] pos)
    {
        int[] old = position;
        if (grid.instance.isPositionValid((pos[0], pos[1]), (old[0], old[1])))
        {
            position = pos;
            UpdateKey();
            grid.instance.listOfCells.Remove((old[0], old[1]));
        }
    }

    public void Update()
    {
        if (isMoving)
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        }
    }

    public void setMove()
    {
        if (isMoving)
        {
            isMoving = false;
        }
        else isMoving = true;
    }
}