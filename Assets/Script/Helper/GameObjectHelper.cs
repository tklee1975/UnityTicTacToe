using System;
using UnityEngine;
using UnityEngine.UI;

public class GameObjectHelper
{
	public static void ChangeObjectLocalY(GameObject obj, float position)
	{
		Vector3 newPos = obj.transform.localPosition;
		newPos.y = position;

		obj.transform.localPosition = newPos;
	}

	public static void ChangeObjectY(GameObject obj, float position)
	{
		Vector3 newPos = obj.transform.position;
		newPos.y = position;

		obj.transform.position = newPos;
	}

	public static GameObject GetChildObject(GameObject parent, string childName)
	{
		return parent.transform.FindChild(childName).gameObject;
	}
}

