using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gene
{
    public alib.GENES gene;
    public int id_level;
    public int EnergyValue;

    public Gene(alib.GENES gene, int id_level)
    {
        this.gene = gene;
        this.id_level = id_level;
        float a = 1.0f;
        switch (gene)
        {
            case alib.GENES.Production:
                a = -a * 3;
                break;

            case alib.GENES.Immune:
                a *= 2;
                break;

            case alib.GENES.Volume:
                a *= 2;
                break;
        }
        EnergyValue = Mathf.FloorToInt(a * id_level * id_level / 2);
    }
}