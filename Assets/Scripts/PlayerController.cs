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
    public SimpleTouchPad touch;
    public SimpleTouchAreaButton areaButton;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    private Quaternion calibrationQuaternion;

    private AudioSource audioSource;

    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        CalibrateAccellerometer();
    }

    void Update()
    {
        if (areaButton.CanFire() && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject clone = 
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
            audioSource.Play();
        }
    }

    void FixedUpdate()
    {
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");

        //Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        // Vector3 accelerationRaw = Input.acceleration;
        //Vector3 acceleration = FixAcceleration(accelerationRaw);
        //Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y);
        Vector2 direction = touch.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);
        objectRigidBody.velocity = movement * speed;

        objectRigidBody.position = new Vector3
            (
                Mathf.Clamp(objectRigidBody.position.x, boundary.xMin, boundary.xMax), 
                0.0f, 
                Mathf.Clamp(objectRigidBody.position.z, boundary.zMin, boundary.zMax)
            );

        objectRigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, objectRigidBody.velocity.x * -tilt);
    }

    void CalibrateAccellerometer ()
    {
        Vector3 accelerationSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);

    }

    Vector3 FixAcceleration (Vector3 accelearation)
    {
        Vector3 fixedAcceleration = calibrationQuaternion * accelearation;
        return fixedAcceleration;
    }
	
}
