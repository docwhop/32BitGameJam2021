using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantMove : MonoBehaviour
{
	public Vector3 PositionAmount;
	public Vector3 RotationAmount;
	public Vector3 ScaleAmount;

    void FixedUpdate()
    {
		transform.Rotate(RotationAmount * Time.deltaTime);
		transform.position = transform.position + PositionAmount * Time.deltaTime;
		transform.localScale = transform.localScale + ScaleAmount * Time.deltaTime;
    }
}
