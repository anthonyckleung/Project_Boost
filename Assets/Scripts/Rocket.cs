using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

	Rigidbody rigidBody;
	AudioSource audioSource;

	[SerializeField] float mainThrust = 100f;
	[SerializeField] float rcsThrust = 100f;
	[SerializeField] float levelLoadDelay = 2f;

	[SerializeField] AudioClip mainEngineSound;
	[SerializeField] AudioClip rocketDeathSound;
	[SerializeField] AudioClip rocketSucceedSound;

	[SerializeField] ParticleSystem mainEngineParticles;
	[SerializeField] ParticleSystem deathParticles;
	[SerializeField] ParticleSystem successParticles;


//	enum State {Alive, Dying, Transcending} 
//	State state = State.Alive;
	bool isTransitioning = false;

	// Use this for initialization
	void Start () 
	{
		rigidBody = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (!isTransitioning)
		{
			RespondToThrustInput ();
			RespondToRotateInput ();
		}
		RespondToDebugKeys();
	}

	private void RespondToDebugKeys()
	{
		if (Input.GetKeyDown(KeyCode.L))
		{
			LoadNextLevel();
		}
	}
		
	void OnCollisionEnter(Collision collision)
	{
		if (isTransitioning)
		{
			return;
		}

		switch(collision.gameObject.tag)
		{
		case "Friendly":
			break;
		case "Finish":
			isTransitioning = true;
			audioSource.Stop();
			audioSource.PlayOneShot (rocketSucceedSound);
			successParticles.Play();
			Invoke("LoadNextLevel", levelLoadDelay);
			break;
		default:
			isTransitioning = true;
			audioSource.Stop();
			audioSource.PlayOneShot (rocketDeathSound);
			deathParticles.Play();
			Invoke("LoadFirstLevel", levelLoadDelay);
			break;
		
		}
	}

	private void LoadNextLevel ()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = currentSceneIndex + 1;
		if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
		{
			nextSceneIndex = 0; //Loop back to start
		}
		SceneManager.LoadScene (nextSceneIndex);
	}

	private void LoadFirstLevel ()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene (currentSceneIndex);
	}

	private void RespondToThrustInput()
	{
		if (Input.GetKey (KeyCode.Space)) {
			ApplyThrust ();
		}
		else {
			StopApplyingThrust ();
		}
	}

	private void ApplyThrust ()
	{
		rigidBody.AddRelativeForce (Vector3.up * mainThrust);
		if (!audioSource.isPlaying) {
			audioSource.PlayOneShot (mainEngineSound);
		}
		mainEngineParticles.Play();
	}

	private void StopApplyingThrust ()
	{
		audioSource.Stop ();
		mainEngineParticles.Stop ();
	}

	private void RespondToRotateInput()
	{
		float rotationThisFrame = rcsThrust * Time.deltaTime; //stop rotation of rocket

		rigidBody.angularVelocity = Vector3.zero; //remove rotation due to physics

		if (Input.GetKey(KeyCode.A)) {
			transform.Rotate (Vector3.forward * rotationThisFrame);
		}
		else if (Input.GetKey(KeyCode.D)) {
			transform.Rotate (-Vector3.forward * rotationThisFrame);
		}

	}
}
