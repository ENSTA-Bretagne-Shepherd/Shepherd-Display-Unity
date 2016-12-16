using UnityEngine;
using System.Collections;
using System;

public class camera : MonoBehaviour {

    private float cameraDistance = 50f;
    private Vector3 focus;
    private Vector3 direction;
    private float xposition = 0f;
    private float yposition = 0f;
    private float differenceX = 0f;
    private float differenceY = 0f;
    private bool objectfocus = false;
    private GameObject m_objfocus;
    private float vAngle = (float)Math.PI/4; 
    private float hAngle = (float)Math.PI/4;

    /// <summary>
    /// Initialize camera starting position.
    /// </summary>
	void Start () {
        this.direction = new Vector3(0.577f, 0.577f, 0.577f);
        this.focus = new Vector3(0,0,0);
        transform.position = cameraDistance * this.direction + focus;
        transform.LookAt(focus);
	}
	
    /// <summary>
    /// Read mouse entry and update camera position.
    /// </summary>
	void Update () {

        if(objectfocus){
            focus = m_objfocus.transform.position;
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0) // forward
        {
            cameraDistance += 10;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0) // back
        {
            cameraDistance -= 10;
        }

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
            hAngle = -factorX * (float)Math.PI + (float)Math.PI/4;

            // CAMERA VERTICAL ROTATION
            float halfHeight = Screen.height * 0.5f;
            float mouseYPos = Input.mousePosition.y;
            differenceY += mouseYPos - yposition;
            float factorY = differenceY / halfHeight;
            vAngle = -factorY * (float)Math.PI + (float)Math.PI / 4;

            
            transform.position = new Vector3(
                focus.x + cameraDistance * (float)(Math.Cos(hAngle) * Math.Cos(vAngle)),
                focus.y + cameraDistance * (float)(Math.Sin(vAngle)),
                focus.z + cameraDistance * (float)(Math.Sin(hAngle) * Math.Cos(vAngle)));

            this.xposition = mouseXPos;
            this.yposition = mouseYPos;
        }
        if(Input.GetMouseButton(0)){
            // Select Object
            Debug.Log("test");
            RaycastHit hitInfo = new RaycastHit();
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo)){
                Debug.Log(hitInfo.transform.gameObject.name);
            }
            else{
                Debug.Log("Missed");
            }
        }
        if (Input.GetKey("left"))
        {
            objectfocus = false;
            focus.x += (float)(0.1 * Math.Sin(hAngle));
            focus.z -= (float)(0.1 * Math.Cos(hAngle));
            transform.position = new Vector3(
                focus.x + cameraDistance * (float)(Math.Cos(hAngle) * Math.Cos(vAngle)),
                focus.y + cameraDistance * (float)(Math.Sin(vAngle)),
                focus.z + cameraDistance * (float)(Math.Sin(hAngle) * Math.Cos(vAngle)));
        }
        if (Input.GetKey("right"))
        {
            objectfocus = false;
            focus.x -= (float)(0.1 * Math.Sin(hAngle));
            focus.z += (float)(0.1 * Math.Cos(hAngle));
            transform.position = new Vector3(
                focus.x + cameraDistance * (float)(Math.Cos(hAngle) * Math.Cos(vAngle)),
                focus.y + cameraDistance * (float)(Math.Sin(vAngle)),
                focus.z + cameraDistance * (float)(Math.Sin(hAngle) * Math.Cos(vAngle)));
        }
        if (Input.GetKey("up"))
        {
            objectfocus = false;
            focus.x -= (float)(0.1 * Math.Cos(hAngle));
            focus.z -= (float)(0.1 * Math.Sin(hAngle));
            transform.position = new Vector3(
                focus.x + cameraDistance * (float)(Math.Cos(hAngle) * Math.Cos(vAngle)),
                focus.y + cameraDistance * (float)(Math.Sin(vAngle)),
                focus.z + cameraDistance * (float)(Math.Sin(hAngle) * Math.Cos(vAngle)));
        }
        if (Input.GetKey("down"))
        {
            objectfocus = false;
            focus.x += (float)(0.1 * Math.Cos(hAngle));
            focus.z += (float)(0.1 * Math.Sin(hAngle));
            transform.position = new Vector3(
                focus.x + cameraDistance * (float)(Math.Cos(hAngle) * Math.Cos(vAngle)),
                focus.y + cameraDistance * (float)(Math.Sin(vAngle)),
                focus.z + cameraDistance * (float)(Math.Sin(hAngle) * Math.Cos(vAngle)));
            
        }
        if(Input.GetKey(""))
        transform.position = new Vector3(
                focus.x + cameraDistance * (float)(Math.Cos(hAngle) * Math.Cos(vAngle)),
                focus.y + cameraDistance * (float)(Math.Sin(vAngle)),
                focus.z + cameraDistance * (float)(Math.Sin(hAngle) * Math.Cos(vAngle)));
        transform.LookAt(focus, new Vector3(0, 1, 0));

    }

    public void setFocus(GameObject obj_focus){
        objectfocus = true;
        m_objfocus = obj_focus;
    }
}
