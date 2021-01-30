using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStateController
{
    public static GameState GameState { get; private set; } = GameState.NotRunning;

    public static void Pause()
    {
        if (GameState == GameState.Running)
        {
            Time.timeScale = 0;
            GameState = GameState.Paused;
        }
    }

    public static void Resume()
    {
        if (GameState == GameState.Paused)
        {
            Time.timeScale = 1;
            GameState = GameState.Running;
        }
    }

    public static void Start()
    {
        GameState = GameState.Running;
    }

    public static void Finish()
    {
        if (GameState == GameState.Paused)
        {
            Time.timeScale = 1;
        }
        GameState = GameState.NotRunning;
    }

}
