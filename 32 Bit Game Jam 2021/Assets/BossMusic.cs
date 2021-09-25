using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMusic : MonoBehaviour
{

    [SerializeField]
    private AudioClip clip;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            AudioManager.Instance.PlayMusic(clip);
            Destroy(gameObject);
        }
    }
}
