using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    [SerializeField] private PlanetFactory planetFactory;

    [SerializeField] public List<GameObject> EnemyPlanets { get; private set; }
    [SerializeField] public GameObject PlayerPlanet { get; private set; }
    [SerializeField] public GameObject Sun { get; private set; }

    public Action<GameResult> OnPlayerDestroyed;
    public Action<GameResult> OnAllEnemiesDestroyed;

    public void GenerateWorld()
    {
        var allPlanets = planetFactory.GeneratePlanets();
        PlayerPlanet = allPlanets.Item1;
        EnemyPlanets = allPlanets.Item2;
    }

    public void ResetWorld()
    {
        planetFactory.Reset();
        if (EnemyPlanets.Count > 0)
        {
            foreach (GameObject planet in EnemyPlanets)
            {
                Destroy(planet);
            }
        }
        EnemyPlanets = new List<GameObject>();
        Destroy(PlayerPlanet);
    }

    public void OnPlayerDies()
    {
        ResetWorld();
        OnPlayerDestroyed?.Invoke(GameResult.Lost);
    }

    public void DestroyPlanet(GameObject selectedPlanet)
    {
        for (int i = 0; i < EnemyPlanets.Count; i++)
            if (EnemyPlanets[i] == selectedPlanet)
            {
                Destroy(EnemyPlanets[i]);
                EnemyPlanets.RemoveAt(i);
                i--;
            }
        if (GameStateController.GameState == GameState.Running && EnemyPlanets.Count == 0)
        {
            ResetWorld();
            OnAllEnemiesDestroyed?.Invoke(GameResult.Won);
        }

    }

    public void Start()
    {
        GenerateWorld();
    }
}
