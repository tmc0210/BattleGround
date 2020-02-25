using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CobaltGuardian : BaseCard
{
    public BaseCard prefab;
    public int numOfDeathrattle = 3;

    public override IEnumerator DeathRattle()
    {
        int tmp = 0;
        while (tmp < numOfDeathrattle)
        {
            yield return StartCoroutine(minions.SummonMinion(prefab, deathPosition + tmp));
            tmp++;
        }
    }

    public override IEnumerator SummonMinion(BaseCard baseCard)
    {
        if (!isDivineShield && baseCard.IsRace(Race.RaceType.Mech))
        {
            yield return new WaitForSeconds(0.2f);
            isDivineShield = true;
            minionView.KeyWordUpdate();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
