using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCell : MovingCell
{
    public bool isActive = false;

    public override void UpdateData()
    {
        base.UpdateData();
        isActive = grid.instance.isPositionValid((position[0], position[1]), null);
        //mayMove = isActive;
    }

    public override void TextureUpdate()
    {
        base.TextureUpdate();
        if (isActive) sr.color = new Color(1, 1, 1, 1);
        else sr.color = new Color(1, 1, 1, 0.5f);
    }
}