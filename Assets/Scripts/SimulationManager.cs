using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class SimulationManager : MonoBehaviour
{
	// Environement
	public SortedDictionary<string, GameObject> auvs = new SortedDictionary<string, GameObject>();
	public SortedDictionary<string, GameObject> buoy = new SortedDictionary<string, GameObject>();
	//public int totalAuv;
	//public int totalBuoy;

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
		Debug.Log ("enter update");
		AuvState auv = AuvState.CreateFromJSON(data);
		Debug.Log("Sim Order : auv  " + auv.name + "  move to " + auv.x + "," + auv.y);

		if(!auvs.ContainsKey(auv.name)){
			//auvs[auv.name] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			auvs[auv.name] = Instantiate(Resources.Load("Auv")) as GameObject;
		}
		auvs[auv.name].transform.position = new Vector3((float)auv.x, (float)0.0, (float)auv.y);
		auvs [auv.name].transform.eulerAngles = new Vector3 ((float)auv.roll, -(float)auv.yaw, (float)auv.pitch);
	}
}

