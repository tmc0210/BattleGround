﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCard : MonoBehaviour
{
    [SerializeField] private int star;
    [SerializeField] private int attack;
    [SerializeField] private int health;
    [SerializeField] private int maxHealth;
    [SerializeField] private int baseAttack;
    [SerializeField] private int baseHealth;
    [SerializeField] private List<Race> races = new List<Race>();
    [SerializeField] private List<KeyWord> keyWords = new List<KeyWord>();
    [SerializeField] private bool death;
    private GameController gameController;
    private Minions minions;
    private MinionView minionView;

    void Start()
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

    public bool IsKeyWord(KeyWord.KeyWordType keyWord)
    {
        bool flag = false;
        foreach(KeyWord tmp in keyWords)
        {
            if (tmp.IsKeyWord(keyWord)) flag = true;
        }
        return flag;
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

    public void GetKeyWord(KeyWord.KeyWordType keyWordType)
    {
        if (!IsKeyWord(keyWordType))
        {
            keyWords.Add(new KeyWord(keyWordType));
            minionView.KeyWordUpdate();
        }
    }

    public void LoseKeyWord(KeyWord.KeyWordType keyWordType)
    {
        if (IsKeyWord(keyWordType))
        {
            keyWords.Remove(new KeyWord(keyWordType));
            minionView.KeyWordUpdate();
        }
    }

    public bool Summon(Minions minions)
    {
        this.minions = minions;
        minionView.ViewUpdate();
        return false;
    }

    public bool Death()
    {
        return false;
    }

    public bool Attack(BaseCard target)
    {
        GetHurt(target.GetCurrentAttack());
        target.GetHurt(GetCurrentAttack());
        return false;
    }

    public bool GetHurt(int damage)
    {

        return false;
    }

    public bool BattleCry()
    {
        return false;
    }

    public bool DealthRattle()
    {
        return false;
    }

    public bool Holo()
    {
        return false;
    }

    public bool _Holo()
    {
        return false;
    }

    //以下为各种扳机触发

    public bool SummonMinion()
    {
        return false;
    }

    public bool MinionDeath()
    {
        return false;
    }

    public bool TakeDamage()
    {
        return false;
    }

    public bool KillMinion()
    {
        return false;
    }

}