using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class SimulationManager : MonoBehaviour
{
	// Environement
	public SortedDictionary<string, GameObject> sail = new SortedDictionary<string, GameObject>();
	public SortedDictionary<string, GameObject> buoy = new SortedDictionary<string, GameObject>();
	public UnityEngine.Object prefab_sailboat;
	public UnityEngine.Object prefab_buoy;

	// Server
	private Queue<String> TaskQueue;
	public object _queueLock;

	void Awake()
	{
	}

	// Use this for initialization
	void Start ()
	{
		TaskQueue = TCPserver.tcpserver.getTaskQueue();
		_queueLock = TCPserver.tcpserver.getQueueLock();
	}
	
	// Update is called once per frame
	void Update ()
	{
		lock (_queueLock)
		{
			while (TaskQueue.Count > 0) {
				string data=TaskQueue.Dequeue();
				updateEnv(data);
			}
		}
	}

	void updateEnv(string data)
	{
		//Debug.Log("Updating: " + data);
		JsonSerial json = JsonSerial.CreateFromJSON(data);

		if(json.Sailboat.name != null){
			if(!sail.ContainsKey(json.Sailboat.name)){
				sail[json.Sailboat.name] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sail[json.Sailboat.name].name = json.Sailboat.name;
				//sail[json.Sailboat.name] = Instantiate(Resources.Load("Sailboat")) as GameObject;
			}
			sail[json.Sailboat.name].transform.position = new Vector3((float)json.Sailboat.x, (float)0.0, (float)json.Sailboat.y);
			sail[json.Sailboat.name].transform.eulerAngles = new Vector3 ((float)json.Sailboat.roll, -(float)json.Sailboat.yaw, (float)json.Sailboat.pitch);
		}
		if(json.Buoy.name != null){
			if(!buoy.ContainsKey(json.Buoy.name)){
				buoy[json.Buoy.name] = GameObject.CreatePrimitive(PrimitiveType.Cube);
				buoy[json.Buoy.name].name = json.Buoy.name;
				//buoy[json.Buoy.name] = Instantiate(Resources.Load("Buoy")) as GameObject;
			}
			buoy[json.Buoy.name].transform.position = new Vector3((float)json.Buoy.x, (float)json.Buoy.z, (float)json.Buoy.y);
		}
		
	}
}

