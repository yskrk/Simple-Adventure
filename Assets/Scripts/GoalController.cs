using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject timer;
	[SerializeField] private GameObject clear;

    // Start is called before the first frame update
    void Start()
    {
        clear.SetActive(false);
    }

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.name == player.name) {
			Time.timeScale = 0.0f;
			clear.GetComponent<GameObject>();
			clear.SetActive(true);
			player.SetActive(false);
		}
	}
}
