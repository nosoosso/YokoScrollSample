using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public PlayerMovement movement;

	void Update()
	{
		movement.PlayerUpdate();
	}

}
