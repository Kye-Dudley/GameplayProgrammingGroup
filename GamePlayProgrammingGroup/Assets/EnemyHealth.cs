using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public float enemyHealth;
    public float enemyMaxHealth;

    public GameObject healthBarUI;
    public Slider slider;

    int damage = 50;
    public GameObject remains;
    public GameObject player;

    private bool isAttackingAlready = false;

    //New Enemy
    public GameObject enemySmall;
    public float spawnTime = 0;
    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        enemyHealth = enemyMaxHealth;
        slider.value = calculateHealth();
        InvokeRepeating("Spawn", spawnTime, spawnTime);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = calculateHealth();

        if (enemyHealth < enemyMaxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (enemyHealth > enemyMaxHealth)
        {
            enemyHealth = enemyMaxHealth;
        }
        if (enemyHealth <= 0)
        {
            if (remains != null)
            {
                Instantiate(remains, transform.position, transform.rotation);
            }
            Destroy(gameObject);
        }
        if(player.GetComponentInChildren<Attacking>().isAttackingEnemy && !isAttackingAlready)
        {
            isAttackingAlready = true;
            Debug.Log("attacking cube");
            TakeDamage();
            player.GetComponentInChildren<Attacking>().isAttackingEnemy = false;
            isAttackingAlready = false;
        }
    }

    float calculateHealth()
    {
        return enemyHealth / enemyMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("attacking cube 1");
        if (other.tag == "PlayerWeapon")
        {
            player.GetComponentInChildren<Attacking>().inRangeOfEnemy = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PlayerWeapon")
        {
            player.GetComponentInChildren<Attacking>().inRangeOfEnemy = false;
        }
    }

    private void TakeDamage()
    {
        enemyHealth -= damage;
    }

    void Spawn()
    {
        if (enemyHealth < 0)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        //Instantiate(enemySmall, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
