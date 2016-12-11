using UnityEngine;
using System.Collections;
using System;

public class camera : MonoBehaviour {

    private float cameraDistance = 50f;
    private Vector3 direction;
    private float xposition = 0f;
    private float yposition = 0f;
    private float differenceX = 0f;
    private float differenceY = 0f;

    /// <summary>
    /// Initialize camera starting position.
    /// </summary>
	void Start () {
        this.direction = new Vector3(0.577f, 0.577f, 0.577f);
        transform.position = cameraDistance * this.direction;
        transform.LookAt(new Vector3(0, 0, 0));
	}
	
    /// <summary>
    /// Read mouse entry and update camera position.
    /// </summary>
	void Update () {

        if (Input.GetAxis("Mouse ScrollWheel") < 0) // forward
        {
            cameraDistance += 10;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // back
        {
            cameraDistance -= 10;
        }
        transform.position = cameraDistance * this.direction;

        if (Input.GetMouseButtonDown(1))
        {
            this.xposition = Input.mousePosition.x;
            this.yposition = Input.mousePosition.y;
        }
        if (Input.GetMouseButton(1))
        {
            // CAMERA HORIZONTAL ROTATION
            float halfWidth = Screen.width * 0.5f;
            float mouseXPos = Input.mousePosition.x;
            differenceX += mouseXPos - xposition;
            float factorX = differenceX / halfWidth;
            float hAngle = -factorX * (float)Math.PI + (float)Math.PI/4;

            // CAMERA VERTICAL ROTATION
            float halfHeight = Screen.height * 0.5f;
            float mouseYPos = Input.mousePosition.y;
            differenceY += mouseYPos - yposition;
            float factorY = differenceY / halfHeight;
            float vAngle = -factorY * (float)Math.PI + (float)Math.PI / 4;

            
            transform.position = new Vector3(
                cameraDistance * (float)(Math.Cos(hAngle) * Math.Cos(vAngle)),
                cameraDistance * (float)(Math.Sin(vAngle)),
                cameraDistance * (float)(Math.Sin(hAngle) * Math.Cos(vAngle)));

            // Update direction vector
            this.direction = transform.position.normalized;

            if (Math.Abs(vAngle) > Math.PI / 2)
                transform.LookAt(new Vector3(0, 0, 0), new Vector3(0, -1, 0));
            else
                transform.LookAt(new Vector3(0, 0, 0), new Vector3(0, 1, 0));

            this.xposition = mouseXPos;
            this.yposition = mouseYPos;
        }
        
        
    }
}
