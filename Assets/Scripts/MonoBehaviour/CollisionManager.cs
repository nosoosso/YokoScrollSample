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

	/// <summary>
	/// コライダーをリストに追加する
	/// </summary>
	/// <param name="c">追加するコライダー</param>
	/// <param name="type">追加するコライダーのレイヤー</param>
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
	/// 与えられた当たり判定が特定のレイヤーの全てのオブジェクトと衝突しているか判定して、している場合そのコライダーを返す。
	/// </summary>
	/// <param name="collider">衝突を判定する当たり判定のRect。数値はUnit換算。</param>
	/// <param name="type">衝突を判定するレイヤー。ここで指定したレイヤーの全てのオブジェクトと当たり判定を比較して判定する。</param>
	/// <returns>衝突を検知したコライダー。複数のコライダーと衝突している場合も、1つのコライダーしか返さない。衝突していない場合はnullを返す</returns>
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
