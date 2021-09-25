using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform1 : MonoBehaviour
{
    [SerializeField]
    private Animator controller;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            controller.SetBool("Platform1", true);
        }
    }
}
