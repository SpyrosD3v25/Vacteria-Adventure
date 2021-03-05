using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int[] position = new int[2];
    public alib.CellType cellType;
    public SpriteRenderer sr;

    public SpriteRenderer GetRenderer()
    {
        return GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        grid.instance.UpdateCells += UpdateData;
    }

    private void FixedUpdate()
    {
        TextureUpdate();
    }

    public virtual void TextureUpdate()
    {
        if (sr == null) sr = GetRenderer();
        switch (cellType)
        {
            case alib.CellType.head:
                sr.sprite = grid.instance.Head;
                break;

            case alib.CellType.simple:
                sr.sprite = grid.instance.Simple;
                break;

            case alib.CellType.hardened:
                sr.sprite = grid.instance.Hardened;
                break;

            case alib.CellType.laser:
                sr.sprite = grid.instance.Hardened;
                break;

            case alib.CellType.dirt:
                sr.sprite = grid.instance.Hardened;
                break;

            case alib.CellType.stone:
                sr.sprite = grid.instance.Hardened;
                break;

            case alib.CellType.miner:
                sr.sprite = grid.instance.Head;
                break;
        }
    }

    public virtual void UpdateData()
    {
        try
        {
            transform.position = new Vector2(position[0], position[1]);
            TextureUpdate();
        }
        catch (System.Exception) { };
    }

    public void UpdateKey()
    {
        if (!grid.instance.listOfCells.ContainsKey((position[0], position[1])))
        {
            grid.instance.listOfCells.Add((position[0], position[1]), this);
        }
    }

    public virtual void Destroy()
    {
        grid.instance.listOfCells.Remove((position[0], position[1]));
        grid.instance.UpdateCells();
        Destroy(gameObject);
    }
}