using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmCameraFollow : MonoBehaviour
{
    // Mouselook.cs handles following on x rotation since rotating the camera roatates the transform of the player,
    // but looking up and down doesn't. So, this script handles that part. 

    // Start is called before the first frame update
    private float mouseSensitivity = 100f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
    }
}
