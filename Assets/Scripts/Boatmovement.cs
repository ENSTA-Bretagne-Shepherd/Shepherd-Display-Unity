using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boatmovement : MonoBehaviour {


	public int speed=5, Rot=20;
	public Transform sail;
	private bool isRotating=false;
	public GameObject Mat;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.Translate (Vector3.forward * Input.GetAxis ("Vertical") * Time.deltaTime * speed);
		transform.Rotate (Vector3.up * Input.GetAxis ("Horizontal") * Time.deltaTime * Rot);

		if (Input.GetKeyDown (KeyCode.Space)) {
			isRotating = !isRotating;

		}
		if (isRotating) {
			Mat.transform.Rotate (Vector3.up * Time.deltaTime * Rot);
		}
	} 
}
