using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class PlanetFactory : MonoBehaviour
{
    [SerializeField] private Sprite[] planetSprites;

    [SerializeField] private float[] sizeRange = {0.11f, 0.15f};
    [SerializeField] private float distanceToSun = 2f;
    [SerializeField] private  float distanceBetweeenPlanets = 1.5f;

    [SerializeField] private float[] planetRotationSpeedsRange = { 0.25f, 0.75f  };
    [SerializeField] private float[] maxPlanetHPRange = { 50, 100 };

    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private GameObject planetParent;

    private GameObject sun;

    private bool[] planetWithSuchSpriteExists;

    private RocketManager rocketManager;


    public GameObject CreatePlayerPlanet(int planetIndex)
    {
        return CreatePlanet(PlanetType.Player, planetIndex);
    }

    public GameObject CreateEnemyPlanet(int planetIndex)
    {
        return CreatePlanet(PlanetType.Enemy, planetIndex);
    }

    private GameObject CreatePlanet(PlanetType type, int planetIndex)
    {
        GameObject newPlanet = Instantiate(planetPrefab);
        newPlanet.transform.SetParent(planetParent.transform);

        PlanetInitializationValues startValues = new PlanetInitializationValues();
        startValues.image = GenerateRandomSprite();
        startValues.scale = UnityEngine.Random.Range(sizeRange[0], sizeRange[1]);
        startValues.speed = UnityEngine.Random.Range(planetRotationSpeedsRange[0], planetRotationSpeedsRange[1]);
        startValues.sunPosition = sun.transform.position;
        startValues.distanceToSun = new Vector2(sun.transform.position.x + distanceToSun + distanceBetweeenPlanets * planetIndex, sun.transform.position.y);
        startValues.maxHP = UnityEngine.Random.Range(maxPlanetHPRange[0], maxPlanetHPRange[1]);

        startValues.planetType = type;
        if (type == PlanetType.Player)
        {
            newPlanet.AddComponent<PlayerPlanetAttack>();
        }
        else
        {
            newPlanet.AddComponent<EnemyPlanetAttack>();
        }

        var randomRocket = rocketManager.GetRandomRocket();
        startValues.rocketType = randomRocket.GetRocketType();
        startValues.reloadingTime = randomRocket.GetCooldown();

        PlanetController planetController = newPlanet.GetComponent<PlanetController>();
        planetController.Initialize(startValues);

        return newPlanet;
    }

    // to prevent copies
    private Sprite GenerateRandomSprite()
    {
        int rand;
        do
        {
            rand = UnityEngine.Random.Range(0, planetSprites.Length);
        }
        while (planetWithSuchSpriteExists[rand]);

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
        
        rocketManager = ServiceLocator.GetInstance().GetRocketManager();
        sun = ServiceLocator.GetInstance().GetSolarSystemManager().GetSun();
    }
}
