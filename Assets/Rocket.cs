using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audioSource;
	[SerializeField] float mainThrust = 100f;
	[SerializeField] float rcsThrust = 100f;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		ProcessInput();

	}

	private void ProcessInput()
	{
		Thrust ();
		Rotate ();
	}

	void OnCollisionEnter(Collision collision)
	{
		switch(collision.gameObject.tag)
		{
		case "Friendly":
			break;
		
		}
	}



	private void Thrust()
	{

		if (Input.GetKey (KeyCode.Space)) {
			rigidBody.AddRelativeForce (Vector3.up * mainThrust);
			if (!audioSource.isPlaying) {
				audioSource.Play ();
			}
		}
		else {
			audioSource.Stop ();
		}
	}

	private void Rotate()
	{
		float rotationThisFrame = rcsThrust * Time.deltaTime;

		rigidBody.freezeRotation = true; //take physics control

		if (Input.GetKey(KeyCode.A)) {
			transform.Rotate (Vector3.forward * rotationThisFrame);
		}
		else if (Input.GetKey(KeyCode.D)) {
			transform.Rotate (-Vector3.forward * rotationThisFrame);
		}
		rigidBody.freezeRotation = false; // resume physics control 
	}
}
