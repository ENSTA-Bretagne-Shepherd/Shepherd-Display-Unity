using UnityEngine;
using System;

[Serializable]
public class JsonSerial{
	public SailboatSerial Sailboat;
	public BuoySerial Buoy;

	public static JsonSerial CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<JsonSerial>(jsonString);
	}
}