using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGameScreenController : MonoBehaviour, IScreenController
{
    [SerializeField] private GameObject inGameScreen;
    [SerializeField] private Button pauseButton;

    [SerializeField] private Slider hpSlider;
    [SerializeField] private Slider cooldownSlider;

    public GameSceneUIScreenTypes Type => GameSceneUIScreenTypes.InGame;

    public void Show()
    {
        inGameScreen.SetActive(true);
    }

    public void Hide()
    {
        inGameScreen.SetActive(false);
    }

    public void Awake()
    {
        Hide();
        pauseButton.onClick.AddListener(() => {
            GameUIManager.Instance.ShowScreen(GameSceneUIScreenTypes.Pause);
            Hide();
        });
    }
}
