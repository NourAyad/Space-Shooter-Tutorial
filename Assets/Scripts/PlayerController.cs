using UnityEngine;
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

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    private AudioSource audioSource;

    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject clone = 
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
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
