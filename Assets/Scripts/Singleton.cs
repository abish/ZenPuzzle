using UnityEngine;
using System;
using System.Collections.Generic;

public class Singleton<T> : MonoBehaviour where T:Singleton<T>
{
    private static T instance;
	private static object _lock = new object();

    public static T Instance
    {
        get
        {
			if (applicationIsQuitting)
			{
				Debug.LogWarning("[Singleton] Instance " + typeof(T) + " already destroyed on application quit.");
			}

	        lock(_lock)
			{
				if (instance == null)
	            {
	                instance = (T)FindObjectOfType(typeof(T));
					if (FindObjectsOfType(typeof(T)).Length > 1)
					{
						Debug.LogWarning("[Singleton] Instance should never be more than 1 singleton");
					}

	                if (instance == null)
	                {
	                    instance = CreateInstance();
	                }
	            }

	            return instance;
			}
        }
    }

    protected static T CreateInstance()
    {
        GameObject singleton = new GameObject(typeof(T).ToString());
        instance = singleton.AddComponent<T>();

        DontDestroyOnLoad(singleton);

        Debug.Log("Created Singleton:" + singleton);

        return instance;
    }

	private static bool applicationIsQuitting = false;

	public virtual void OnDestroy ()
	{
		applicationIsQuitting = true;
	}
}
