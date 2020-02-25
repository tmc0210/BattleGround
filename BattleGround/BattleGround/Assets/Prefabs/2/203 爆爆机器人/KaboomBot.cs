using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KaboomBot : BaseCard
{
    public override IEnumerator DeathRattle()
    {
        yield return new WaitForSeconds(0.5f);
        if (enemyMinions.GetNumOfMinions() > 0)
        {
            BaseCard target = minions.enemyMinions.minions[minions.enemyMinions.RandomlyChooseMinion()];
            if (target != null) yield return StartCoroutine(target.GetHurt(4));
        }
        if (isGold)
        {
            yield return new WaitForSeconds(0.5f);
            if (enemyMinions.GetNumOfMinions() > 0)
            {
                BaseCard target = minions.enemyMinions.minions[minions.enemyMinions.RandomlyChooseMinion()];
                if (target != null) yield return StartCoroutine(target.GetHurt(4));
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
}

