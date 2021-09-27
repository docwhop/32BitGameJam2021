using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    GameObject[] keys;
    Animator animator;
    int keysToOpenDoor = 3;
    private void OnEnable()
    {
        EventManager.keyPickupEvent += OpenDoor;
    }

    private void OnDisable()
    {
        EventManager.keyPickupEvent -= OpenDoor;
    }
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        keys = GameObject.FindGameObjectsWithTag("Key");
    }
    private void OpenDoor()
    {
        Debug.Log("OpenDoor Called" + keys.Length + ", " + GameManager.Instance.KeyCount);

        //collected all the keys and open the door
        if(GameManager.Instance.KeyCount == keys.Length)
        {
			gameObject.SetActive(false);
        }
    }
}
