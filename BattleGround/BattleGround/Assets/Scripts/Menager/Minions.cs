using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minions : MonoBehaviour
{
    public const int LIMIT_OF_MINIONS = 7;

    public List<BaseCard> minions = new List<BaseCard>();
    public List<BaseCard> deadMinion = new List<BaseCard>();
    public List<BaseCard> damagedMinion = new List<BaseCard>();

    public Minions enemyMinions;
    public GameController gameController;

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
                baseCard.SetInvisible();                
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

    public IEnumerator SummonMinion(BaseCard baseCard, int position)
    {
        if (GetNumOfMinions() < 7)
        {
            yield return new WaitForSeconds(0.7f);
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
        }
        yield return StartCoroutine(PositionUpdate());
    }

    public IEnumerator RandomlyAttack()
    {
        GetNumOfMinions();
        attackingMinionID = (attackingMinionID + 1) % GetNumOfMinions();
        yield return StartCoroutine(minions[attackingMinionID].AttackAction(enemyMinions.RandomlyChooseMinionToAttack()));    
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
        while (GetMinionByID(tmp1).IsDie() && tmp2 <= 50);
        return tmp1;    
    }

    public int RandomlyChooseMinionToAttack()
    {
        GetNumOfMinions();

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
            return tauntMinions[Random.Range(0, tauntMinions.ToArray().Length)].GetID();
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

        foreach (BaseCard baseCard in minions)
        {            
            if (baseCard.IsDie())
            {
                deadMinion.Add(baseCard);
                baseCard.DeathRattleView();
            }
        }

        foreach (BaseCard baseCard in enemyMinions.minions)
        {           
            if (baseCard.IsDie())
            {
                deadMinion.Add(baseCard);
                baseCard.DeathRattleView();
            }
        }

        AuraUpdateSettlement();
        enemyMinions.AuraUpdateSettlement();

        foreach (BaseCard baseCard in deadMinion)
        {
            baseCard.AuraRemove();
            baseCard.deathPosition = baseCard.GetID();
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
        }
    }

    public IEnumerator DamagedSettlement()
    {
        foreach (BaseCard damagedMinion in damagedMinion)
        {
            yield return StartCoroutine(damagedMinion.TakeDamage());
        }
    }

    public void AuraUpdateSettlement()
    {

    }
    
    public IEnumerator PositionUpdate()
    {
        foreach (BaseCard baseCard in minions)
        {
            StartCoroutine(baseCard.MoveTo(baseCard.PositionCalculate(baseCard.GetID(), GetNumOfMinions())));
        }

        foreach (BaseCard baseCard in enemyMinions.minions)
        {
            StartCoroutine(baseCard.MoveTo(baseCard.PositionCalculate(baseCard.GetID(), enemyMinions.GetNumOfMinions())));
        }

        yield return new WaitForSeconds(1);
    }

    public bool IsUnder()
    {
        return isUnder;
    }

}
