using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private bool _isGameOver = false;
	[SerializeField]
	private GameObject _pausePanel;
	private bool _isGamePaused = false;

	void Start()
	{ 
			_pausePanel.SetActive(false);
	}
	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.R) && _isGameOver)
		{
			//current game scene 
			SceneManager.LoadScene("Game");
		}

		if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Escape))
		{
			if(_isGamePaused == false)
			{
				Pause();
			}
			else
			{
				Resume();
			}
		}
			
	}
	public void GameOver()
	{
		_isGameOver = true;
	}
	public void Pause()
	{
		_isGamePaused = true;
		Time.timeScale = 0;
		_pausePanel.SetActive(true);
	}
	public void Resume()
	{
		_isGamePaused = false;
		Time.timeScale = 1;
		_pausePanel.SetActive(false);
	}
}
