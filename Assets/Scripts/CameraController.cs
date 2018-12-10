using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector2 velocity;

	public Vector3 minCameraPosition;
	public Vector3 maxCameraPosition;

	public bool bounds; 

	public float smoothTimeX = 0.05f;
	public float smoothTimeY = 0.05f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		// Note: transform.position refers to the transform of the game object
		// In this case, it is the camera.
		float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		transform.position = new Vector3(posX, posY, transform.position.z);

		if (bounds)//If camera is within bounds
		{
			transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCameraPosition.x, maxCameraPosition.x),
				Mathf.Clamp(transform.position.y, minCameraPosition.y, maxCameraPosition.y),
				Mathf.Clamp(transform.position.z, minCameraPosition.z, maxCameraPosition.z));
		}

	}
}
