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

    int damage = 10;
    public GameObject remains;

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
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = calculateHealth();

        if (enemyHealth < enemyMaxHealth)
        {
            healthBarUI.SetActive(true);
        }

        if (enemyHealth < 0)
        {
            Destroy(gameObject);
        }

        if (enemyHealth > enemyMaxHealth)
        {
            enemyHealth = enemyMaxHealth;
        }
        if (enemyHealth < 0)
        {
            Instantiate(remains, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    float calculateHealth()
    {
        return enemyHealth / enemyMaxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            TakeDamage();
        }

    }

    private void TakeDamage()
    {

        enemyHealth -= damage;
        if (enemyHealth < 50)
        {
            Instantiate(remains, transform.position, transform.rotation);
            Destroy(gameObject);
            gameObject.name.Equals("CubeRemains");
            //gameObject.SetActive(true);
        }

        if (enemyHealth <= 0) Invoke(nameof(DestroyEnemy), 0.5f);
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);

    }

    void Spawn()
    {
        if (enemyHealth < 0)
        {
            return;
        }

        int spawnPointIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(enemySmall, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
