using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask groundMask;

    [SerializeField]
    private float groundDistance = .4f;
    [SerializeField]
    private float speed = 12f;
    [SerializeField]
    private float gravity = -9.81f;
    [SerializeField]
    private float jumpHeight = 3f;
    [SerializeField]
    private float hoverTime = 2f;
    [SerializeField]
    private AudioSource hoverSource;
    [SerializeField]
    private AudioClip[] hoverClips;
    public bool alreadyPlayed = false;
    private float fadeTime = 1f;

    CharacterController controller;

	Vector3 velocity;
    float timeHovering;
    bool canHover;
	bool isGrounded;

    public void HoverSound(params AudioClip[] clips)
    {
        int randomIndex = Random.Range(0, clips.Length);

        hoverSource.clip = clips[randomIndex];
        hoverSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        canHover = true;
        timeHovering = 0f;
        controller = GetComponent<CharacterController>();
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            timeHovering = 0f;
            hoverSource.Stop();
            alreadyPlayed = false;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (controller)
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        if(Input.GetButton("Jump") || Input.GetKey(KeyCode.Backspace))
        {
            if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                canHover = true;
                hoverSource.Stop();
            }
            //double jump bee hover 
            else if(timeHovering < hoverTime && canHover) 
            {
                timeHovering += Time.deltaTime;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                if (!alreadyPlayed)
                {
                    StartCoroutine(AudioHelper.FadeIn(hoverSource, fadeTime));
                    HoverSound(hoverClips);
                    alreadyPlayed = true;

                }
            }
            // finished with hover
            else
            {
                canHover = false;
                timeHovering = 0f;
            }
        }

        velocity.y += gravity * Time.deltaTime;

		// because of equation deltaY = 1/2g*t^2, we multiply by deltaTime again
		controller.Move(velocity * Time.deltaTime);
	}

	public void AddForce(Vector3 _force)
	{
		velocity += _force;
	}

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "IntroHoneyLava" || hit.gameObject.tag == "MainHoneyLava")
        {
            Vector3 spawnPoint = GameManager.Instance.GetSpawnpoint(hit);
            transform.position = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z);
        }
    }
}
