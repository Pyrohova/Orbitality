using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : Singleton<ServiceLocator>
{
    [SerializeField] private SolarSystemManager solarSystemManager;
    [SerializeField] private RocketPool rocketPool;

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
