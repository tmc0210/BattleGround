using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KangorsApprentice : BaseCard
{
    public override IEnumerator DeathRattle()
    {
        yield return StartCoroutine(minions.SummonMinion(minions.deadMech[0], deathPosition));
        yield return StartCoroutine(minions.SummonMinion(minions.deadMech[1], deathPosition + 1));
    }
}
