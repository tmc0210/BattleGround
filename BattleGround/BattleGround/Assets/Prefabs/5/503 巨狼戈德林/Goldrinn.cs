using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goldrinn : BaseCard
{
    public override IEnumerator DeathRattle()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (BaseCard baseCard in minions.minions)
        {
            if (baseCard.IsBeast)
            {
                baseCard.ChangeAttack(8);
                baseCard.ChangeHealth(8);
                baseCard.minionView.ViewUpdate();
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
}
