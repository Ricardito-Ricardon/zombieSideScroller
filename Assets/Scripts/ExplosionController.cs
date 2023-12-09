using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    [SerializeField] Light explosionLight;
    [SerializeField] float power;
    [SerializeField] float radius;
    [SerializeField] float damage;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null) rb.AddExplosionForce(power, explosionPos, radius, 3.0f, ForceMode.Impulse);
            if(hit.tag == "Player")
            {
                PlayerHealth thePlayerHealth = hit.gameObject.GetComponent<PlayerHealth>();
                thePlayerHealth.addDamage(damage);
            }else if(hit.tag == "Enemy")
            {
                EnemyHealth theEnemyHealth = hit.gameObject.GetComponent<EnemyHealth>();
                theEnemyHealth.AddDamage(damage);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        explosionLight.intensity = Mathf.Lerp(explosionLight.intensity, 0f, 5 * Time.time);
    }
}
