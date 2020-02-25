using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour
{
    public const int LIMIT_OF_MINIONS = 7;

    public List<BaseCard> minions = new List<BaseCard>();
    public List<BaseCard> deadMinion = new List<BaseCard>();
    public List<BaseCard> damagedMinion = new List<BaseCard>();

    public List<BaseCard> deadMech = new List<BaseCard>();

    public Minions enemyMinions;
    public GameController gameController;
    public AllMinions allMinions;

    public bool isRivendare = false;

    [SerializeField] private bool isUnder = false;
    [SerializeField] private int attackingMinionID = -1;

    public int GetNumOfMinions()
    {
        if (minions.ToArray().Length > 0)
        {
            return minions.ToArray().Length;
        }
        else
        {            
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
                baseCard.SetInvisible();                
            }
        }
    }

    public BaseCard GetMinionByID(int num)
    {
        if (num < minions.ToArray().Length && num >= 0)
        {
            return minions[num];
        }
        else
        {
            return null;
        }
    }

    public IEnumerator SummonMinion(BaseCard baseCard, int position)
    {
        if (GetNumOfMinions() < 7)
        {
            yield return new WaitForSeconds(0.2f);
            BaseCard newMinion = Instantiate(baseCard, transform.position + new Vector3(0, 0, 5), transform.rotation);
            if (position >= GetNumOfMinions())
            {
                minions.Add(newMinion);
            }
            else
            {
                minions.Insert(position, newMinion);
            }
            newMinion.Summon(this);
            if (deadMinion.ToArray().Length != 0)
            {
                foreach(BaseCard deadMinion in deadMinion)
                {
                    if (deadMinion.deathPosition >= position && minions.Contains(deadMinion))
                    {
                        deadMinion.deathPosition += 1;
                    }
                }
            }
            newMinion.transform.parent = gameObject.transform;
            yield return new WaitForSeconds(0.5f);
            yield return StartCoroutine(PositionUpdate());
            yield return StartCoroutine(MinionSummonSettleMent(newMinion));
        }
    }

    public IEnumerator RandomlyAttack()
    {
        if (GetNumOfMinions() != 0)
        {
            attackingMinionID = (attackingMinionID + 1) % GetNumOfMinions();
            int tmp = enemyMinions.RandomlyChooseMinionToAttack();
            if (tmp < enemyMinions.GetNumOfMinions() && tmp >= 0)
            {
                yield return StartCoroutine(minions[attackingMinionID].AttackAction(tmp));
            }
        }
        else
        {
            yield return 0;
        }
    }

    public int RandomlyChooseMinion()
    {
        GetNumOfMinions();
        int tmp1 = 0;
        int tmp2 = 0;
        do
        {
            tmp1 = Random.Range(0, GetNumOfMinions());
            tmp2++;
        }
        while (GetMinionByID(tmp1).IsDie() && tmp2 <= 500);
        return tmp1;    
    }

    public int RandomlyChooseMinionToAttack()
    {
        List<BaseCard> tauntMinions = new List<BaseCard>();

        foreach(BaseCard baseCard in minions)
        {
            if (baseCard.IsTaunt)
            {
                tauntMinions.Add(baseCard);
            }
        }
        
        if (tauntMinions.ToArray().Length != 0)
        {
            return tauntMinions[Random.Range(0, tauntMinions.ToArray().Length)].GetPosition();
        }
        else
        {
            return RandomlyChooseMinion();
        }
    }

    public int GetLowestAttackMinionID()
    {
        return 0;
    }

    public IEnumerator DeathSettlement()
    {
        yield return new WaitForSeconds(0.8f);

        //FirstAuraSettlement()

        foreach (BaseCard baseCard in minions)
        {            
            if (baseCard.IsDie())
            {
                deadMinion.Add(baseCard);
                if (baseCard.IsMech)
                {
                    deadMech.Add(allMinions.GetCardByInt(baseCard.ID));
                }
                baseCard.DeathRattleView();
            }
        }

        foreach (BaseCard baseCard in enemyMinions.minions)
        {           
            if (baseCard.IsDie())
            {
                deadMinion.Add(baseCard);
                if (baseCard.IsMech)
                {
                    enemyMinions.deadMech.Add(allMinions.GetCardByInt(baseCard.ID));
                }
                baseCard.DeathRattleView();
            }
        }
        
        foreach (BaseCard baseCard in deadMinion)
        {
            //baseCard.AuraRemove();
            baseCard.deathPosition = baseCard.GetPosition();
            Remove(baseCard);            
            enemyMinions.Remove(baseCard);
        }

        if (deadMinion.ToArray().Length != 0)
        {
            yield return StartCoroutine(DeathRattleSettlement());
            deadMinion = new List<BaseCard>();
            yield return StartCoroutine(PositionUpdate());
            StartCoroutine(DeathSettlement());
        }
        else
        {
            IsGameOver();
            enemyMinions.IsGameOver();
            gameController.NextTurn(enemyMinions);
        }
    }

    public void RemoveDamageText()
    {
        foreach(BaseCard baseCard in minions)
        {
            baseCard.NotShowNumOfDamage();
        }
    }

    public IEnumerator DeathRattleSettlement()
    {
        foreach(BaseCard deadMinion in deadMinion)
        {
            yield return StartCoroutine(deadMinion.DeathRattle());
            if (deadMinion.minions.isRivendare)
            {
                yield return StartCoroutine(deadMinion.DeathRattle());
                yield return StartCoroutine(deadMinion.DeathRattle());
            }
        }
    }

    public IEnumerator MinionSummonSettleMent(BaseCard baseCard)
    {
        foreach (BaseCard minion in minions)
        {
            yield return StartCoroutine(minion.SummonMinion(baseCard));
        }
    }

    public IEnumerator DamagedSettlement()
    {
        foreach (BaseCard damagedMinion in damagedMinion)
        {
            yield return StartCoroutine(damagedMinion.TakeDamage());
        }
    }

    public void MinionsGetAura(BaseCard baseCard)
    {
        foreach (BaseCard minion in minions)
        {
            baseCard.GetAura(minion);
        }
    }

    public void MinionsRemoveAura(BaseCard baseCard)
    {
        foreach (BaseCard minion in minions)
        {
            baseCard.RemoveAura(minion);
        }
    }

    public IEnumerator PositionUpdate()
    {
        foreach (BaseCard baseCard in minions)
        {
            StartCoroutine(baseCard.MoveTo(baseCard.PositionCalculate(baseCard.GetPosition(), GetNumOfMinions())));
        }

        foreach (BaseCard baseCard in enemyMinions.minions)
        {
            StartCoroutine(baseCard.MoveTo(baseCard.PositionCalculate(baseCard.GetPosition(), enemyMinions.GetNumOfMinions())));
        }

        yield return new WaitForSeconds(1);
    }

    public bool IsUnder()
    {
        return isUnder;
    }

    public void GameStart()
    {
        foreach (BaseCard baseCard in minions)
        {
            baseCard.Summon(this);
        }
    }

    public void IsGameOver()
    {
        if (minions.ToArray().Length == 0)
        {
            if (enemyMinions.GetNumOfMinions() != 0)
            {
                gameController.GameOver(gameObject.name);
            }
            else
            {
                gameController.GameOver("Draw!");
            }
        }
    }
}
