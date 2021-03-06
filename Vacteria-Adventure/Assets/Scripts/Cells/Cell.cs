using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int[] position = new int[2];
    public alib.CellType cellType;
    public SpriteRenderer sr;
    public Animator an;

    public SpriteRenderer GetRenderer()
    {
        return GetComponent<SpriteRenderer>();
    }

    public Animator GetAnimator()
    {
        return GetComponent<Animator>();
    }

    private void Start()
    {
        grid.instance.UpdateCells += UpdateData;
    }

    public virtual void TextureUpdate()
    {
        if (sr == null) sr = GetRenderer();
        if (an == null) an = GetAnimator();
        switch (cellType)
        {
            case alib.CellType.head:
                an.SetTrigger("Core");
                break;

            case alib.CellType.hardened:
                an.SetTrigger("Capsule");
                break;

            case alib.CellType.miner:

                break;

            case alib.CellType.vines:
                sr.sprite = grid.instance.stoneTypes[0];
                break;

            case alib.CellType.ultisols:
                sr.sprite = grid.instance.stoneTypes[1];
                break;

            case alib.CellType.scoria:
                sr.sprite = grid.instance.stoneTypes[2];
                break;

            case alib.CellType.tuff:
                sr.sprite = grid.instance.stoneTypes[3];
                break;

            case alib.CellType.myionite:
                sr.sprite = grid.instance.stoneTypes[4];
                break;

            case alib.CellType.steel:
                sr.sprite = grid.instance.stoneTypes[5];
                break;

            case alib.CellType.titanium:
                sr.sprite = grid.instance.stoneTypes[6];
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