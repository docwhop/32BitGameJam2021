using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRigRotation : MonoBehaviour
{

    [SerializeField]
    Vector3 weaponRoation;
    // Start is called before the first frame update
    void Start()
    {
        transform.localEulerAngles = new Vector3(weaponRoation.x, weaponRoation.y, weaponRoation.z);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
