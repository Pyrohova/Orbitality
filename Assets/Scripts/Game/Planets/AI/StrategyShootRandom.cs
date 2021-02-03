using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrategyShootRandom : MonoBehaviour, IEnemyPlanetAI
{
    public Vector2 GetDirection(Transform selectedPlanet)
    {
        return Random.insideUnitCircle.normalized;
    }

}
