using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KangorsApprentice : BaseCard
{
    public override IEnumerator DeathRattle()
    {
        if (minions.deadMech.ToArray().Length > 0)
        {
            yield return StartCoroutine(minions.SummonMinion(minions.deadMech[0], deathPosition));
        }
        if (minions.deadMech.ToArray().Length > 1)
        {
            yield return StartCoroutine(minions.SummonMinion(minions.deadMech[1], deathPosition + 1));
        }
    }
}
