using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains all rocket prefabs as values and their types as keys for quick access.
/// </summary>
[CreateAssetMenu]
public class RocketCollection : ScriptableObject
{
    [Serializable]
    public class RocketPrefab
    {
        public RocketController prefab;
        public RocketType type;
    }
    private Dictionary<RocketType, RocketController> dictionary;

    public RocketPrefab[] AllPrefabs;

    public RocketController this[RocketType type]
    {
        get
        {
            Init();
            return dictionary[type];
        }
    }

    public int Count
    {
        get
        {
            Init();
            return AllPrefabs.Length;
        }
    }

    private void Init()
    {
        if (dictionary != null)
            return;

        dictionary = new Dictionary<RocketType, RocketController>();
        foreach (var o in AllPrefabs)
        {
            dictionary[o.type] = o.prefab;
        }
    }
}