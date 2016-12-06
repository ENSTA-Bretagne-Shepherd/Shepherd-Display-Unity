using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SimulationManager : MonoBehaviour
{
	public static SimulationManager manager;
	public GameObject[] auvs;
	public GameObject[] buoys;
	//public int totalAuv;
	//public int totalBuoy;

	void Awake()
	{
		manager = this;
	}

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	public static GameObject[] getAuvs()
	{
		return manager.auvs.ToArray();
	}

	public static GameObject getBuoys(int index)
	{
		return manager.buoys[index];
	}
}

