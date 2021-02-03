using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceLocator : Singleton<ServiceLocator>
{
    [SerializeField] private SolarSystemManager solarSystemManager;
    [SerializeField] private RocketPool rocketPool;
    [SerializeField] private GameStateController gameStateController;
    [SerializeField] private InputController inputController;
    [SerializeField] private EnemyAIManager enemyAIManager;

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

    public GameStateController GetGameStateController()
    {
        return gameStateController;
    }

    public InputController GetInputController()
    {
        return inputController;
    }

    public EnemyAIManager GetEnemyAIManager()
    {
        return enemyAIManager;
    }

}
