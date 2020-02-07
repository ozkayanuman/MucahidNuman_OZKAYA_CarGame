using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySpawn : MonoBehaviour
{
    private static DontDestroySpawn dontDestroy;

    /// <summary>
    /// When reloading scene it makes don't destroy our Spawn Points
    /// </summary>
    private void Awake()
    {
        
        if (DontDestroySpawn.dontDestroy == null)
        {
            DontDestroySpawn.dontDestroy = this;
        }
        else
        {
            if (this != DontDestroySpawn.dontDestroy)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(dontDestroy.gameObject);
    }
   
}
