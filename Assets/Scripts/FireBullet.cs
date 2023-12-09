using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireBullet : MonoBehaviour
{
    [SerializeField] float timeBetweenBullets = 0.15f;
    [SerializeField] GameObject projectile;

    //Bullet info
    [SerializeField] Slider playerAmmoSlider;
    [SerializeField] int maxRounds;
    [SerializeField] int startingRounds;
    int remainingRounds;

    float nextBullet;

    //audio info
    AudioSource gunMuzzleAS;
    [SerializeField] AudioClip shootSound;
    [SerializeField] AudioClip reloadSound;

    //graphic info
    [SerializeField] Sprite weaponSprite;
    [SerializeField] Image weaponImage;

    // Start is called before the first frame update
    void Awake()
    {
        nextBullet = 0f;
        remainingRounds = startingRounds;
        playerAmmoSlider.maxValue = maxRounds;
        playerAmmoSlider.value = remainingRounds;
        gunMuzzleAS = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        playerController myPlayer = transform.root.GetComponent<playerController>();

        if(Input.GetAxisRaw("Fire1")>0 && nextBullet<Time.time && remainingRounds>0)
        {
            nextBullet = Time.time + timeBetweenBullets;
            Vector3 rot;
            if (myPlayer.GetFacing() == -1f)
            {
               rot = new Vector3(0, -90, 0);
            }
            else rot = new Vector3(0, 90, 0);

            Instantiate(projectile, transform.position, Quaternion.Euler(rot));

            playASound(shootSound);

            remainingRounds -= 1;
            playerAmmoSlider.value = remainingRounds;
        }
    }

    public void reload()
    {
        remainingRounds = maxRounds;
        playerAmmoSlider.value = remainingRounds;
        playASound(reloadSound);

    }

    void playASound(AudioClip playTheSound)
    {
        gunMuzzleAS.clip = playTheSound;
        gunMuzzleAS.Play();
    }

    public void InitalizeWeapon()
    {
        gunMuzzleAS.clip = reloadSound;
        gunMuzzleAS.Play();
        nextBullet = 0;
        playerAmmoSlider.maxValue = maxRounds;
        playerAmmoSlider.value = remainingRounds;
        weaponImage.sprite = weaponSprite;
    }

}
