using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages generating planets and controls alive planets quantity.
/// </summary>
public class SolarSystemManager : MonoBehaviour
{
    [SerializeField] private PlanetFactory planetFactory;

    private List<GameObject> planets;
    private GameObject playerPlanet;

    [SerializeField] private GameObject sun;

    public Action<GameResult> OnPlayerDestroyed;
    public Action<GameResult> OnAllEnemiesDestroyed;

    private const int MAX_PLANETS_QUANTITY = 5;
    private const int MIN_PLANETS_QUANTITY = 3;

    private void GenerateWorld()
    {
        int planetQuantity = UnityEngine.Random.Range(MIN_PLANETS_QUANTITY, MAX_PLANETS_QUANTITY);
        int playerPlanetIndex = UnityEngine.Random.Range(0, planetQuantity);

        planets = new List<GameObject>();
        for (int i = 0; i < planetQuantity; i++)
        {
            GameObject newPlanet;
            if (i == playerPlanetIndex)
            {
                newPlanet = planetFactory.CreatePlayerPlanet(i);
                playerPlanet = newPlanet;
            }
            else
            {
                newPlanet = planetFactory.CreateEnemyPlanet(i);
            }
            planets.Add(newPlanet);
        }
    }

    public List<GameObject> GetAllPlanetsExceptSelected(GameObject selectedPlanet)
    {
        List<GameObject> requiredPlanets = new List<GameObject>();
        foreach (GameObject planet in planets)
        {
            if (planet != selectedPlanet)
            {
                requiredPlanets.Add(planet);
            }
        }

        return requiredPlanets;
    }

    public GameObject GetSun()
    {
        return sun;
    }

    public List<GameObject> GetPlanets()
    {
        return planets;
    }

    public GameObject GetPlayerPlanet()
    {
        return playerPlanet;

    }

    public void ResetWorld()
    {
        planetFactory.Reset();
        if (planets.Count > 0)
        {
            foreach (GameObject planet in planets)
            {
                Destroy(planet);
            }
        }
        planets = new List<GameObject>();
    }

    public void DestroyPlanet(GameObject selectedPlanet)
    {
        if (selectedPlanet != playerPlanet)
        {
            for (int i = 0; i < planets.Count; i++)
                if (planets[i] == selectedPlanet)
                {
                    Destroy(planets[i]);
                    planets.RemoveAt(i);
                    i--;
                }
            if (planets.Count == 1)
            {
                ResetWorld();
                OnAllEnemiesDestroyed?.Invoke(GameResult.Won);
            }
        }
        else
        {
            ResetWorld();
            OnPlayerDestroyed?.Invoke(GameResult.Lost);
        }

    }

    public void Awake()
    {
        GenerateWorld();
    }
}
