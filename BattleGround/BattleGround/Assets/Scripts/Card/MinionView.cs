using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionView : MonoBehaviour
{
    public Text attackText;
    public Text healthText;
    public Image shield;
    public Image taunt;
    public Image damaged;
    public Text damageText;
    public Image deathRattle;
    public Canvas canvas;

    public float speedOfAttack = 0.8f;
    public float speedOfMove = 0.3f;

    public float leftX = - 8.4f;
    public float deltaX = 1.4f;
    public float y = 3.8f;

    public Vector3 direction;

    public BaseCard baseCard;
    private Text attackTextComponent;
    private Text healthTextComponent;
    private Text damageTextComponent;

    void Start()
    {        
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

        if (baseCard.IsUnder())
        {
            y = -y;
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
        if (baseCard.IsDivineShield)
        {
            shield.gameObject.SetActive(true);
        }
        else
        {
            shield.gameObject.SetActive(false);
        }
        if (baseCard.IsTaunt)
        {
            taunt.gameObject.SetActive(true);
        }
        else
        {
            taunt.gameObject.SetActive(false);
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

    public IEnumerator AttackMove(int targetPosition)
    {
        Vector3 target = new Vector3(leftX + deltaX * targetPosition, -y, 0);

        direction = (target - transform.localPosition).normalized;
        do
        {
            yield return StartCoroutine(MoveTo(direction));
        }
        while ((transform.localPosition - target).magnitude > 0.16);
        
        yield return 0;
    }

    public IEnumerator Move(int targetPosition)
    {
        Vector3 target = new Vector3(leftX + deltaX * targetPosition, 0, 0);
        direction = (target - transform.localPosition).normalized;
        do
        {
            yield return StartCoroutine(MoveTo(direction));
        }
        while ((transform.localPosition - target).magnitude > 0.16);
        PositionUpdating(targetPosition);
        yield return 0;
    }

    public IEnumerator MoveTo(Vector3 direction)
    {
        transform.Translate(direction * speedOfMove, Space.Self);
        yield return 0;
    }
    
    public void PositionUpdating(int targetPosition)
    {
        Vector3 target = new Vector3(leftX + deltaX * targetPosition, 0, 0);
        transform.localPosition = target;
    }
}