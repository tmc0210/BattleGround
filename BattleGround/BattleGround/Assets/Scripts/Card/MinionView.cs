using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionView : MonoBehaviour
{
    public Text attackText;
    public Text healthText;
    public Image shield;
    public Image damaged;
    public Text damageText;
    public Image deathRattle;
    public Canvas canvas;

    public float speedOfAttack = 10.0f;
    public float speedOfMove = 2.0f;

    public Vector3 direction;
    private BaseCard baseCard;
    private Text attackTextComponent;
    private Text healthTextComponent;
    private Text damageTextComponent;

    void Start()
    {
        baseCard = GetComponent<BaseCard>();
        if (baseCard == null)
        {
            Debug.Log("Can't find BaseCard");
        }
        if (attackText == null || healthText == null)
        {
            Debug.Log("Can't find Text");
        }
        attackTextComponent = attackText.GetComponent<Text>();
        healthTextComponent = healthText.GetComponent<Text>();
        damageTextComponent = damageText.GetComponent<Text>();
        if (attackTextComponent == null || healthTextComponent == null || damageText == null)
        {
            Debug.Log("Can't find TextComponent");
        }

        ViewUpdate();
    }

    public void AttackUpdate()
    {
        int currentAttack = baseCard.GetCurrentAttack();
        attackTextComponent.text = currentAttack + "";
        if (currentAttack > baseCard.GetBaseAttack())
        {
            attackTextComponent.color = Color.green;
        }
        else
        {
            attackTextComponent.color = Color.white;
        }
    }

    public void HealthUpdate()
    {
        int currentHealth = baseCard.GetCurrentHealth();
        healthTextComponent.text = currentHealth + "";
        if (currentHealth < baseCard.GetMaxHealth())
        {
            healthTextComponent.color = Color.red;
        }
        else
        {
            if (currentHealth > baseCard.GetBaseHealth())
            {
                healthTextComponent.color = Color.green;
            }
            else
            {
                healthTextComponent.color = Color.white;
            }
        }
    }

    public void KeyWordUpdate()
    {
        if (baseCard.IsKeyWord(KeyWord.KeyWordType.DivineShield))
        {
            shield.gameObject.SetActive(true);
        }
        else
        {
            shield.gameObject.SetActive(false);
        }
    }

    public void ViewUpdate()
    {
        AttackUpdate();
        HealthUpdate();
        KeyWordUpdate();
    }

    public void ShowNumOfDamage(int damage)
    {
        damaged.gameObject.SetActive(true);
        damageText.text = "-" + damage;
        damageText.gameObject.SetActive(true);
    }

    public void NotShowNumOfDamage()
    {
        damaged.gameObject.SetActive(false);
        damageText.text = "";
        damageText.gameObject.SetActive(false);
    }

    public void DeathRattle()
    {
        deathRattle.gameObject.SetActive(true);
    }

    public void SetInvisible()
    {
        canvas.gameObject.SetActive(false);
    }

    public void Attack()
    {
        
    }

    void Update()
    {
        //transform.position.x += direction * speedOfAttack;   
    }
}