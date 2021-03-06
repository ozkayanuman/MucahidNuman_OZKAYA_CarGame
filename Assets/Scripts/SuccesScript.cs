﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccesScript : MonoBehaviour
{

    /// <summary>
    ///  It is checked whether the cars have reached the destination and creates a new car
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.tag = "Succes";
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;       //making car speed zero
            collision.gameObject.GetComponent<CarController>().enabled = false;             //disable input for this car
            collision.gameObject.AddComponent<LevelReloader>();                             //making this car obstacles
            GameController.gameController.stage++;

            DontDestroyOnLoad(collision.gameObject);

            GameController.gameController.successedCars.Add(collision.gameObject);          //adding this car to parked car list to remove new scene 


            if (GameController.gameController.stage > 8)    //Determines when the EndLevelPanel is displayed 
            {
                GameObject myUIManagerGameObject = GameObject.FindGameObjectWithTag("UserInterface");
                myUIManagerGameObject.GetComponent<UIController>().ShowEndLevelPanel();
            }
            else
            {
                GameController.gameController.isSucces = true;
                GameController.gameController.ReLoadCurrentScene();
            }
        }
    }
}
