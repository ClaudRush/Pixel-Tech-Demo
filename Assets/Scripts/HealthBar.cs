using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private float delta;
    [SerializeField] private Health health;
    private float healthValue;
    private float currentHealth;
    void Start()
    {
        //health.onHealthChange += StartAnimationHealthBar;
        healthValue = health.GetHealth / (float)health.MaxHealth;
    }

    private void OnDestroy()
    {
        //health.onHealthChange -= StartAnimationHealthBar;
    }

    void Update()
    {
        currentHealth = health.GetHealth / (float)health.MaxHealth;

        if (currentHealth > healthValue)
            healthValue += delta;

        if (currentHealth < healthValue)
            healthValue -= delta;

        if (currentHealth < delta)
            healthValue = currentHealth;

        healthBar.fillAmount = healthValue;
    }
//    private void StartAnimationHealthBar(int healthPlayer)
//    {
//        StartCoroutine(AnimationHealthBar(healthPlayer));
//    }

//    public IEnumerator AnimationHealthBar(int healthPlayer)
//    {
//        float precentHealthPlayer = healthPlayer / 100.0f;
//        while (Mathf.Abs(healthBar.fillAmount - precentHealthPlayer) > 0.001)
//        {
//            healthBar.fillAmount = Mathf.Lerp(healthBar.fillAmount, precentHealthPlayer, delta*delta);
//            yield return null;
//        }
//        healthBar.fillAmount = precentHealthPlayer;
//        yield return null;
//    }
}
