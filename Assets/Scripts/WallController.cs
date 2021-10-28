using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
	[SerializeField] private GameObject cameraToActivate;
	[SerializeField] private GameObject cameraOut;

	public VirtualCameraController vCamController;

    // Update is called once per frame
    void Update()
    {
        if (transform.parent.childCount == 1) {
			// focus wall
			vCamController.TransitionTo(cameraToActivate);

			// remove wall
			Invoke("DisableWall", 3.0f);

			// focus player
			Invoke("ZoomToPlayer", 5.0f);
		}
    }

	private void DisableWall() {
		this.gameObject.SetActive(false);
	}

	private void ZoomToPlayer() {
		vCamController.TransitionTo(cameraOut);
		// destroy itself here
		Destroy(this.gameObject);
	}
}