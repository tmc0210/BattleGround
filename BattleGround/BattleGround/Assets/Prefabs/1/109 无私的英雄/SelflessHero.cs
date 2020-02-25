using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelflessHero : BaseCard
{
    public override IEnumerator DeathRattle()
    {
        yield return new WaitForSeconds(0.5f);

        if (minions.GetMinionByID(minions.RandomlyChooseMinion()) != null)
        {
            int i = 0;
            int tmp = 0;
            do
            {
                tmp = minions.RandomlyChooseMinion();
                i++;
            }
            while (minions.GetMinionByID(tmp).IsDivineShield && i <= 50);
            minions.GetMinionByID(tmp).IsDivineShield = true;
            minions.GetMinionByID(tmp).minionView.ViewUpdate();
        }

        if (isGold)
        {
            if (minions.GetMinionByID(minions.RandomlyChooseMinion()) != null)
            {
                int i = 0;
                int tmp = 0;
                do
                {
                    tmp = minions.RandomlyChooseMinion();
                    i++;
                }
                while (minions.GetMinionByID(tmp).IsDivineShield && i <= 50);
                minions.GetMinionByID(tmp).IsDivineShield = true;
                minions.GetMinionByID(tmp).minionView.ViewUpdate();
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
}
