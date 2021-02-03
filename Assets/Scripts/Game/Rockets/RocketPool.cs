using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RocketPool : MonoBehaviour
{
    [SerializeField] private RocketController[] rocketPrefabs;
    [SerializeField] private int maxRocketEachPlanetQuantity = 1;
    [SerializeField] private GameObject rocketParent;
    [SerializeField] private Vector2 spawnPoint;

    // save all created rockets sorted by their types
    private Dictionary<RocketType, List<RocketController>> existedRockets;


    public void GenerateRockets(List<RocketType> selectedTypes)
    {
        existedRockets = new Dictionary<RocketType, List<RocketController>>();
        foreach (RocketController rocket in rocketPrefabs)
        {
            existedRockets.Add(rocket.GetRocketType(), new List<RocketController>());
        }

        // for each of the selected rocket types
        for (int i = 0; i < selectedTypes.Count; i++)
        {
            // find requested rocket prefab by it's type
            for (int j = 0; j < rocketPrefabs.Length; j++)
            {
                if (rocketPrefabs[j].GetRocketType() == selectedTypes[i])
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
            var rocketTypeAndReloadingTime = Tuple.Create(randomRocket.GetRocketType(), randomRocket.GetCooldown());
            distibutedRocketTypes.Add(rocketTypeAndReloadingTime);
            selectedTypes.Add(rocketTypeAndReloadingTime.Item1);
        }
        GenerateRockets(selectedTypes);

        return distibutedRocketTypes;
    }

    public void AcquireRocket(RocketType rocketType, Vector2 newPosition, Vector3 direction)
    {
        var selectedListByType = existedRockets[rocketType];
        for (int i = 0; i < selectedListByType.Count; i++)
        {
            var selectedRocket = selectedListByType[i].GetComponent<RocketController>();
            if (!selectedRocket.IsEnabled)
            {
                selectedRocket.Enable(direction);
                selectedRocket.gameObject.transform.position = newPosition;
                break;
            }
        }
    }

    public void ReleaseRocket(RocketController rocket)
    {
        var selectedListByType = existedRockets[rocket.GetRocketType()];
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

    public void Reset()
    {
        foreach (RocketType rocketType in existedRockets.Keys)
        {
            foreach (RocketController rocket in existedRockets[rocketType])
            {
                Destroy(rocket.gameObject);
            }
        }
        existedRockets = new Dictionary<RocketType, List<RocketController>>();

    }

}
