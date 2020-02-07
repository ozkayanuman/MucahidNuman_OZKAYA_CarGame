using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    
    public Color lastColor = Color.red;

    public static GameController gameController;
    

    public GameObject[] destinations;
    public GameObject[] spawnPoints;
    public int stageCount;
    public int stage = 1;
    public bool isSucces;

    public List<GameObject> successedCars;

    /// <summary>
    /// Making game controller to singelton and when reloading scene it makes don't destroy our game controller
    /// </summary>
    private void Awake()
    {
        destinations[0].SetActive(true);
        if (GameController.gameController == null)
        {
            GameController.gameController = this;
        }
        else
        {
            if (this != GameController.gameController)
            {
                Destroy(gameObject);
            }
        }
        DontDestroyOnLoad(GameController.gameController.gameObject);
    }

    /// <summary>
    /// Seting Destination points.
    /// Showing current destination point.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void SetDestination(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < destinations.Length; i++)
        {
            if (i == stage - 1)
            {
                destinations[i].SetActive(true);
                Debug.Log("Stage :" + stage);

            }
            else
            {
                destinations[i].SetActive(false);
            }
        }
        SceneManager.sceneLoaded -= SetDestination;
    }

    /// <summary>
    /// Setting spawn points.
    /// Starting current spawn point.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void SetSpawnPoint(Scene scene, LoadSceneMode mode)
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i == stage - 1)
            {
                GameObject car = GameObject.FindGameObjectWithTag("Player");
                car.transform.position = spawnPoints[i].transform.position;
                car.transform.rotation = spawnPoints[i].transform.rotation;
            }

        }
        SceneManager.sceneLoaded -= SetSpawnPoint;
    }

    /// <summary>
    /// Game freezes moving objects when it needs to stop.
    /// </summary>
    public void FreezeGame()
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        Time.timeScale = 0;
    }

    /// <summary>
    /// To make the game objects moving again.
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    public void DefreezeGame(Scene scene, LoadSceneMode mode)
    {
        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        playerGO.GetComponent<CarController>().EnablePlayerMovement();

     
        Time.timeScale = 1;
        SceneManager.sceneLoaded -= DefreezeGame;
    }

    /// <summary>
    ///Sets a different car color for each level
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void SetCarColor(Scene scene, LoadSceneMode mode)
    {

        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        playerGO.GetComponent<SpriteRenderer>().color = lastColor;

        SceneManager.sceneLoaded -= SetCarColor;            //When function complete is running, removes itself from the work list
    }

    #region SCENE MANAGEMENT

    /// <summary>
    /// Used for situations where the scene needs to be reloaded (collision with level bounds etc.)
    /// </summary>
    public void ReLoadCurrentScene()
    {
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (isSucces)
        {
            isSucces = false;
            lastColor = new Color(
                Random.Range(0f, 1f),
                Random.Range(0f, 1f),
                Random.Range(0f, 1f)
                );
        }

        SceneManager.sceneLoaded += SetCarColor;        //After scene loaded run this functions.
        SceneManager.sceneLoaded += DefreezeGame;
        SceneManager.sceneLoaded += SetDestination;
        SceneManager.sceneLoaded += SetSpawnPoint;

        SceneManager.LoadScene(currentSceneIndex);

    }
    /// <summary>
    /// Used to switch to new level.
    /// </summary>
    public void LoadNextScene()
    {
        stage = 1;
        lastColor = Color.red;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneInex = currentSceneIndex + 1;

        SceneManager.sceneLoaded += DefreezeGame;
        SceneManager.LoadScene(nextSceneInex);
    }

    /// <summary>
    /// Give current level number.
    /// </summary>
    /// <returns></returns>
    public int GetCurrentLevelNo()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        return currentSceneIndex;
    }

    /// <summary>
    ///  Clears the screen for the new level.(for ex. old cars from old levels.)
    /// </summary>
    public void ClearScene()
    {
        foreach (var item in successedCars)
        {
            Destroy(item);
        }
    }
    #endregion



}
