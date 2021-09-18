using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundMask;

    [SerializeField]
    private float groundDistance = .4f;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip land;
    public bool alreadyPlayed = false;
    bool isGrounded;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (isGrounded)
        {
            if (!alreadyPlayed)
            {
                AudioManager.Instance.RandomizePitchAndVolume(source);
                source.PlayOneShot(land);
                alreadyPlayed = true;
            }

        }
        else
        {
            alreadyPlayed = false;
        }
    }

}
