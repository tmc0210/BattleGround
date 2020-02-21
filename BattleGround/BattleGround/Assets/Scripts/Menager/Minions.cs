using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour
{
    public List<BaseCard> minions = new List<BaseCard>();
    public List<BaseCard> deadMinion = new List<BaseCard>();

    public Minions enemyMinions;
    public GameController gameController;

    [SerializeField] private int attackingMinionID = -1;

    public float speedOfAttack = 10.0f;
    public float speedOfMove = 2.0f;

    public int GetNumOfMinions()
    {
        if (minions.ToArray().Length > 0)
        {
            return minions.ToArray().Length;
        }
        else
        {
            if (enemyMinions.GetNumOfMinions() != 0)
            {
                gameController.GameOver(gameObject.name);

            }
            else
            {
                gameController.GameOver("Draw!");
            }
            return 0;
        }
    }

    public void Remove(BaseCard baseCard)
    {
        for (int i = 0;i < GetNumOfMinions(); i++)
        {
            if (GetMinionByID(i) == baseCard)
            {
                if (i <= attackingMinionID % GetNumOfMinions())
                {
                    attackingMinionID--;
                }
                minions.Remove(baseCard);
            }
        }
    }

    public BaseCard GetMinionByID(int num)
    {
        if (num < minions.ToArray().Length)
        {
            return minions[num];
        }
        else
        {
            return null;
        }
    }

    public void SummonMinion(BaseCard baseCard, int position)
    {

    }

    public void RandomlyAttack()
    {
        GetNumOfMinions();
        attackingMinionID = (attackingMinionID + 1) % GetNumOfMinions();
        minions[attackingMinionID].AttackAction(enemyMinions.GetMinionByID(enemyMinions.RandomlyChooseMinion()));    
    }

    public int RandomlyChooseMinion()
    {
        GetNumOfMinions();
        return Random.Range(0, GetNumOfMinions());       
    }

    public int GetLowestAttackMinionID()
    {
        return 0;
    }

    public bool DeathSettlement()
    {  
        foreach(BaseCard baseCard in minions)
        {
            //baseCard.NotShowNumOfDamage();
            if (baseCard.IsDie())
            {
                deadMinion.Add(baseCard);                
            }
        }

        foreach (BaseCard baseCard in enemyMinions.minions)
        {
            //baseCard.NotShowNumOfDamage();
            if (baseCard.IsDie())
            {
                deadMinion.Add(baseCard);
            }
        }
        foreach (BaseCard baseCard in deadMinion)
        {
            Remove(baseCard);            
            enemyMinions.Remove(baseCard);
        }

        if (deadMinion.ToArray().Length != 0)
        {
            HoloSettlement();
            DeathRattleSettlement();
            deadMinion = new List<BaseCard>();
            DeathSettlement();
        }
        else
        {            
            gameController.NextTurn(enemyMinions);
        }
        return false;
    }

    public bool DeathRattleSettlement()
    {
        foreach(BaseCard baseCard in deadMinion)
        {
            baseCard.DeathRattle();
        }
        return true;
    }

    public bool HoloSettlement()
    {
        return false;
    }
}
