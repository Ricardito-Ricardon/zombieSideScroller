using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float enemyMaxHealth;
    [SerializeField] float damageModifier;
    [SerializeField] GameObject damageParticles;
    [SerializeField] bool drops;
    [SerializeField] GameObject drop;
    [SerializeField] AudioClip deathSound;
    [SerializeField] bool canBurn;
    [SerializeField] float burnDamage;
    [SerializeField] float burnTime;
    [SerializeField] GameObject burnEffects;

    bool onFire;
    float nextBurn;
    float burnInterval = 1f;
    float endBurn;

    float currentHealth;

    [SerializeField] Slider enemyHealthIndicator;

    AudioSource enemyAS;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = enemyMaxHealth;
        enemyHealthIndicator.maxValue = enemyMaxHealth;
        enemyHealthIndicator.value = currentHealth;
        enemyAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(onFire && Time.time > nextBurn)
        {
            AddDamage(burnDamage);
            nextBurn += burnInterval;
        }
        if(onFire && Time.time > endBurn)
        {
            onFire = false;
            burnEffects.SetActive(false);
        }
    }

    public void AddDamage(float damage)
    {
        enemyHealthIndicator.gameObject.SetActive(true);
        damage = damage*damageModifier;
        if (damage <= 0f) return;
        currentHealth -= damage;
        enemyHealthIndicator.value = currentHealth;
        enemyAS.Play();
        if (currentHealth <= 0) MakeDead();
    }

    public void damageFX(Vector3 point, Vector3 rotation)
    {
        Instantiate(damageParticles, point, Quaternion.Euler(rotation));
    }

    public void AddFire()
    {
        if(!canBurn) return;
        onFire = true;
        burnEffects.SetActive(true);
        endBurn = Time.time+burnTime;
        nextBurn = Time.time + burnInterval;
    }

    void MakeDead()
    {
        //turn off movement
        ZombieController aZombie = GetComponentInChildren<ZombieController>();
        if(aZombie != null)
        {
            aZombie.RagdollDeath();
        }

        AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.10f);

        Destroy(gameObject.transform.root.gameObject);
        if(drops) Instantiate(drop, transform.position, Quaternion.identity);
    }
}
