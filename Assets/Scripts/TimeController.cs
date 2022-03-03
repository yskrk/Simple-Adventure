using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
	[SerializeField] private float second;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject gameover;
	[SerializeField] private AudioClip soundGameOver;
	[SerializeField] private AudioSource sourceBGM;

	private float oldSec;
	private bool isActive = false;
	private Text textTimer;
	private Text textGameOver;

	private AudioSource musicGameOver;

    // Start is called before the first frame update
    void Start()
    {
		gameover.SetActive(false);
		oldSec = second;
		textTimer = GetComponent<Text>();
		musicGameOver = GetComponent<AudioSource>();
		textTimer.text = ((int)second).ToString("00");
    }

    // Update is called once per frame
    void Update()
    {
		second -= Time.deltaTime;
		if ((int)second != (int)oldSec) {
			textTimer.text = ((int)second).ToString("00");
		}
		oldSec = second;

		if (second <= 0.0f) {
			if (!isActive) {
				sourceBGM.Stop();
				musicGameOver.loop = true;
				musicGameOver.clip = soundGameOver;
				musicGameOver.Play();
				isActive = true;
			}

			// show result canvas for game over
			Time.timeScale = 0.0f;
			player.SetActive(false);
			gameover.SetActive(true);
		}
    }
}
