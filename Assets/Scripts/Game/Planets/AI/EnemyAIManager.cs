using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains attack strategies for enemy planets.
/// </summary>
public class EnemyAIManager : MonoBehaviour
{
    [SerializeField] private StrategyShootClosestPlanet strategyShootClosestPlanet;
    [SerializeField] private StrategyShootPlayerPlanet strategyShootPlayerPlanet;
    [SerializeField] private StrategyShootRandom strategyShootRandom;

    private List<IEnemyPlanetAttackStrategy> enemyPlanetStrategies;

    public StrategyShootClosestPlanet GetStrategyShootClosest()
    {
        return strategyShootClosestPlanet;
    }

    public StrategyShootPlayerPlanet GetStrategyShootPlayer()
    {
        return strategyShootPlayerPlanet;
    }

    public StrategyShootRandom GetStrategyShootRandom()
    {
        return strategyShootRandom;
    }

    public IEnemyPlanetAttackStrategy GetRandomEnemyStrategy()
    {
        return enemyPlanetStrategies[Random.Range(0, enemyPlanetStrategies.Count)];
    }


    public void Awake()
    {
        enemyPlanetStrategies = new List<IEnemyPlanetAttackStrategy>() {
            strategyShootClosestPlanet,
            strategyShootPlayerPlanet,
            strategyShootRandom
        };
    }
}
