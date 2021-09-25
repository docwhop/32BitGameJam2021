using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;

    public static GameManager Instance { get { return instance; } }


    [field: SerializeField]
    public float MouseSensitivity { get; set; }

    public bool IsGamePaused { get; set; }

    public int KeyCount { get; set; }

    [SerializeField]
    Transform introSpawnPoint, MainSpawnPoint;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public Vector3 GetSpawnpoint(ControllerColliderHit hit)
    {
        Debug.Log("hit: " + hit.gameObject.tag);
        if (hit.gameObject.tag == "IntroHoneyLava")
        {
            return introSpawnPoint.position;
        }
        else if (hit.gameObject.tag == "MainHoneyLava")
        {
            return MainSpawnPoint.position;
        }
        return Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
