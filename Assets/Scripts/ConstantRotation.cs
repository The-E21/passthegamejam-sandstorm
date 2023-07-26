using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public Vector3 axis;
    public float amount = 360;

    private void Update()
    {
        transform.Rotate(axis, amount * Time.deltaTime);
    }
}
