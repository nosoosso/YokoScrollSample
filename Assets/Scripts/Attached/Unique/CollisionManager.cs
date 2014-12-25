using UnityEngine;
using System.Collections.Generic;

public class CollisionManager : MonoBehaviour
{
	private Dictionary<ColliderType, List<CollidableObject>> allCollidables;

	void Start()
	{
		allCollidables = new Dictionary<ColliderType, List<CollidableObject>>();

		allCollidables[ColliderType.Player] = new List<CollidableObject>();
		allCollidables[ColliderType.Enemy] = new List<CollidableObject>();
		allCollidables[ColliderType.Floor] = new List<CollidableObject>();
	}

	public void AddCollision(CollidableObject c, ColliderType type)
	{
		allCollidables[type].Add(c);
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="collider"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	public CollidableObject Collided(Rect collider, ColliderType type)
	{
		foreach (CollidableObject c in allCollidables[type])
		{
			//衝突を検知した
			if (c.GetRect().Overlaps(collider))
			{
				//TODO オブジェクトのクローンを返すようにする
				return c;
			}
		}
		return null;
	}
}
