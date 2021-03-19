using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] HealthBar healthBarObj;

    [Header("Health Amount")]
    [SerializeField] public int maxHealth = 100;
    [SerializeField] public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        healthBarObj.setMaxHealth(maxHealth);
        healthBarObj.setPastHealth(currentHealth);
        healthBarObj.setCurrentHealth(currentHealth);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            healthBarObj.setPastHealth(currentHealth);
            takeDamage(Random.Range(1, 20));
            healthBarObj.setCurrentHealth(currentHealth);
        }
    }

    public void takeDamage(int damageAmount)
    {
        healthBarObj.setPastHealth(currentHealth);
        currentHealth -= damageAmount;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        healthBarObj.setCurrentHealth(currentHealth);
    }
}
