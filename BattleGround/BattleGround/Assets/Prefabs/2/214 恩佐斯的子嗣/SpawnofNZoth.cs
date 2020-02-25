using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnofNZoth : BaseCard
{
    public override IEnumerator DeathRattle()
    {
        yield return new WaitForSeconds(0.5f);
        if (isGold){
            foreach (BaseCard baseCard in minions.minions)
            {
                baseCard.ChangeAttack(2);
                baseCard.ChangeHealth(2);
                baseCard.minionView.ViewUpdate();
            }
        }
        else
        {
            foreach (BaseCard baseCard in minions.minions)
            {
                baseCard.ChangeAttack(1);
                baseCard.ChangeHealth(1);
                baseCard.minionView.ViewUpdate();
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
}

