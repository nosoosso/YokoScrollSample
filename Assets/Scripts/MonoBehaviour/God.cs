using UnityEngine;
using System.Collections;

public class God : MonoBehaviour
{

	public GameObject[] gameObjects;

	void Awake()
	{
		Instantiate(gameObjects[0]);
		Instantiate(gameObjects[1]);
		Instantiate(gameObjects[2], new Vector3(0, -2), Quaternion.identity);
		Instantiate(gameObjects[2], new Vector3(1.57f, 0), Quaternion.identity);
		Instantiate(gameObjects[2], new Vector3(-2, -1), Quaternion.identity);
		Instantiate(gameObjects[3], new Vector3(1.17f, 3.21f), Quaternion.identity);
	}
}
