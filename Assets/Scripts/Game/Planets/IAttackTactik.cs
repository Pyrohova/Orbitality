using UnityEngine;
using System.Collections;

public interface IAttackTactik
{
    void Initialize(RocketType rocketType, float cooldown, GameObject readyToAttackIcon);
    void Shoot(Vector2 direction);
}
