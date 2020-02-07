using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    public Transform EndLevelPanel;

    [SerializeField]
    public Button PlayAgainButton,
        EndLevelPanelQuitAppButton,
        EndLevelPanelPlayAgainButton,
        EndLevelPanelNextLevelButton;

    public Text EndLevelPanelLevelNoText, CurrentLevelText;

    /// <summary>
    /// Assign function to buttons and update text areas.
    /// setting current level
    /// </summary>
    private void Start()
    {
        UpdateCurrentLevelText();
        PlayAgainButton.onClick.AddListener(PlayAgain);
        EndLevelPanelQuitAppButton.onClick.AddListener(QuitApp);
        EndLevelPanelPlayAgainButton.onClick.AddListener(PlayAgain);
        EndLevelPanelNextLevelButton.onClick.AddListener(NextLevel);
    }


    #region EndLevelPanel Functions
    /// <summary>
    /// Shows End Level Panel
    /// </summary>
    public void ShowEndLevelPanel()
    {
        UpdateLevelTextInEndPanel();
        SetEndLevelPanelVisibility(true);
        GameController.gameController.FreezeGame();
    }

    /// <summary>
    /// To activate end level panel
    /// </summary>
    /// <param name="aVisibilityValue"></param>
    public void SetEndLevelPanelVisibility(bool aVisibilityValue)
    {
        EndLevelPanel.gameObject.SetActive(aVisibilityValue);
    }

    /// <summary>
    /// Quit Application
    /// </summary>
    private void QuitApp()
    {
        Application.Quit();
    }

    /// <summary>
    /// Performs the transition to the new section
    /// </summary>
    private void NextLevel()
    {
        GameController.gameController.ClearScene();
        GameController.gameController.LoadNextScene();
    }

    /// <summary>
    /// Replay section
    /// </summary>
    private void PlayAgain()
    {
        GameController.gameController.ClearScene();
        GameController.gameController.lastColor=Color.red;
        GameController.gameController.stage = 1;
        GameController.gameController.ReLoadCurrentScene();
    }

    /// <summary>
    /// Shows which level we have finished at the end of the level
    /// </summary>
    private void UpdateLevelTextInEndPanel()
    {
        EndLevelPanelLevelNoText.text = GameController.gameController.GetCurrentLevelNo().ToString();
    }
    #endregion


    #region UTILITIES
    /// <summary>
    /// Shows which level we playing on screen
    /// </summary>
    public void UpdateCurrentLevelText()
    {
        CurrentLevelText.text = GameController.gameController.GetCurrentLevelNo().ToString();
    }

    #endregion
}
