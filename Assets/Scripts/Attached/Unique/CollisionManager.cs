using UnityEngine;
using System.Collections.Generic;

public class CollisionManager : MonoBehaviour
{
	//CollidaerTypeをキーにしてCollidableObjectのリストを格納する連想配列
	private Dictionary<ColliderType, List<CollidableObject>> allCollidables;

	void Start()
	{
		allCollidables = new Dictionary<ColliderType, List<CollidableObject>>();
	}

	public void AddCollision(CollidableObject c, ColliderType type)
	{
		if (allCollidables.ContainsKey(type))
		{
			allCollidables[type].Add(c);
		}
		else
		{
			allCollidables[type] = new List<CollidableObject>();
			AddCollision(c, type);
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="collider"></param>
	/// <param name="type"></param>
	/// <returns></returns>
	public CollidableObject Collided(Rect collider, ColliderType type)
	{
		//TODO 指定したColliderTypeのキーをallCollidabledsが持っていない場合の処理
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
