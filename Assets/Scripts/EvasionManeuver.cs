using UnityEngine;
using System.Collections;

public class EvasionManeuver : MonoBehaviour {


    public float dodge;
    public float smoothing;
    public float tilt;
    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    private float currentSpeed;
    private Rigidbody rigidBody;
    private float targetManeuver;
	
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        currentSpeed = rigidBody.velocity.z;
        StartCoroutine(Evade());
	}

    IEnumerator Evade ()
    {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true)
        {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.x, maneuverWait.y));
        }
    }
	
	
	void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards(rigidBody.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rigidBody.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rigidBody.position = new Vector3
            (
            Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
            );

        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);

	}
}
