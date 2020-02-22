using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    [SerializeField] protected int star;
    [SerializeField] protected int attack;
    [SerializeField] protected int health;
    [SerializeField] protected int maxHealth;
    [SerializeField] protected int baseAttack = 1;
    [SerializeField] protected int baseHealth = 1;
    [SerializeField] protected List<Race> races = new List<Race>();
    [SerializeField] protected bool death;
    [SerializeField] protected GameController gameController;
    [SerializeField] protected Minions minions;
    [SerializeField] protected Minions enemyMinions;

    [SerializeField] protected bool isTaunt = false;
    [SerializeField] protected bool isDivineShield = false;
    [SerializeField] protected bool isWindfury = false;
    [SerializeField] protected bool isMegaWindfury = false;
    [SerializeField] protected bool isStealth = false;
    [SerializeField] protected bool haveDeathrattle = false;

    public MinionView minionView;
    public int deathPosition;

    public bool IsTaunt { get => isTaunt; set => isTaunt = value; }
    public bool IsDivineShield { get => isDivineShield; set => isDivineShield = value; }
    public bool IsWindfury { get => isWindfury; set => isWindfury = value; }
    public bool IsMegaWindfury { get => isMegaWindfury; set => isMegaWindfury = value; }
    public bool IsStealth { get => isStealth; set => isStealth = value; }
    public bool HaveDeathrattle { get => haveDeathrattle; set => haveDeathrattle = value; }

    void Awake()
    {
        if (GameObject.FindGameObjectsWithTag("GameController") != null)
        {
            gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Can't find GameController");
        }
        if (gameObject.GetComponent<MinionView>() != null)
        {
            minionView = gameObject.GetComponent<MinionView>();
        }
        else
        {
            Debug.Log("Can't find MinionView");
        }
    }

    public int GetCurrentAttack()
    {
        return attack;
    }

    public int GetCurrentHealth()
    {
        return health;
    }
    public int GetBaseAttack()
    {
        return baseAttack;
    }

    public int GetBaseHealth()
    {
        return baseHealth;
    }
    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsDie()
    {
        return death;
    }

    public bool IsRace(Race.RaceType race)
    {
        bool flag = false;
        foreach (Race tmp in races)
        {
            if (tmp.IsRace(race)) flag = true;
        }
        return flag;
    }

    public void ChangeAttack(int value)
    {
        attack += value;
        if (attack <= 0) attack = 0;
        minionView.HealthUpdate();
    }

    public void ChangeHealth(int value)
    {
        health += value;
        maxHealth += value;
        if (health <= 0) Death();
        minionView.AttackUpdate();
    }

    public void ChangeLeftHealth(int value)
    {
        health += value;
        if (health <= 0) Death();
        minionView.HealthUpdate();
    }

    public bool Summon(Minions minions)
    {
        this.minions = minions;
        enemyMinions = minions.enemyMinions;
        if (GameObject.FindGameObjectsWithTag("GameController") != null)
        {
            gameController = GameObject.FindGameObjectsWithTag("GameController")[0].GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Can't find GameController");
        }
        if (gameObject.GetComponent<MinionView>() != null)
        {
            minionView = gameObject.GetComponent<MinionView>();
        }
        else
        {
            Debug.Log("Can't find MinionView");
        }
        return false;
    }

    public bool Death()
    {
        death = true;
        return false;
    }

    public virtual IEnumerator Attack(BaseCard target)
    {
        yield return StartCoroutine(target.GetHurt(GetCurrentAttack()));
        yield return StartCoroutine(GetHurt(target.GetCurrentAttack()));        
    }

    public IEnumerator AttackAction(int targetID)
    {
        BaseCard target = enemyMinions.GetMinionByID(targetID);

        yield return StartCoroutine(minionView.AttackMove(PositionCalculate(targetID, enemyMinions.GetNumOfMinions())));
        StartCoroutine(minionView.Move(PositionCalculate(GetID(), minions.GetNumOfMinions())));

        yield return StartCoroutine(Attack(target));

        StartCoroutine(minions.DeathSettlement());
        
    }

    public IEnumerator MoveTo(int targetPosition)
    {
        StartCoroutine (minionView.Move(targetPosition));
        yield return 0;
    }

    public int PositionCalculate(int targetID,int total)
    {
        return (Minions.LIMIT_OF_MINIONS - total) + targetID * 2;
    }

    public virtual IEnumerator GetHurt(int damage)
    {
        if (isDivineShield)
        {
            minionView.ShowNumOfDamage(0);
            isDivineShield = false;
            minionView.KeyWordUpdate();
            yield return 0;
        }
        else
        {
            ChangeLeftHealth(-damage);
            minionView.ShowNumOfDamage(damage);
            yield return StartCoroutine(TakeDamage());
        }
    }

    public virtual bool BattleCry()
    {
        return false;
    }

    public virtual IEnumerator DeathRattle()
    {
        yield return 0;
    }

    public virtual bool Aura()
    {
        return false;
    }

    public virtual bool AuraUpdate()
    {
        return false;
    }

    public virtual bool AuraRemove()
    {
        return false;
    }

    //以下为各种扳机触发

    public virtual bool SummonMinion()
    {
        return false;
    }

    public virtual bool MinionDeath()
    {
        return false;
    }

    public virtual IEnumerator TakeDamage()
    {
        yield return 0;
    }

    public virtual bool KillMinion()
    {
        return false;
    }

    //一些辅助函数

    public void NotShowNumOfDamage()
    {
        minionView.NotShowNumOfDamage();
    }

    public void SetInvisible()
    {
        minionView.SetInvisible();
    }

    public void DeathRattleView()
    {
        if (haveDeathrattle)
        {
            minionView.DeathRattle();
        }
    }   
    
    public int GetID()
    {
        return minions.minions.IndexOf(this);
    }

    public bool IsUnder()
    {
        return minions.IsUnder();
    }
}
