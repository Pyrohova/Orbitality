using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUIManager : MonoBehaviour
{
    [Header("Main Menu Screen")]
    [SerializeField] private GameObject mainMenuScreen;

    [SerializeField] private Button newGameButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        newGameButton.onClick.AddListener(()=> {
            SceneManager.LoadGameScene();
        });

        exitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }
}
