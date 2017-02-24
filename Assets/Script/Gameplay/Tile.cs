using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
	public enum Type {
		TypeEmpty = 0,
		TypeCircle = 1,		// Player 1
		TypeCross = 2,		// Player 2
	};
		

	public Bounds bounds;
	public Type type;

	public Sprite spriteCross;
	public Sprite spriteCircle;
	public int gridX;
	public int gridY;

	private SpriteRenderer chessSpriteRender;

	// Use this for initialization
	void Start () {
		BoxCollider2D box = GetComponent<BoxCollider2D>();
		bounds = box.bounds;
		type = Type.TypeEmpty;

		//chessSpriteRender = Get GetComponentInChildren<SpriteRenderer>();
	}

	void Awake() {
		chessSpriteRender = transform.FindChild("Chess").gameObject
			.GetComponentInChildren<SpriteRenderer>();
		SetupGridData();
	}

	private int GetGridPosition(float localPos)
	{
		if(localPos == 0){
			return 1;
		}

		if(localPos > 0) {
			return 2;
		}

		return 0;
	}

	void SetupGridData()
	{
		Debug.Log("transform=" + transform.position + " / " + transform.localPosition);			// position in the world space
		//transform.l
		gridX = GetGridPosition(transform.localPosition.x);
		gridY = GetGridPosition(transform.localPosition.y);
	}


	// Update is called once per frame
	void Update () {
		
	}

	public void Reset()
	{
		// Debug.Log("ChangeType: Reset");
		type = Type.TypeEmpty;
		UpdateTileImage();
	}

	public bool ChangeType(Type newType)
	{
		if(type != Type.TypeEmpty) {
			return false;
		}

		type = newType;

		// Debug.Log("ChangeType: newType=" + newType);

		UpdateTileImage();

		return true;
	}

	public void UpdateTileImage()
	{
		SpriteRenderer spriteRender = chessSpriteRender;

		spriteRender.enabled = true;
		switch(type) {
			case Type.TypeCircle: {
				spriteRender.sprite = spriteCircle;
				break;
			}
			case Type.TypeCross: {
				spriteRender.sprite = spriteCross;
				break;
			}
			default: {
				spriteRender.enabled = false;
				break;
			}
		}
	}


	public bool ContainsPoint(Vector3 point)
	{
		Vector3 myPoint = point;
		myPoint.z = bounds.center.z;

		return bounds.Contains(myPoint);
	}

	public bool HavePlaced()	// placed a cross or circle
	{ 
		return type != Type.TypeEmpty;
	}

	public string InfoBox() {
		string info = "bound: " + bounds;
		return info;
	}
}
