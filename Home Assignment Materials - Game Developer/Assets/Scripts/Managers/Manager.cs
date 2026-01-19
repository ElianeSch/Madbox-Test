using UnityEngine;

public abstract class Manager<T> : MonoBehaviour where T : Manager<T>
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindAnyObjectByType<T>();
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }

        _instance = (T)this;
    }
}