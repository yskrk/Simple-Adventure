using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
	[SerializeField] private GameObject cameraToActivate;
	[SerializeField] private GameObject cameraOut;
	[SerializeField] private GameObject wallLv1;
	[SerializeField] private AudioClip doorOpen;

	private AudioSource sound;
	private bool isActive = true;
	private bool isPlayed = false;

	public VirtualCameraController vCamController;

	void Start() {
		sound = GetComponent<AudioSource>();
	}

    // Update is called once per frame
    void Update()
    {
        if (this.transform.childCount == 1) {
			controllWall(false);
		}
    }

	private void controllWall(bool active) {
			isActive = active;

			// focus wall
			vCamController.TransitionTo(cameraToActivate);

			// remove wall
			Invoke("DisableWall", 3.0f);

			// focus player
			Invoke("ZoomToPlayer", 5.0f);
	}

	private void DisableWall() {
		if (!isPlayed) {
			sound.PlayOneShot(doorOpen);
			isPlayed = true;
		}
		wallLv1.gameObject.SetActive(isActive);
	}

	private void ZoomToPlayer() {
		vCamController.TransitionTo(cameraOut);
		// destroy itself here
		Destroy(this.gameObject);
	}
}