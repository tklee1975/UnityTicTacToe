using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteTestController : MonoBehaviour {
	public Sprite[] testSpriteList;

	private int index = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// filename:  The path is relative to any Resources folder inside the Assets folder of your project, extensions must be omitted.
	public Sprite getSprite(string filename)
	{
		Sprite sprite = Resources.Load(filename, typeof(Sprite)) as Sprite;
		Debug.Log("sprite=" + sprite + " filename=" + filename);

		return sprite;
	}

	public void testLoadSprite() {
		Debug.Log("Try to change the sprite");

		GameObject spriteObj = GameObject.Find("TestSprite");

		SpriteRenderer sprite = spriteObj.GetComponent<SpriteRenderer>();
		//sprite.sprite = (Sprite) Resources.Load("Sprite/icon-o.png", typeof(Sprite));

		sprite.sprite = getSprite("Sprite/icon-o");
	}

	// using setter in Inspector
	public void testChangeSprite() {
		Sprite sprite = testSpriteList[index];

		// Setting
		GameObject spriteObj = GameObject.Find("TestSprite");
		SpriteRenderer spriteRenderer = spriteObj.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = sprite;

		// Next Sprite 
		index++;
		if(index >= testSpriteList.Length) {
			index = 0;
		}
	}
}
