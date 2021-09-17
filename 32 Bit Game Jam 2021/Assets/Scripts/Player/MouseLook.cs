using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public Transform playerBody, playerArms;
    float xRotation;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        MoveCamera();
        MoveArms();
    }

    private void MoveCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * GameManager.Instance.MouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * GameManager.Instance.MouseSensitivity * Time.deltaTime;
        if (playerBody)
        {
            playerBody.Rotate(Vector3.up * mouseX);
        }
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    private void MoveArms()
    {



    }

}
