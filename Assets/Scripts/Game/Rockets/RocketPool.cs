using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RocketPool : MonoBehaviour
{
    [SerializeField] private RocketController[] rocketPrefabs;
    [SerializeField] private int maxRocketEachPlanetQuantity = 2;
    [SerializeField] private GameObject rocketParent;
    [SerializeField] private Vector2 spawnPoint;

    // save all created rockets sorted by their types
    private Dictionary<RocketType, List<RocketController>> existedRockets;

    public void AcquireRocket(RocketType rocketType)
    {
        var selectedListByType = existedRockets[rocketType];
        for (int i = 0; i < selectedListByType.Count; i++)
        {
            var selectedRocket = selectedListByType[i].GetComponent<RocketController>();
            if (!selectedRocket.IsEnabled)
            {
                selectedRocket.Enable();
                break;
            }
        }
    }

    public void ReleaseRocket(RocketController rocket)
    {
        var selectedListByType = existedRockets[rocket.Type];
        for (int i = 0; i < selectedListByType.Count; i++)
        {
            var selectedRocket = selectedListByType[i].GetComponent<RocketController>();
            if (selectedRocket == rocket)
            {
                selectedRocket.transform.position = spawnPoint;
                break;
            }
        }
    }

    public void GenerateRockets(int planetQuantity, List<RocketType> selectedTypes)
    {
        foreach (RocketController rocket in rocketPrefabs)
        {
            existedRockets.Add(rocket.Type, new List<RocketController>());
        }

        // for each of the selected rocket types
        for (int i = 0; i < selectedTypes.Count; i++)
        {
           // find requested rocket prefab by it's type
           for (int j = 0; j < rocketPrefabs.Length; j++)
           {
               if (rocketPrefabs[j].Type == selectedTypes[i])
               {
                    int counter = 0;
                    // repeat max permitted time
                    while (counter < maxRocketEachPlanetQuantity)
                    {
                        // create new rocket with required type and save it into the appropriate list
                        RocketController newRocket = Instantiate(rocketPrefabs[j]);
                        existedRockets[selectedTypes[i]].Add(newRocket);
                        newRocket.gameObject.transform.SetParent(rocketParent.transform);
                        newRocket.transform.position = spawnPoint;
                    }
               }
           }
        }
    }


    public List<Tuple<RocketType, float>> DistributeRocketsForPlanets(int quantity)
    {
        List<Tuple<RocketType, float>> distibutedRocketTypes = new List<Tuple<RocketType, float>>();
        List<RocketType> selectedTypes = new List<RocketType>();

        for (int i = 0; i < quantity; i++)
        {
            var randomRocket = rocketPrefabs[UnityEngine.Random.Range(0, rocketPrefabs.Length)];
            var rocketTypeAndReloadingTime = Tuple.Create(randomRocket.Type, randomRocket.Cooldown);
            distibutedRocketTypes.Add(rocketTypeAndReloadingTime);
            selectedTypes.Add(rocketTypeAndReloadingTime.Item1);
        }

        GenerateRockets(quantity, selectedTypes);
        return distibutedRocketTypes;
    }
}
