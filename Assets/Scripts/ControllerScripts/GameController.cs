using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private Color lastColor = Color.red;

    public static GameController gameController;
    

    public GameObject[] destinations;
    public GameObject[] spawnPoints;
    public int stageCount;
    public int stage = 1;
    public bool isSucces;

    public List<GameObject> successedCars;


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
    /// Setting Destination pointsç
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
        //playerGO.GetComponent<CarController>().DisablePlayerMovement();
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

        //GameObject myUI = GameObject.FindGameObjectWithTag("UserInterface");
        //myUI.GetComponent<CarController>().UpdateCurrentLevelText();

        Time.timeScale = 1;
        SceneManager.sceneLoaded -= DefreezeGame;
    }

    /// <summary>
    ///  Allows determination of car colors
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    private void SetCarColor(Scene scene, LoadSceneMode mode)
    {

        GameObject playerGO = GameObject.FindGameObjectWithTag("Player");
        playerGO.GetComponent<SpriteRenderer>().color = lastColor;

        SceneManager.sceneLoaded -= SetCarColor;
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

        SceneManager.sceneLoaded += SetCarColor;
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
    ///  Clears the screen for the new level.
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
