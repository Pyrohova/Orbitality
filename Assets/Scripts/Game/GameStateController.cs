using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameStateController: MonoBehaviour
{
    public GameState GameState { get; private set; } = GameState.NotRunning;

    public void Pause()
    {
        if (GameState == GameState.Running)
        {
            Time.timeScale = 0;
            GameState = GameState.Paused;
        }
    }

    public void Resume()
    {
        if (GameState == GameState.Paused)
        {
            Time.timeScale = 1;
            GameState = GameState.Running;
        }
    }

    public void Start()
    {
        GameState = GameState.Running;
    }

    public void Finish()
    {
        if (GameState == GameState.Paused)
        {
            Time.timeScale = 1;
        }
        GameState = GameState.NotRunning;
    }

}
