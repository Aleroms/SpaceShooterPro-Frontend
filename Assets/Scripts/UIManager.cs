using SpaceShooterPro.API;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoretext;

    // highscore stuff
    [SerializeField]
    private Text _highscoretext;
    [SerializeField]
    private GameObject _highscorePanel;
    [SerializeField]
    private GameObject _highScoreContainers;
    [SerializeField]
    private GameObject _playerEntryPrefab;

    [SerializeField]
    private Text _gameover_text;
    [SerializeField]
    private Text _gameover_instructions_text;
    [SerializeField]
    private Image _livesDisplay;
    [SerializeField]
    private Sprite[] _lives;
    private GameManager _gm;
    private BackendAPI _backendAPI;

    private int _highscore = 0;
    private int _currentscore = 0;


    private void Awake()
    {

        _backendAPI = GameObject.Find("NetworkManager").GetComponent<BackendAPI>();
        var playerUsername = PlayerPrefs.GetString("username");

        if (playerUsername != null && _backendAPI != null)
        {
            _backendAPI.GetPlayerHighScore(playerUsername, (int highscore) =>
            {
                _highscoretext.text = highscore != 0 ? "Highscore: " + highscore : "";
                _highscore = highscore;
            });
        }
        else
            Debug.LogError("username is null: check PlayerPrefs");
    }
    void Start()
    {
        _scoretext.text = "Score: " + 0;
        _highscorePanel.SetActive(false);
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
        _currentscore = score;
    }
    public void OnPlayerDeath()
    {
        /*
		 * update player highscore
		 * if > previous -> display 'NEW HIGH SCORE'
		 * display top 10 high scores
		 */
        if (_currentscore > _highscore)
        {
            // update highscore
            // turn on NEW HIGH SCORE banner.
            _gameover_text.text = "NEW HIGH SCORE";
            _backendAPI.UpdatePlayerHighScore(_currentscore,
                PlayerPrefs.GetString("sessionToken"));
        }

        // display 5 player highscores
        DisplayPlayerHighScores();


        _gameover_instructions_text.gameObject.SetActive(true);
        _gm.GameOver();



        StartCoroutine(GameOverFlicker());
    }
    private void DisplayPlayerHighScores()
    {
        _highscorePanel.SetActive(true);
        _backendAPI.GetPlayersHighScore(10, (List<HighScoreResponse> list) =>
        {

            int idx = 1;
            foreach (var item in list)
            {
                // instantiate new prefab
                GameObject playerEntry = Instantiate(_playerEntryPrefab);
                playerEntry.transform.SetParent(_highScoreContainers.transform);
                playerEntry.gameObject.name = "player" + idx;

                // set username and high score by getting the child GO
                playerEntry.transform.Find("username").GetComponent<Text>()
                .text = item.username;

                playerEntry.transform.Find("highscore").GetComponent<Text>()
                .text = item.highscore.ToString();
                idx++;
            }

            // resize Content depending on how many items returned
            RectTransform Content = GameObject.FindGameObjectWithTag("HighscoreContent")
                .GetComponent<RectTransform>();

            var prefabSize = _playerEntryPrefab.GetComponent<RectTransform>().sizeDelta;
            var padding = 50f;
            Content.sizeDelta = new Vector2(0, prefabSize.y * idx + padding);
        });

    }
    private IEnumerator GameOverFlicker()
    {
        while (true)
        {
            _gameover_text.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameover_text.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);

        }
    }
}
