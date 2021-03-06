using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCells : MonoBehaviour
{
    private bool isAlreadyPressed = false;
    private Cell MovingCell;
    public GameObject canva;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !alib.isMoving)
        {
            OpenLab();
        }
        if (!alib.isLab)
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
                ((MovingCell)MovingCell).setMove();
                if (!grid.instance.listOfCells.ContainsKey((x, y)))
                {
                    grid.instance.MoveCell(x, y, MovingCell);
                }
            }
        }
    }

    private void OpenLab()
    {
        if (!alib.isLab)
        {
            alib.isLab = true;
            Time.timeScale = 0;
            canva.SetActive(true);
        }
        else
        {
            alib.isLab = false;
            Time.timeScale = 1;
            canva.SetActive(false);
        }
    }
}