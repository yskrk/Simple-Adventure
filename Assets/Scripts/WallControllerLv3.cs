using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControllerLv3 : MonoBehaviour
{
	[SerializeField] private GameObject cameraToActivate;
	[SerializeField] private GameObject cameraOut;
	[SerializeField] private GameObject wallLv3;
	[SerializeField] private float secondOpenWall = 30.0f;
	[SerializeField] private AudioClip doorOpen;
	[SerializeField] private AudioClip touchVase;
	[SerializeField] private AudioClip soundClock;

	private AudioSource sound;

	public VirtualCameraController vCamController;
	private float defaultSec;
	private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
		defaultSec = secondOpenWall;
		sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
		if (!isActive) {
			secondOpenWall -= Time.deltaTime;
		} else {
			secondOpenWall = defaultSec;
		}

		// enable wall
		if (secondOpenWall <= 0.0f) {
			sound.Stop();
			controllWall(true);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		// disable wall
		if (isActive && other.gameObject.name == "Player") {
			sound.PlayOneShot(touchVase);
			controllWall(false);
		}
	}

	private void controllWall(bool active) {
		isActive = active;

		// focus wall
		vCamController.TransitionTo(cameraToActivate);

		// remove wall
		Invoke("ActiveWall", 3.0f);

		// focus player
		Invoke("ZoomToPlayer", 5.0f);
	}

	private void ActiveWall() {
		sound.PlayOneShot(doorOpen);
		wallLv3.gameObject.SetActive(isActive);
		if (!isActive) {
			sound.loop = true;
			sound.clip = soundClock;
			sound.Play();
		}

	}

	private void ZoomToPlayer() {
		vCamController.TransitionTo(cameraOut);
	}
}
