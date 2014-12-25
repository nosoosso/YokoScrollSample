using UnityEngine;
using System.Collections;

/// <summary>
/// 当たり判定の種類。UnityのLayerのようなもの
/// </summary>
public enum ColliderType
{
	Player,
	Enemy,
	Floor
}

/// <summary>
/// 当たり判定をもつゲームオブジェクト
/// </summary>
//本来はColliderというようなクラス名にしたいのだが、その名前はUnityのものと紛らわしいので仕方なくこれにした。
public class CollidableObject : MonoBehaviour
{
	/// <summary>
	/// 当たり判定の幅(px)
	/// </summary>
	public int width;
	/// <summary>
	/// 当たり判定の高さ(px)
	/// </summary>
	public int height;

	/// <summary>
	/// 当たり判定の表示に使用するUIのゲームオブジェクト
	/// </summary>
	public RectTransform collisionUI;

	public ColliderType type;

	public float UnitWidth
	{
		get { return width * Constants.PIXEL_SIZE; }
	}

	public float UnitHeight
	{
		get { return height * Constants.PIXEL_SIZE; }
	}

	void Start()
	{
		CollisionManager collisionManager = GameObject.Find("CollisionManager(Clone)").GetComponent<CollisionManager>();


		//CollisionManagerに登録する
		collisionManager.AddCollision(this, type);

		//当たり判定のUIのプレハブが設定されていない場合何もしない。当たり判定も表示しない。
		if (collisionUI == null) return;

		//当たり判定をUIで表示する
		Transform collisions = GameObject.Find("Collisions").transform;
		var item = GameObject.Instantiate(collisionUI) as RectTransform;
		item.SetParent(collisions);
		item.GetComponent<CollisionUI>().SetTraceObject(gameObject);
	}

	/// <summary>
	/// 当たり判定の四角形を返す
	/// </summary>
	/// <returns>Unit換算の四角形</returns>
	public Rect GetRect()
	{
		return new Rect(
			transform.position.x - UnitWidth / 2,
			transform.position.y -/* ←なぜ符号がマイナスなのか分からない */ UnitHeight / 2,
			UnitWidth,
			UnitHeight);
	}
}
