using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour
{
    private Rigidbody objectRigidBody;

    public float tumble;

    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        objectRigidBody.angularVelocity = Random.insideUnitSphere * tumble;
    }

}
