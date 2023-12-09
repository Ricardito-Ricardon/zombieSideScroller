using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeScript : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] float knockBack;
    [SerializeField] float knockBackRadius;
    [SerializeField] float meleeRate;

    float nextMelee;

    int shootableMask;

    Animator myAnim;
    playerController myPC;

    // Start is called before the first frame update
    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        myAnim = transform.root.GetComponent<Animator>();
        myPC = transform.root.GetComponent<playerController>();
        nextMelee = 0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float melee = Input.GetAxis("Fire2");

        if(melee > 0 && nextMelee < Time.time && !(myPC.getRunning()))
        {
            myAnim.SetTrigger("gunMelee");
            nextMelee = Time.time + meleeRate;

            //do damage
            Collider[] attacked = Physics.OverlapSphere(transform.position, knockBackRadius, shootableMask);

            int i = 0;
            while(i< attacked.Length)
            {
                if (attacked[i].tag == "Enemy")
                {
                    EnemyHealth doDamage = attacked[i].GetComponent<EnemyHealth>();
                    doDamage.AddDamage(damage);
                    doDamage.damageFX(transform.position, transform.localEulerAngles);
                }
                i++;
            }
        }
    }
}
