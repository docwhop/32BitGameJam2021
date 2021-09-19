using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupKey : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip clip;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "Key")
        {
            Debug.Log("Hit a key");
            GameManager.Instance.KeyCount++;
            EventManager.Instance.KeyPickedUp();
            source.PlayOneShot(clip);
            Destroy(hit.gameObject);
        }
    }
}
