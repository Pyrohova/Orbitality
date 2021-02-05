using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains methods that gives necessary values for shooting.
/// </summary>
public interface IEnemyPlanetAttackStrategy
{
    /// <summary>
    /// Gives direction where weapon must shoot. 
    /// </summary>
    /// <param name="selectedPlanet"></param>
    /// <returns></returns>
    Vector2 GetDirection(Transform selectedPlanet);
}
