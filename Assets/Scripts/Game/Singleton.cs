using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T Instance { get; private set; }

    protected virtual void CreateSingleton()
    {
        if (Instance == null)
        {
            Instance = this as T;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    protected virtual void InitializeManager()
    {
        
    }

    protected virtual void Awake()
    {
        CreateSingleton();
        InitializeManager();
    }
}

