﻿using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody objectRigidBody;
    public float tilt;
    public Boundary boundary;

    public float speed;

    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        objectRigidBody.velocity = movement * speed;

        objectRigidBody.position = new Vector3
            (
                Mathf.Clamp(objectRigidBody.position.x, boundary.xMin, boundary.xMax), 
                0.0f, 
                Mathf.Clamp(objectRigidBody.position.z, boundary.zMin, boundary.zMax)
            );

        objectRigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, objectRigidBody.velocity.x * -tilt);
    }
	
}
