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

    private void FixedUpdate()
    {
        TextureUpdate();
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

            case alib.CellType.simple:
                an.SetTrigger("Cytoplasm");
                break;

            case alib.CellType.hardened:
                an.SetTrigger("Capsule");
                break;

            case alib.CellType.dirt:

                break;

            case alib.CellType.stone:

                break;

            case alib.CellType.miner:

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