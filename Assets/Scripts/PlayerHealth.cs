using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float fullHealth;
    float currentHealth;

    public GameObject playerDeathFX;

    //HUD
    public Slider playerHealthSlider;
    public Image damagedScreen;
    Color flashColor = new Color(255f, 255f, 255f, 1f);
    float flashSpeed = 5f;
    bool damaged = false;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = fullHealth;
        playerHealthSlider.maxValue = fullHealth;
        playerHealthSlider.value = currentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        //are we hurt?
        if (damaged)
        {
            damagedScreen.color = flashColor;
        }else
        {
            damagedScreen.color = Color.Lerp(damagedScreen.color, Color.clear, flashSpeed*Time.deltaTime);
        }
        damaged = false;
    }

    public void addDamage(float damage)
    {
        currentHealth -= damage;
        playerHealthSlider.value = currentHealth;
        damaged = true;
        if(currentHealth < 0)
        {
            makeDead();
        }
    }

    public void addHealth(float health)
    {
        currentHealth += health;
        if(currentHealth>fullHealth) currentHealth = fullHealth;
        playerHealthSlider.value = currentHealth;
    }

    public void makeDead()
    {
        Instantiate(playerDeathFX, transform.position, Quaternion.Euler(new Vector3(-90, 0, 0)));
        damagedScreen.color = flashColor;
        Destroy(gameObject);
    }
}
