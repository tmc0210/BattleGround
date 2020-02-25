using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveHydra : BaseCard
{ 
    public override IEnumerator Attack(BaseCard target)
    {
        yield return StartCoroutine(target.GetHurt(GetCurrentAttack()));
        yield return StartCoroutine(GetHurt(target.GetCurrentAttack()));
        if (enemyMinions.GetMinionByID(target.GetPosition() - 1) != null)
        {
            yield return StartCoroutine(enemyMinions.GetMinionByID(target.GetPosition() - 1).GetHurt(GetCurrentAttack()));
        }
        if (enemyMinions.GetMinionByID(target.GetPosition() + 1) != null)
        {
            yield return StartCoroutine(enemyMinions.GetMinionByID(target.GetPosition() + 1).GetHurt(GetCurrentAttack()));
        }
    }
}
