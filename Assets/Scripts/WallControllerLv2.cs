using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallControllerLv2 : MonoBehaviour
{
	[SerializeField] private GameObject cameraToActivate;
	[SerializeField] private GameObject cameraOut;
	[SerializeField] private GameObject wallLv2;
	[SerializeField] private List<int> listAnswer;

	public VirtualCameraController vCamController;
	
	public List<int> listOrder;
	private int countAnswer;
	private bool isActive = true;
	private bool isCorrect = false;

    // Start is called before the first frame update
    void Start()
    {
        listOrder = new List<int>();
		countAnswer = listAnswer.Count;
    }

    // Update is called once per frame
    void Update()
    {
        if (listOrder.Count == countAnswer) {
			// Debug.Log("check order ...");
			// check key order
			for(int i = 0; i < countAnswer; i++) {
				if (listAnswer[i] == listOrder[i]) {
					if (i == (countAnswer - 1)) {
						isCorrect = true;
					}
				} else {
					// reset key order and vase condition
					listOrder.Clear();
					break;
				}
			}

			if (isCorrect) {
				// if correct, remove wall and vases
				controllWall(false);
			}
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
		wallLv2.gameObject.SetActive(isActive);
	}

	private void ZoomToPlayer() {
		vCamController.TransitionTo(cameraOut);
		// destroy itself here
		Destroy(this.gameObject);
	}

	public void setSwitchNumber(int keyNum) {
		// set number to this list from vaseswitch
		// Debug.Log("added number: " + keyNum);
		listOrder.Add(keyNum);
	}
}
