using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class BuildingHealth : MonoBehaviour
{
    public float startHealth = 100;
    private float currentHealth;

    public Image healthBar;

    bool isDead;

    void Start()
    {
        currentHealth = startHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;      
        healthBar.fillAmount = currentHealth / startHealth;
     
        if (currentHealth <= 0 && !isDead)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Debug.Log("ME IS DEAD");
        isDead = true;
    }

}

