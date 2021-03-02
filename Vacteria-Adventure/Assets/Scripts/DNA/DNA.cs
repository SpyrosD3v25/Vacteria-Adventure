using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DNA
{
    public Gene[] genes = new Gene[Enum.GetNames(typeof(alib.GENES)).Length];
}