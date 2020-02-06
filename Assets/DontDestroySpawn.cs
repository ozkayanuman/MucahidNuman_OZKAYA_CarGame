using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroySpawn : MonoBehaviour
{
    private static DontDestroySpawn dontDestroy;
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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
