using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionView : MonoBehaviour
{
    public Text attackText;
    public Text healthText;
    public Image shield;

    private BaseCard baseCard;
    private Text attackTextComponent;
    private Text healthTextComponent;

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
        if (attackTextComponent == null || healthTextComponent == null)
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
}