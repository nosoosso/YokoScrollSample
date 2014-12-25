using UnityEngine;
using System.Collections;

/// <summary>
/// キャラクターを移動するための機構
/// </summary>
public class PlayerMovement : MonoBehaviour
{
	/// <summary>
	/// 動く速さ
	/// </summary>
	public float MOVE_SPEED = 5;

	/// <summary>
	/// ジャンプ力
	/// </summary>
	public float JUMP_FORCE = 3;

	/// <summary>
	/// 動きの最高速度
	/// </summary>
	public float maxVx = 3;

	/// <summary>
	/// 何も操作しなかったときの制動力
	/// </summary>
	public float brakingForce = 0.5f;

	private CollisionManager collisionManager;
	private CollidableObject collidable;

	/// <summary>
	/// 速度
	/// </summary>
	private float vx;
	private float vy;

	/// <summary>
	/// 地面についているか
	/// </summary>
	private bool grounded = false;

	/// <summary>
	/// 右を向いているか
	/// </summary>
	private bool facingRight = true;

	void Start()
	{
		collisionManager = GameObject.Find("CollisionManager(Clone)").GetComponent<CollisionManager>();
		collidable = GetComponent<CollidableObject>();
	}

	//TODO なんかのデザインパターン(Builder?)つかって特殊化する
	public void PlayerUpdate()
	{
		CatchInput();
		Move();
	}

	private void CatchInput()
	{
		if (Input.GetKey(KeyCode.RightArrow))
		{
			vx += MOVE_SPEED * Time.deltaTime;
			facingRight = true;
		}
		else if (Input.GetKey(KeyCode.LeftArrow))
		{
			vx -= MOVE_SPEED * Time.deltaTime;
			facingRight = false;
		}
		//左か右のキーが入力されていなかったとき
		else
		{
			Brake();
		}
		//最大速度に制限する
		if (vx > maxVx)
		{
			vx = maxVx;
		}
		else if (vx < -maxVx)
		{
			vx = -maxVx;
		}

		//ジャンプキーが押されたとき
		if (Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			vy = JUMP_FORCE;
			//ジャンプしたので地面を離れた
			grounded = false;
		}
	}

	//キャラクターにブレーキをかける
	private void Brake()
	{
		//右に動いているとき
		if (vx > 0)
		{
			if (vx < brakingForce * Time.deltaTime)
			{
				vx = 0;
			}
			else
			{
				vx -= brakingForce * Time.deltaTime;
			}
		}
		//左に動いているとき
		else
		{
			if (vx > -brakingForce * Time.deltaTime)
			{
				vx = 0;
			}
			else
			{
				vx += brakingForce * Time.deltaTime;
			}
		}
	}

	/// <summary>
	/// 現在のvx,vyなどの値によって実際に移動する
	/// </summary>
	private void Move()
	{
		#region 横に動く処理

		transform.Translate(vx * Time.deltaTime, 0, 0);

		//衝突した障害物を取得(ある場合)
		CollidableObject collisionX = collisionManager.Collided(collidable.GetRect(), ColliderType.Floor);
		//障害物との衝突がある場合、めり込んでいるのを直す
		if (collisionX != null)
		{
			//左から衝突した
			if (vx > 0)
			{
				transform.position = new Vector3(
					collisionX.transform.position.x - collisionX.UnitWidth / 2 - collidable.UnitWidth / 2,
					transform.position.y);
			}
			//右から衝突した
			else if (vx < 0)
			{
				transform.position = new Vector3(
					collisionX.transform.position.x + collisionX.UnitWidth / 2 + collidable.UnitWidth / 2 + 0.00001f/* この値がないと移動した後も衝突してしまう */,
					transform.position.y);
			}
			vx = 0;
		}

		#endregion

		#region 縦に動く処理

		transform.Translate(0, vy * Time.deltaTime, 0);
		//衝突した障害物を取得(ある場合)
		CollidableObject collisionY = collisionManager.Collided(collidable.GetRect(), ColliderType.Floor);
		//障害物との衝突がある場合
		//めり込んでいるのを直した後、速度を0にする
		if (collisionY != null)
		{
			//下から衝突した
			if (vy > 0)
			{
				transform.position = new Vector3(
					transform.position.x,
					collisionY.transform.position.y - collisionY.UnitHeight / 2 - collidable.UnitHeight / 2 - 0.00001f);
			}
			//上から衝突した
			else if (vy < 0)
			{
				transform.position = new Vector3(
					transform.position.x,
					collisionY.transform.position.y + collisionY.UnitHeight / 2 + collidable.UnitHeight / 2);
				//地面についた
				grounded = true;
			}
			vy = 0;

		}

		//自由落下している場合は地面についていない
		//この処理がないと、ある地面からジャンプせずに落下した場合に、空中でジャンプすることができてしまう
		if (vy < 0) grounded = false;

		//重力分vyを変化させる
		vy += Constants.GRAVITY * Time.deltaTime;

		#endregion
	}

}
