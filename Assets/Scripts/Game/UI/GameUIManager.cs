using System.Collections;
using System;
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
        {
            currentScreen.SetActive(false);
        }
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

    public void UpdatePlayerHealthBarValue(float newValue)
    {
        hpSlider.value = newValue;
    }

    public void UpdatePlayerCooldownBarValue(float cooldown)
    {
        StartCoroutine(UpdateCooldown(cooldown));
    }

    private IEnumerator UpdateCooldown(float cooldown)
    {
        float currentCooldown = cooldown;
        while (currentCooldown > 0)
        {
            currentCooldown -= 1;
            yield return new WaitForSeconds(1);
            cooldownSlider.value = currentCooldown / cooldown;
        }
    }

    private void Start()
    {
        hpSlider.value = 1;
        cooldownSlider.value = 0;

        var solarSystemManager = ServiceLocator.GetInstance().GetSolarSystemManager();
        var gameStateController = ServiceLocator.GetInstance().GetGameStateController();
        var rocketPool = ServiceLocator.GetInstance().GetRocketPool();

        inGameScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);

        solarSystemManager.OnAllEnemiesDestroyed += (result) => { ShowGameOverScreen(result); };
        solarSystemManager.OnPlayerDestroyed += (result) => { ShowGameOverScreen(result); };

        pauseButton.onClick.AddListener(() => {
            ShowPauseScreen();
            gameStateController.Pause();
        });

        mainMenuButton.onClick.AddListener(() => {
            SceneManager.LoadMainMenuScene();
            gameStateController.Finish();
            solarSystemManager.ResetWorld();
            rocketPool.Reset();
        });

        continueButton.onClick.AddListener(() => {
            ShowInGameScreen();
            gameStateController.Resume();
        });

        mainMenuButtonPauseScreen.onClick.AddListener(() => {
            SceneManager.LoadMainMenuScene();
            gameStateController.Finish();
            solarSystemManager.ResetWorld();
            rocketPool.Reset();
        });

        ShowInGameScreen();

        var playerPlanet = solarSystemManager.GetPlayerPlanet().GetComponent<PlanetController>();
        playerPlanet.OnHealthChanged += UpdatePlayerHealthBarValue;
        playerPlanet.OnCooldownStarted += UpdatePlayerCooldownBarValue;

    }
}
