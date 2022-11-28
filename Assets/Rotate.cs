using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    float z;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(5*Time.deltaTime, 5 * Time.deltaTime, 5 * Time.deltaTime);
        z = 1.5f*Mathf.Sin(Time.time / 10) - 2;
        transform.localScale = new Vector3(z, z, z);
    }
}
