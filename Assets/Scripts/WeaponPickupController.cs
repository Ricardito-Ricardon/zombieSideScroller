using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickupController : MonoBehaviour
{
    [SerializeField] int whichWeapon;
    [SerializeField] AudioClip pickupClip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.GetComponent<InventoryManager>().ActivateWeapon(whichWeapon);
            Destroy(transform.root.gameObject);
            AudioSource.PlayClipAtPoint(pickupClip, transform.position);
        }
    }
}
