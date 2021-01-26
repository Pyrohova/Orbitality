using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : Singleton<GameUIManager>
{
    private Dictionary<GameSceneUIScreenTypes, IScreenController> screenControllers;

    public void ShowScreen(GameSceneUIScreenTypes screenToShow)
    {
        foreach (GameSceneUIScreenTypes currentScreenType in screenControllers.Keys)
        {
            if (screenToShow == currentScreenType)
            {
                screenControllers[screenToShow].Show();
            }
            else
            {
                screenControllers[screenToShow].Hide();
            }
        }
    }

    protected override void InitializeManager()
    {
        InGameScreenController inGameScreenController = GetComponent<InGameScreenController>();
        PauseScreenController pauseScreenController = GetComponent<PauseScreenController>();
        GameOverScreenController gameOverScreenController = GetComponent<GameOverScreenController>();

        screenControllers = new Dictionary<GameSceneUIScreenTypes, IScreenController>
        {
            {inGameScreenController.Type, inGameScreenController },
            {pauseScreenController.Type, pauseScreenController },
            {gameOverScreenController.Type, gameOverScreenController}
        };
    }
}
