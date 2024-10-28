using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Text _scoretext;
	[SerializeField]
	private Text _gameover_text;
	[SerializeField]
	private Text _gameover_instructions_text;
	[SerializeField]
	private Image _livesDisplay;
	[SerializeField]
	private Sprite[] _lives;
	private GameManager _gm;
	

    // Start is called before the first frame update
    void Start()
    {
		_scoretext.text = "Score: " + 0;
		// modify code to turn off highscore leaderboard
		_gameover_text.gameObject.SetActive(false);
		_gameover_instructions_text.gameObject.SetActive(false);
		_gm = GameObject.Find("GameManager").GetComponent<GameManager>();

		if (_gm == null)
			Debug.LogError("Game manager is null");
    }

   public void UpdateLives(int currentLives)
	{
		_livesDisplay.sprite = _lives[currentLives];

	}
	public void SetScore(int score)
	{
		_scoretext.text = "Score: " + score;
	}
	public void OnPlayerDeath()
	{
		/*
		 * update player highscore
		 * if > previous -> display 'NEW HIGH SCORE'
		 * display top 10 high scores
		 */
		_gameover_instructions_text.gameObject.SetActive(true);
		_gm.GameOver();
		StartCoroutine(GameOverFlicker());
	}
	IEnumerator GameOverFlicker()
	{
		while(true)
		{
			_gameover_text.gameObject.SetActive(true);
			yield return new WaitForSeconds(0.5f);
			_gameover_text.gameObject.SetActive(false);
			yield return new WaitForSeconds(0.5f);

		}
	}
}
