using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenController : MonoBehaviour, IScreenController
{
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Text infoText;

    [Header("Game result texts")]
    [SerializeField] private string wonText = "you won!";
    [SerializeField] private string lostText = "you lost!";

    public GameSceneUIScreenTypes Type => GameSceneUIScreenTypes.GameOver;

    public void ShowResult(GameResult result)
    {
        switch(result)
        {
            case GameResult.Won:
                infoText.text = wonText;
                break;
            case GameResult.Lost:
                infoText.text = lostText;
                break;
        }
    }

    public void Show()
    {
        gameOverScreen.SetActive(true);
    }

    public void Hide()
    {
        gameOverScreen.SetActive(false);
    }

    public void Awake()
    {
        Hide();
        mainMenuButton.onClick.AddListener(()=> { });
    }

}
