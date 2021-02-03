using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketManager : MonoBehaviour
{
    [SerializeField] private RocketCollection rocketCollection;
    [SerializeField] private RocketType[] allowedRocketTypes;
    [SerializeField] private GameObject rocketParent;

    private List<RocketController> existedRockets;

    public void CreateRocket(RocketType rocketType, Transform planet, Vector3 direction)
    {
        var newRocket = Instantiate(rocketCollection[rocketType]);
        newRocket.transform.position = planet.position;
        newRocket.gameObject.transform.SetParent(rocketParent.transform);
        newRocket.Initialize(direction, planet.gameObject);

        existedRockets.Add(newRocket);
    }

    public void RemoveRocket(RocketController rocket)
    {
        for (int i = 0; i < existedRockets.Count; i++)
        {
            if (existedRockets[i] == rocket)
            {
                Destroy(existedRockets[i].gameObject);
                existedRockets.RemoveAt(i);
                i--;
            }
        }
    }

    public RocketController GetRandomRocket()
    {
        var randomType = allowedRocketTypes[Random.Range(0, allowedRocketTypes.Length)];
        return rocketCollection[randomType];
    }

    public void Reset()
    {
        foreach (RocketController rocket in existedRockets)
        {
            RemoveRocket(rocket);
        }
        existedRockets = new List<RocketController>();

    }

    private void Awake()
    {
        existedRockets = new List<RocketController>();
    }
}
