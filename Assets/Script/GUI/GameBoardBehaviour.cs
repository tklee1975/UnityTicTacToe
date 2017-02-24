using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

	void handleMouseDown(Vector3 pos)
	{
		Debug.Log("Mouse position: " + pos);





		// World position
		Vector3 worldPosV3 = Camera.main.ScreenToWorldPoint(pos);


//		var hit: RaycastHit2D = Physics2D.Raycast(Vector2(
		// camera.ScreenToWorldPoint(Input.mousePosition).x,
		// camera.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0);
//
//
		Vector2 worldPos = new Vector2(worldPosV3.x, worldPosV3.y);

		Collider2D[] colliders = this.GetComponents<Collider2D>();
		Bounds bound = colliders[0].bounds;

		Vector3 hitPos = new Vector3(worldPos.x, worldPos.y, this.transform.position.z);
		bool isHit = bound.Contains(hitPos);

		Debug.Log("World position: " + worldPos + " bounds=" + colliders[0].bounds
			+ " isHit=" + isHit);

//		RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, 0, 0, 100f);
//
//		if(hit) {
//			Debug.Log("Hit something!!");	
//		}
//		// 
//

		// RayCast will hit Mesh Collider
//		worldPos.z = -5;
//
//		Vector3 fwd = transform.TransformDirection(Vector3.forward);
//		Debug.DrawRay(worldPos, fwd * 10, Color.green);		// Debug ray
//
//		Ray ray = new Ray();
//		ray.origin = worldPos;
//		ray.direction = fwd;
//
//		RaycastHit hit;
//		if(Physics.Raycast(ray, out hit, 10)) {
//			print("There is something in front of the object!");
//		}
//
//		// Send a Ray 
//		Vector3 fwd = transform.TransformDirection(Vector3.forward);
//		Debug.DrawRay(worldPos, fwd * 10, Color.green);
//
//
//		if (Physics.Raycast(worldPos, fwd, 10)) {	// 10 is the distance
//			print("There is something in front of the object!");
//		}
//		



		//		RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
//		if (hit.collider != null) {
//			Debug.Log("Hit something!!");
//		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Debug.Log("Pressed left click.");
			handleMouseDown(Input.mousePosition);				
		}

//		for (int i = 0; i < Input.touchCount; ++i) {
//			if (Input.GetTouch(i).phase == TouchPhase.Began) {
//				Debug.Log("Touch Began");
//			}
//		}
	}
}
