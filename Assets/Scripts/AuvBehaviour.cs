using UnityEngine;
using System.Collections;

public class AuvBehaviour : MonoBehaviour
{
	public float speed;
	public Vector3 target;
	private float startTime;
	private Vector3 startPos;
	private float journeyLength;

	void Start()
	{
		startTime = Time.time;
		startPos = transform.position;
		target=new Vector3(0,0,0);
	}

	// Update is called once per frame
	void Update ()
	{
		float distCovered = (Time.time - startTime);

		if (target !=new Vector3(0,0,0)) {
			journeyLength = Vector3.Distance (startPos, target);
			float fracJourney = distCovered / journeyLength;
			//float step = speed * Time.deltaTime;

			//if (target != null) {
			//	transform.position = Vector3.MoveTowards (transform.position, target.position, step);
			//}
			transform.position = Vector3.Lerp (transform.position, target, fracJourney);
			//Debug.Log("Move to"+ target);  
			startTime=Time.time;
		}
	}

	public void setSpeed(float speed)
	{
		this.speed = speed;
	}
	public void setTarget(Vector3 target)
	{
		this.target = target;
	}
}

