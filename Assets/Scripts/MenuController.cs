using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
	public void StartGame() {
		SceneManager.LoadScene("Level");
		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void StartPractice() {
		SceneManager.LoadScene("Test");
		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);s
	}

	public void BackMenu() {
		SceneManager.LoadScene("Main Menu");
		// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void QuitGame() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else 
		Application.Quit();
#endif
	}
}
