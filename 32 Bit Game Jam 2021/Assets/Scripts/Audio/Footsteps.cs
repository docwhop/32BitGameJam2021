using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform groundCheck;
    public LayerMask groundMask;
    [SerializeField]
    private float groundDistance = .4f;
    [SerializeField]
    private AudioSource stepSource;

    [SerializeField]
    private AudioClip[] clips;

    CharacterController controller;
    bool isGrounded;

    public void StepSound(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);

        stepSource.clip = clips[randomIndex];
        stepSource.volume = Random.Range(0.8f, 1f);
        stepSource.pitch = Random.Range(0.8f, 1.1f);
        stepSource.Play();
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (controller.isGrounded == true && controller.velocity.magnitude > 0f && stepSource.isPlaying == false)
        {
            StepSound(clips);
        }
    }
}
