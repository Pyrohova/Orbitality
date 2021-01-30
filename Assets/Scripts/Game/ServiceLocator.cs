using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : MonoBehaviour
{
    [SerializeField] private SolarSystemManager solarSystemManager;
    [SerializeField] private RocketPool rocketPool;

    private static ServiceLocator Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public static ServiceLocator GetInstance()
    {
        return Instance;
    }

    public SolarSystemManager GetSolarSystemManager()
    {
        return solarSystemManager;
    }

    public RocketPool GetRocketPool()
    {
        return rocketPool;
    }

}
