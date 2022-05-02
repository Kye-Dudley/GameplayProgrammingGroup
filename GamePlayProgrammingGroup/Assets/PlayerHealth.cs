using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //Player Health 
    public float playerHealth;
    public float playerMaxHealth;

    public GameObject healthBarUI;
    public Slider slider;

    int damage = 2;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private Transform player;

    void Start()
    {
        playerHealth = playerMaxHealth;
        slider.value = calculateHealth();
    }

    void Update()
    {
        //Health Handling 
        slider.value = calculateHealth();
        if (playerHealth < playerMaxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (playerHealth <= 0)
        {
            Destroy(gameObject);
        }

        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }

    float calculateHealth()
    {
        return playerHealth / playerMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Projectile")
        {
            TakeDamage();
        }
    }

    private void TakeDamage()
    {

        playerHealth -= damage;
        if (playerHealth <= 0) Invoke(nameof(DestroyPlayer), 0.5f);

    }

    private void DestroyPlayer()
    {
        

    }
}
