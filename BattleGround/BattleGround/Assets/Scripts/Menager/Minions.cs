using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour
{
    [SerializeField] private List<BaseCard> minions;

    private int attackingMinionID = 0;

    public int GetNumOfMinions()
    {
        return minions.ToArray().Length;
    }

    public BaseCard GetMinionByID(int num)
    {
        if (num < minions.ToArray().Length - 1)
        {
            return minions[num];
        }
        else
        {
            return null;
        }
    }

    public void SummonMinion(BaseCard baseCard)
    {

    }

    public void RandomlyAttack()
    {

    }

    public int GetLowestAttackMinionID()
    {
        return 0;
    }

    public bool DeathSettlement()
    {
        return false;
    }

    public bool HoloSettlement()
    {
        return false;
    }
}
