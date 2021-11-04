using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaseSwitchBehaviour : MonoBehaviour
{
	[SerializeField] private int numSwitch;
	private bool isSendNum = false;
	private bool isDoneWork = false;

	public WallControllerLv2 wallController;

	private void OnTriggerEnter2D(Collider2D other) {
		if (!isDoneWork) {
			if (!isSendNum && other.gameObject.name == "Player") {
				wallController.setSwitchNumber(numSwitch);
				isSendNum = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if (!isDoneWork) {
			if (isSendNum && other.gameObject.name == "Player") {
				isSendNum = false;
				Debug.Log("reActive vase " + numSwitch);
			}
		}
	}

	public void setDoneWork(bool isDone) {
		isDoneWork = isDone;
	}

	public void setSendNum(bool isSend) {
		isSendNum = isSend;
	}
}
