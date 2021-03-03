using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gene
{
    public alib.GENES gene;
    public int id_level;

    public Gene(alib.GENES gene, int id_level)
    {
        this.gene = gene;
        this.id_level = id_level;
    }
}