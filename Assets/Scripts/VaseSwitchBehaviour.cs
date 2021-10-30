using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseSwitchBehaviour : MonoBehaviour
{
	[SerializeField] private int numSwitch;
	[SerializeField] private AudioClip touchVase;
	private bool isSendNum = false;
	private bool isDoneWork = false;

	private AudioSource sound;
	public WallControllerLv2 wallController;

	void Start() {
		sound = GetComponent<AudioSource>();
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!isDoneWork) {
			if (!isSendNum && other.gameObject.name == "Player") {
				sound.PlayOneShot(touchVase);
				wallController.setSwitchNumber(numSwitch);
				isSendNum = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (!isDoneWork) {
			if (isSendNum && other.gameObject.name == "Player") {
				isSendNum = false;
				// Debug.Log("reActive vase " + numSwitch);
			}
		}
	}

	// public void setDoneWork(bool isDone) {
	// 	isDoneWork = isDone;
	// }

	// public void setSendNum(bool isSend) {
	// 	isSendNum = isSend;
	// }
}
