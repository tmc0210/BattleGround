using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaboomBot : BaseCard
{
    public override IEnumerator DeathRattle()
    {
        yield return new WaitForSeconds(0.5f);
        BaseCard target = minions.enemyMinions.minions[minions.enemyMinions.RandomlyChooseMinion()];
        yield return StartCoroutine (target.GetHurt(4));
        yield return new WaitForSeconds(0.5f);
        target = minions.enemyMinions.minions[minions.enemyMinions.RandomlyChooseMinion()];
        yield return StartCoroutine(target.GetHurt(4));
        yield return new WaitForSeconds(0.5f);
    }
}

