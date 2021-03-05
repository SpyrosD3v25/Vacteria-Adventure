using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCells : MonoBehaviour
{
    private bool isAlreadyPressed = false;
    private GameObject PlaceHolder;
    private Cell MovingCell;

    private void Update()
    {
        if (alib.isEditMode)
        {
            if (Input.GetMouseButton(0))
            {
                if (!isAlreadyPressed)
                {
                    GetPositionData(true);
                }
                isAlreadyPressed = true;
            }
            else if (!Input.GetMouseButton(0))
            {
                if (isAlreadyPressed)
                {
                    GetPositionData(false);
                }
                isAlreadyPressed = false;
            }
        }
    }

    private void GetPositionData(bool onCreate)
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int x = (int)Mathf.Round(mousePos.x);
        int y = (int)Mathf.Round(mousePos.y);
        if (onCreate)
        {
            if (grid.instance.listOfCells.ContainsKey((x, y)))
            {
                MovingCell = grid.instance.listOfCells[(x, y)];
                if (MovingCell is MovingCell cell && cell.mayMove)
                {
                    alib.isMoving = true;
                    cell.setMove();
                }
            }
        }
        else
        {
            if (alib.isMoving)
            {
                alib.isMoving = false;
                if (PlaceHolder != null) Destroy(PlaceHolder);
                ((MovingCell)MovingCell).setMove();
                if (!grid.instance.listOfCells.ContainsKey((x, y)))
                {
                    grid.instance.MoveCell(x, y, MovingCell);
                }
            }
        }
    }
}