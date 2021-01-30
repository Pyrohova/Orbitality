using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFactory : MonoBehaviour
{
    [SerializeField] private Sprite[] planetSprites;
    [SerializeField] private float[] sizes = {0.12f, 0.13f, 0.15f, 0.14f, 0.11f };
    [SerializeField] private float[] distanceToSun = {2f, 3.45f, 5f, 6.7f, 8.2f };
    [SerializeField] private float[] planetRotationSpeeds = { 5f, 2f, 4.3f, 7f, 6f  };
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private GameObject planetParent;

    private GameObject sun;

    private bool[] planetWithSuchSpriteExists;

    private const int MAX_PLANETS_QUANTITY = 5;
    private const int MIN_PLANETS_QUANTITY = 2;

    private RocketPool rocketPool;


    public Tuple<GameObject, List<GameObject>> GeneratePlanets()
    {
        GameObject planet = null;
        List<GameObject> enemies = GenerateNewPlanetsWithStartValues();
        return Tuple.Create(planet, enemies);
    }

    private List<GameObject> GenerateNewPlanetsWithStartValues()
    {
        int quantity = UnityEngine.Random.Range(MIN_PLANETS_QUANTITY, MAX_PLANETS_QUANTITY);

        var distributedRockets = rocketPool.DistributeRocketsForPlanets(quantity);
        List<GameObject> planets = new List<GameObject>();

        for (int i = 0; i < quantity; i++)
        {
            GameObject newPlanet = Instantiate(planetPrefab);
            PlanetInitializationValues startValues = new PlanetInitializationValues();
            startValues.image = GenerateRandomSprite();
            startValues.scale = sizes[i];
            startValues.speed = planetRotationSpeeds[i];
            startValues.sunPosition = sun.transform.position;
            startValues.distanceToSun = new Vector2(sun.transform.position.x + distanceToSun[i], sun.transform.position.y);
            startValues.rocketType = distributedRockets[i].Item1;
            startValues.reloadingTime = distributedRockets[i].Item2;

            PlanetController planetController = newPlanet.GetComponent<PlanetController>();
            planetController.Initialize(startValues);
            planets.Add(newPlanet);
        }
        return planets;
    }

    // to prevent copies
    private Sprite GenerateRandomSprite()
    {
        int rand = 0;
        do
        {
            rand = UnityEngine.Random.Range(0, planetSprites.Length);
        }
        while (!planetWithSuchSpriteExists[rand]);

        planetWithSuchSpriteExists[rand] = true;
        return planetSprites[rand];
    }

    public void Reset()
    {
        planetWithSuchSpriteExists = new bool[planetSprites.Length];
    }

    private void Awake()
    {
        Reset();
        rocketPool = ServiceLocator.GetInstance().GetRocketPool();
        sun = ServiceLocator.GetInstance().GetSolarSystemManager().Sun;
    }
}
