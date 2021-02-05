using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines objects that will get damage from the shot.
/// </summary>
public interface IHittable
{
    void AcceptDamage(float damage);
}
