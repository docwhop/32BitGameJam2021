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

    public int BulletsInClip { get; set; }

    [SerializeField]
    Transform introSpawnPoint, MainSpawnPoint, TeleportSpawnPoint;
    [SerializeField]
    private AudioSource splashSource;
    [SerializeField]
    private AudioSource teleportSource;

    private void OnEnable()
    {
        EventManager.weaponChangedEvent += WeaponChanged;
    }

    private void OnDisable()
    {

        EventManager.weaponChangedEvent -= WeaponChanged;
    }

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
            splashSource.Play();
            return introSpawnPoint.position;
        }
        else if (hit.gameObject.tag == "MainHoneyLava")
        {
            splashSource.Play();
            return MainSpawnPoint.position;
        }
        else if (hit.gameObject.tag == "Teleport Platform")
        {
            teleportSource.Play();
            return TeleportSpawnPoint.position;
        }
        return Vector3.zero;
    }

    public void WeaponChanged(Weapon weapon)
    {
        if (weapon.WeaponName == WeaponName.NeedleGun)
        {
           
        }
        else if (weapon.WeaponName == WeaponName.HoneyLauncher)
        {
        }
        else if (weapon.WeaponName == WeaponName.Pollenator)
        {
        }
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
