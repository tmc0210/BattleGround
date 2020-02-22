using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanoEgg : BaseCard
{
    public BaseCard prefab;

    public override IEnumerator DeathRattle()
    {
        yield return StartCoroutine(minions.SummonMinion(prefab, deathPosition));
        yield return StartCoroutine(minions.SummonMinion(prefab, deathPosition + 1));
    }
}
