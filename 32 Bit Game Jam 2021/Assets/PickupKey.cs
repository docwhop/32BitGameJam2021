using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Key")
        {
            Debug.Log("Hit a key");
            GameManager.Instance.KeyCount++;
            EventManager.Instance.KeyPickedUp();
            Destroy(hit.gameObject);
        }
    }
}
