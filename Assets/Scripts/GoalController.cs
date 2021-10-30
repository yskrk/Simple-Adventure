using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalController : MonoBehaviour
{
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject timer;
	[SerializeField] private GameObject clear;
	[SerializeField] private AudioClip soundClear;
	[SerializeField] private AudioSource sourceBGM;

	private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        clear.SetActive(false);
		sound = GetComponent<AudioSource>();
    }

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.name == player.name) {
			Time.timeScale = 0.0f;
			sourceBGM.Stop();
			sound.PlayOneShot(soundClear, 1.5f);
			clear.GetComponent<GameObject>();
			clear.SetActive(true);
			player.SetActive(false);
		}
	}
}
