using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public void LoadLevel()
	{
		SceneManager.LoadScene("Game");
	}
	public void LoadMainMenu()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene("MainMenu");
	}
	public void Quit()
	{
		Application.Quit();
	}
  //load the main menu
}
