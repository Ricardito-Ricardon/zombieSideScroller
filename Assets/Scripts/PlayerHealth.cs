using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float fullHealth;
    float currentHealth;

    [SerializeField] GameObject playerDeathFX;

    //HUD
    [SerializeField] Slider playerHealthSlider;
    public Image damagedScreen;
    Color flashColor = new Color(255f, 255f, 255f, 1f);
    float flashSpeed = 5f;
    bool damaged = false;
    public TextMeshProUGUI endGameText;
    public RestartGame theGameController;

    AudioSource playerAS;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = fullHealth;
        playerHealthSlider.maxValue = fullHealth;
        playerHealthSlider.value = currentHealth;

        playerAS = GetComponent<AudioSource>();
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

        playerAS.Play();

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
        Animator endGameAnim = endGameText.GetComponent<Animator>();
        endGameAnim.SetTrigger("EndGame");
        theGameController.restartTheGame();
    }
}
