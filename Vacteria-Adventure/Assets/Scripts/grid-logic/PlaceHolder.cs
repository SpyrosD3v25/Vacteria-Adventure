using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceHolder : MonoBehaviour
{
    public alib.CellType cellType;
    public SpriteRenderer sp;

    private void Update()
    {
        transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        switch (cellType)
        {
            case alib.CellType.head:
                sp.sprite = grid.instance.Head;
                break;

            case alib.CellType.simple:
                sp.sprite = grid.instance.Simple;
                break;

            case alib.CellType.hardened:
                sp.sprite = grid.instance.Hardened;
                break;

            case alib.CellType.miner:
                sp.sprite = grid.instance.Hardened;
                break;
        }
    }
}