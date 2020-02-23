using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Minions playerMinions1;
    public Minions playerMinions2;

    // Start is called before the first frame update
    void Start()
    {
        playerMinions1.GameStart();
        playerMinions2.GameStart();
        NextTurn(playerMinions1);
    }

    public void NextTurn(Minions minion)
    {
        StartCoroutine(minion.RandomlyAttack());
    }

    private bool IsGameOver()
    {
        return false;
    }

    public void GameOver(string str)
    {
#if UNITY_EDITOR
        Debug.Log(str + " is lost");
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
