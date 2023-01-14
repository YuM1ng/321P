using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MonoBehaviourSingleton<T> : ScriptableObject where T : ScriptableObject
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if(results.Length == 0)
                {
                    Debug.LogError($"No instance of {typeof(T)} found");
                    return null;
                }
                else if(results.Length > 1)
                {
                    Debug.LogError($"More than 1 instance of {typeof(T)} found");
                    return null;
                }
                _instance = results[0];
            }
            return _instance;
        }
    }
}
