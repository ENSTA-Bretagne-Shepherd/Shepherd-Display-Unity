using UnityEngine;
using System;

[Serializable]
public class AuvState{
	public string name;
	public float x;
	public float y;
	public float yaw;
	public float pitch;
	public float roll;
	public float speed;
	public float sailYaw;

	public static AuvState CreateFromJSON(string jsonString)
	{
		return JsonUtility.FromJson<AuvState>(jsonString);
	}
}