using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAnimation : MonoBehaviour
{
    float output, counter;
    [SerializeField]
    float movementAmount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;
        output = Mathf.Sin(counter);
        output *= movementAmount;
        transform.position = new Vector3(transform.position.x, transform.position.y + output, transform.position.z);
    }
}
