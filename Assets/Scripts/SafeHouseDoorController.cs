using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SafeHouseDoorController : MonoBehaviour
{
    AudioSource safeDoorAudio;

    bool inSafe = false;

    //HUD
    public TextMeshProUGUI endGameText;
    public RestartGame theGameController;

    // Start is called before the first frame update
    void Start()
    {
        safeDoorAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && inSafe == false)
        {
            Animator safeDoorAnim = GetComponentInChildren<Animator>();
            safeDoorAnim.SetTrigger("SafeHouseTrigger");
            safeDoorAudio.Play();
            endGameText.text = "Safe House";
            Animator endGameAnim = endGameText.GetComponent<Animator>();
            endGameAnim.SetTrigger("EndGame");
            theGameController.restartTheGame();
            inSafe = true;
        }
    }
}
