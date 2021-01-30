using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("In Game Screen")]
    [SerializeField] private GameObject inGameScreen;

    [SerializeField] private Button pauseButton;
    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider cooldownSlider;

    [Header("Game Over Screen")]
    [SerializeField] private GameObject gameOverScreen;

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Text infoText;

    [SerializeField] private string wonText = "you won!";
    [SerializeField] private string lostText = "you lost!";

    [Header("Pause Screen")]
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button mainMenuButtonPauseScreen;

    private GameObject currentScreen = null;

    private void HidePreviousScreenAndShowNew(GameObject newScreen)
    {
        if (currentScreen != null)
            currentScreen.SetActive(false);
        currentScreen = newScreen;
        currentScreen.SetActive(true);
    }

    public void ShowInGameScreen()
    {
        HidePreviousScreenAndShowNew(inGameScreen);
    }

    public void ShowPauseScreen()
    {
        HidePreviousScreenAndShowNew(pauseScreen);
    } 

    public void ShowGameOverScreen(GameResult result)
    {
        HidePreviousScreenAndShowNew(gameOverScreen);

        switch (result)
        {
            case GameResult.Won:
                infoText.text = wonText;
                break;
            case GameResult.Lost:
                infoText.text = lostText;
                break;
        }
    }

    private void Awake()
    {
        var solarSystemManager = ServiceLocator.GetInstance().GetSolarSystemManager();

        inGameScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);

        solarSystemManager.OnAllEnemiesDestroyed += (result) => { ShowGameOverScreen(result); };
        solarSystemManager.OnPlayerDestroyed += (result) => { ShowGameOverScreen(result); };

        pauseButton.onClick.AddListener(() => {
            ShowPauseScreen();
            GameStateController.Pause();
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneManager.LoadMainMenuScene();
            GameStateController.Finish();
            solarSystemManager.ResetWorld();
        });

        continueButton.onClick.AddListener(() => {
            ShowInGameScreen();
            GameStateController.Resume();
        });

        mainMenuButtonPauseScreen.onClick.AddListener(() => {
            SceneManager.LoadMainMenuScene();
            GameStateController.Finish();
            solarSystemManager.ResetWorld();
        });

    }
}
