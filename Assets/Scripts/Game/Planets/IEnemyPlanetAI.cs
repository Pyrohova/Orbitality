using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyPlanetAI
{
    Vector2 GetDirection(Transform selectedPlanet);
}
