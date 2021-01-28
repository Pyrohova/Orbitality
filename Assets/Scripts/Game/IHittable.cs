using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
    void AcceptDamage(float damage);
}
