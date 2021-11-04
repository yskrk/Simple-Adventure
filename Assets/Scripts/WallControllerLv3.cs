using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControllerLv3 : MonoBehaviour
{
	[SerializeField] private GameObject cameraToActivate;
	[SerializeField] private GameObject cameraOut;
	[SerializeField] private GameObject wallLv3;
	[SerializeField] private float secondOpenWall = 30.0f;

	public VirtualCameraController vCamController;
	private float defaultSec;
	private bool isActive = true;

    // Start is called before the first frame update
    void Start()
    {
		defaultSec = secondOpenWall;
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
			controllWall(true);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		// disable wall
		if (isActive && other.gameObject.name == "Player") {
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
		wallLv3.gameObject.SetActive(isActive);
	}

	private void ZoomToPlayer() {
		vCamController.TransitionTo(cameraOut);
	}
}
