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

    private CharacterMovement movement;
    private bool reset;
    public GameObject teleportTo;

    int damage = 2;
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

        if (playerHealth > playerMaxHealth)
        {
            playerHealth = playerMaxHealth;
        }
    }

    private void FixedUpdate()
    {
        if (reset)
        {
            movement.enabled = true;
            playerHealth = 100;
            reset = false;
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
        movement = GetComponentInChildren<CharacterMovement>();
        movement.enabled = false;
        transform.position = teleportTo.transform.position;
        transform.rotation = teleportTo.transform.rotation;

        reset = true;
    }


}
