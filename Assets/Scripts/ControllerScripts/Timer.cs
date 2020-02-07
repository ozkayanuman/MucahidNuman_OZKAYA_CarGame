using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text uiText;
    [SerializeField] private float mainTimer;

    private float timer;
    private bool canCount = true;
    private bool doOnce = false;
    private bool pressed = false;


    void Start()
    {
        timer = mainTimer;      //Determines the start time

    }
    void Update()
    {
        
        if (Input.anyKey)       //Checks that the screen is touched
        {
            pressed = true;
        }

        if (pressed)            //Time start when touch on screen
        {

            if (timer >= 0.0f && canCount)
            {
                timer -= Time.deltaTime;
                uiText.text = timer.ToString("F");
            }

            else if (timer <= 0.0f && !doOnce)
            {
                canCount = false;
                doOnce = true;
                uiText.text = "0.00";
                timer = 0.0f;
                TimeOver();
            }
        }
    }

    /// <summary>
    /// Determines what happens when the time expires
    /// </summary>
    void TimeOver()
    {
        GameController.gameController.ReLoadCurrentScene();
    }

}