using UnityEngine;
using System.Collections;

public class Player : CollidableObject
{
	public Movement movement;

	void Update()
	{
		movement.PlayerUpdate();
	}

}
