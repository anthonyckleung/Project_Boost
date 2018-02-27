using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {

	[SerializeField] Vector3 movementVector;
	[SerializeField] float period = 2f;

	float movementFactor; 
	private Vector3 startingPosition;

	// Use this for initialization
	void Start () {
		startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		// set movement factor
		// Protect against period is zero
		if (period <= Mathf.Epsilon){ return;}

		float cycles = Time.time / period; //grows continually from 0
		const float tau = Mathf.PI * 2;
		float rawSineWave = Mathf.Sin(cycles * tau);
		movementFactor = rawSineWave / 2f + 0.5f;


		Vector3 offset = movementVector * movementFactor ;

		transform.position = startingPosition + offset; //transform the position
	}
}
