using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaronRivendare : BaseCard
{
    public override bool Summon(Minions minions)
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
        minions.isRivendare = true;
        return true;
    }

    public override IEnumerator DeathRattle()
    {
        minions.isRivendare = false;
        yield return 0;
    }
}
