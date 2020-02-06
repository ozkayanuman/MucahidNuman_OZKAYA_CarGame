using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReloader : MonoBehaviour
{
    /// <summary>
    /// Checks for collisions to obstacles and level bounds
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {

            GameController.gameController.ReLoadCurrentScene();
        }
    }
}
