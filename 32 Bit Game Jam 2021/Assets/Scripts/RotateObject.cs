using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour
{
    [SerializeField]
    private Vector3 RotateAmount;  // degrees per second to rotate in each axis. Set in inspector.

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(RotateAmount * Time.deltaTime);
    }
}