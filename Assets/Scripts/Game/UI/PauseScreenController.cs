using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreenController : MonoBehaviour, IScreenController
{
    [SerializeField] private GameObject pauseScreen;

    [SerializeField] private Button continueButton;
    [SerializeField] private Button mainMenuButton;

    public GameSceneUIScreenTypes Type => GameSceneUIScreenTypes.Pause;

    public void Show()
    {
        pauseScreen.SetActive(true);
    }

    public void Hide()
    {
        pauseScreen.SetActive(false);
    }

    public void Awake()
    {
        Hide();
        continueButton.onClick.AddListener(() => { });
        mainMenuButton.onClick.AddListener(() => { });
    }
}
