using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("Health Bars")]
    [SerializeField] Slider healthBar;
    [SerializeField] Transform healthLossBar;
    [SerializeField] Text healthAmountText;
    //[SerializeField] float lerpTimeStep = 0.2f;

    int pastHealth;
    //float t = 0f;
    Coroutine co;
    private void Start()
    {
        healthLossBar.localScale = new Vector3(1.0f, healthLossBar.localScale.y, healthLossBar.localScale.z);
    }

    public void setMaxHealth(int maxHealth)
    {
        healthBar.maxValue = maxHealth;
        pastHealth = maxHealth;
    }

    public void setCurrentHealth(int health)
    {
        /*
        if (co != null)
        {
            StopCoroutine(co);
            co = null;
        }*/
        healthLossBar.localScale = new Vector3(health * 0.01f, healthLossBar.localScale.y, healthLossBar.localScale.z);
        healthBar.value = health;
        healthAmountText.text = health + "%";
        //co = StartCoroutine(LerpPastHealthToCurrent(pastHealth, health, lerpTimeStep));
    }

    public void setPastHealth(int pastHealth)
    {
        this.pastHealth = pastHealth;
    }

    /* Unused coroutine damage animation
    IEnumerator LerpPastHealthToCurrent(int pastH, int currentH, float timeStep)
    {
        while (t < 1.0f)
        {
            healthLossBar.localScale = new Vector3(Mathf.Lerp(pastH, currentH, t) * 0.01f, healthLossBar.localScale.y, healthLossBar.localScale.z);
            yield return new WaitForSeconds(timeStep);
            t += timeStep;
        }
        if (t >= 1.0f)
        {
            healthLossBar.localScale = new Vector3(Mathf.Lerp(pastH, currentH, 1f) * 0.01f, healthLossBar.localScale.y, healthLossBar.localScale.z);
            t = 0f;
        }
    }*/
}
