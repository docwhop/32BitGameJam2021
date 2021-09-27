using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour
{
	public Vector3 PositionAmount;
	public Vector3 RotationAmount;

    void FixedUpdate()
    {
		transform.Rotate(RotationAmount);
		transform.position = transform.position + PositionAmount;
    }
}
