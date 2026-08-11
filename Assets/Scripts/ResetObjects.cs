using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ResetObjects : MonoBehaviour
{
    private Vector3 origPosition;
    private Quaternion origRotation;
    private Rigidbody2D origRigidBody;

    private void Start()
    {
        origPosition = transform.position;
        origRotation = transform.rotation;
        origRigidBody = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        ResetScene.ResetWorld += Reset;
    }
    private void OnDisable()
    {
        ResetScene.ResetWorld -= Reset;
    }

    private void Reset()
    {
        origRigidBody.linearVelocity = Vector3.zero;
        transform.position = origPosition;
        transform.rotation = origRotation;
    }
}
