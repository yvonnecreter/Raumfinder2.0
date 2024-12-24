using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RotationController : MonoBehaviour
{
    public GameObject signatureObject;
    public Vector3 RotationVector;

    private void Update()
    {
        signatureObject.transform.Rotate(RotationVector * Time.deltaTime);
    }
}