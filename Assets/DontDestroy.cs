using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private static DontDestroy dontDestroy;
    private void Awake()
    {
        if (DontDestroy.dontDestroy == null)
        {
            DontDestroy.dontDestroy = this;
        }
        else
        {
            if (this != DontDestroy.dontDestroy)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(dontDestroy.gameObject);
    }
 
}
