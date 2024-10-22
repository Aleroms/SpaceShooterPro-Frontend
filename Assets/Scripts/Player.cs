using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	[SerializeField]
	private float _speed = 3.5f;
	[SerializeField]
	private float _boundaryX = 10.6f;

	[SerializeField]
	private GameObject _laserPrefab;
	[SerializeField]
	private GameObject _tripleShotPrefab;
	[SerializeField]
	private GameObject _shields;
	[SerializeField]
	private GameObject _rightEngine;
	[SerializeField]
	private GameObject _leftEngine;

	[SerializeField]
	private float _fireRate = 0.5f;
	private float _canFire = -1f;
	[SerializeField]
	private float _speedboostMultiplier = 2;

	[SerializeField]
	private int _lives = 3;
	[SerializeField]
	private int _score;
	
	private bool _tripleShotEnable = false;
	private bool _speedBoostEnable = false;
	private bool _shieldsEnable = false;
	

	[SerializeField]
	private float _tripleShotCooldown = 5f;
	[SerializeField]
	private float _speedboostCooldown = 5f;

	private SpawnManager _manager;
	private UIManager _uimanager;
	[SerializeField]
	private AudioClip _laserClip;
	private AudioSource _audiosource;

	private GameManager _gameManager;
	//use _ to denote that variable is private
   
    void Start()
    {
		_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		if (_gameManager == null)
			Debug.LogError("Game Manger is Null");

			///
		//transform.position = new Vector3(0, 2, 0);
		
		_manager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		_uimanager = GameObject.Find("Canvas").GetComponent<UIManager>();

		if(_uimanager == null)
		{
			Debug.LogError("UI Manager is NULL");
		}

		if(_manager == null)
		{
			Debug.LogError("Spawn Manager is NULL");
		}
		//_shields.transform = gameObject.transform.GetChild(0);
		_rightEngine.SetActive(false);
		_leftEngine.SetActive(false);

		_audiosource = GetComponent<AudioSource>();

		if (_audiosource == null)
			Debug.LogError("Audio Source is null in Player.cs");
		else
			_audiosource.clip = _laserClip;
	}

  
    void Update()
    {
			CalculateMovement();

			if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
			{
				FireLaser();
			}
		
    }
	void FireLaser()
	{
			_canFire = Time.time + _fireRate;
		

		if (_tripleShotEnable)
			Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
		else
			Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.06f), Quaternion.identity);

		_audiosource.Play();
	}
	void CalculateMovement()
	{
		float horizontalInput = Input.GetAxis("Horizontal");
		float verticalInput = Input.GetAxis("Vertical");

		//move the player
		Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
		
		transform.Translate(direction * _speed * Time.deltaTime);

		//y-axis boundary
		transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -2.8f, 2), 0);


		//if out of range wrap the player
		if (transform.position.x >= _boundaryX)
		{
			transform.position = new Vector3(-1 * _boundaryX, transform.position.y, 0);
		}
		else if (transform.position.x < -1 * _boundaryX)
		{
			transform.position = new Vector3(_boundaryX, transform.position.y, 0);
		}
	}

	public void ShieldsActive()
	{
		_shields.SetActive(true);
		_shieldsEnable = true;
	}
	public void ScoreAdd(int num)
	{
		_score += num;
		_uimanager.SetScore(_score);

	}
	public void Damage()
	{
		//if shields is active do nothing
		if(_shieldsEnable)
		{
			_shieldsEnable = false;
			_shields.SetActive(false);
			return;
		}
		//deactivate shields and return

		_lives--;
		_uimanager.UpdateLives(_lives);

		EngineFailure();

		if(_lives < 1)
		{
			//let SpawnManager know to stop spawning
			//let UIManager know to display GAME OVER
			_manager.OnPlayerDeath();
			_uimanager.OnPlayerDeath();
			Destroy(this.gameObject);
		}
	}
	private void EngineFailure()
	{

		if (_lives == 2)
			_leftEngine.SetActive(true);
		else if (_lives == 1)
			_rightEngine.SetActive(true);

	}
	public void TripleShotActive()
	{
		_tripleShotEnable = true;
		StartCoroutine(TripleShotCoroutine());
	}
	IEnumerator TripleShotCoroutine()
	{
		yield return new WaitForSeconds(_tripleShotCooldown);
		_tripleShotEnable = false;
	}
	public void SpeedBostActive()
	{
		_speed *= _speedboostMultiplier;
		_speedBoostEnable = true;
		StartCoroutine(SpeedBoostCoroutine());
	}
	IEnumerator SpeedBoostCoroutine()
	{
		yield return new WaitForSeconds(_speedboostCooldown);
		_speedBoostEnable = false;
		_speed /= _speedboostMultiplier;
	}
}
