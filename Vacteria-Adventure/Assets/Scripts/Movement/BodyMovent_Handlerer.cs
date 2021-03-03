using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BodyMovent_Handlerer : MonoBehaviour
{
    public void HandleBodyPos() {
        int found;
        List<int, int> wrongPos = new List<int, int>();

        for (int i = 0; i <= 100; i++) 
        {
            for (int j = 0; j <= 100; j ++)
            {
                switch(cell.type){

                    case alib.cellType.head:
                        found = true;
                        break;
                }
                switch(cell.type){

                    case alib.cellType.head:
                        found = true;
                        break;

                    case found == false:
                        wrongPos.Add((i, j));
                }

                if (found == true)
                {
                    FixWrongPos(wrongPos);
                }
                

            }
        }
    }

    public void FixWrongPos(List<int, int> wrongPos) {
        foreach (var pos in wrongPos)
        {
            
        }
    }

    public void MoveCell(int x, int y, Cell cellToMove)
    {
        ((MovingCell)cellTo).Move(new int[] { x, y });
    }

}