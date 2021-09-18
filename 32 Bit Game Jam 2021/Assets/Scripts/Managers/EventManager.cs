using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    public delegate void OnKeyPickupDelegate();
    public static event OnKeyPickupDelegate keyPickupEvent;

    private static EventManager _instance;

    public static EventManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }


    public void KeyPickedUp()
    {
        keyPickupEvent?.Invoke();
    }
}
