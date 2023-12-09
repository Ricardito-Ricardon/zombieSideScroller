using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour
{
    [SerializeField] float restartTime;
    bool resetNow = false;
    float resetTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(resetNow && resetTime <= Time.time)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }

    public void restartTheGame()
    {
        resetNow = true;
        resetTime = restartTime + Time.time;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
