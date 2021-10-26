using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
	[SerializeField] private float second;
	[SerializeField] private GameObject player;
	[SerializeField] private GameObject gameover;

	private float oldSec;
	private Text textTimer;
	private Text textGameOver;

    // Start is called before the first frame update
    void Start()
    {
		gameover.SetActive(false);
		oldSec = second;
		textTimer = GetComponent<Text>();
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
			// show result canvas for game over
			player.SetActive(false);
			gameover.SetActive(true);
			Time.timeScale = 0.0f;
		}
    }
}
