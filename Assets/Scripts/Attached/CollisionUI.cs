using UnityEngine;
using System.Collections;

public class CollisionUI : MonoBehaviour {

	private GameObject traceObject;
	private RectTransform rectTransform;

	void Start()
	{
		rectTransform = GetComponent<RectTransform>();
	}

	void Update()
	{
		transform.position = traceObject.transform.position;
		Rect collisionRect = traceObject.GetComponent<CollidableObject>().GetRect();
		rectTransform.sizeDelta = new Vector2(collisionRect.width, collisionRect.height);
	}

	public void SetTraceObject(GameObject g)
	{
		traceObject = g;
	}
}
