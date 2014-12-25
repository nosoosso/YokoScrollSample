using UnityEngine;
using System.Collections;

public static class Constants
{
	//キャラクターの落下する速度に影響する。
	public static readonly float GRAVITY;

	//1ユニット当たりのピクセル数
	public static readonly int PIXELS_PER_UNIT;

	//1ピクセル当たり何ユニットか
	public static readonly float PIXEL_SIZE;

	static Constants()
	{
		GRAVITY = -10;
		PIXELS_PER_UNIT = 100;
		PIXEL_SIZE = 1f / PIXELS_PER_UNIT;
	}
}
