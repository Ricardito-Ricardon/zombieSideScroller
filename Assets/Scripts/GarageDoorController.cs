using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoorController : MonoBehaviour
{

    [SerializeField] bool resetable;
    [SerializeField] GameObject door;
    [SerializeField] GameObject gear;
    [SerializeField] bool startOpen;

    bool firstTrigger = false;
    bool open = true;
    Animator doorAnim;
    Animator gearAnim;
    AudioSource doorAudio;

    // Start is called before the first frame update
    void Start()
    {
        doorAnim = door.GetComponent<Animator>();
        gearAnim = gear.GetComponent<Animator>();
        doorAudio = door.GetComponent<AudioSource>();

        if(!startOpen)
        {
            open = false;
            doorAnim.SetTrigger("DoorTrigger");
            doorAudio.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !firstTrigger)
        {
            if (!resetable) firstTrigger = true;
            doorAnim.SetTrigger("DoorTrigger");
            open = !open;
            //gearAnim.SetTrigger("gearRotating");
            doorAudio.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
