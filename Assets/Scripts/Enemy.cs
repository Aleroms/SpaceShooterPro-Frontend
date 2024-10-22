using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	[SerializeField]
	private float _speed = 4f;
	[SerializeField]
	private float _respawnBoundaryX = 10.6f;
	[SerializeField]
	private int _reward = 10;

	private Player _player;

	private Animator _enemyDeath;

	[SerializeField]
	private AudioClip _explosionClip;
	private AudioSource _audioSource;

	private float _canFire = -1f;
	private float _fireRate = 0.5f;
	[SerializeField]
	private GameObject _laserPrefab;
	//private Laser _laser;
   
	void Start()
	{
		_player = GameObject.Find("Player").GetComponent<Player>();

		if (_player == null)
			Debug.LogError("player script is null");

		_enemyDeath = GetComponent<Animator>();

		if (_enemyDeath == null)
			Debug.LogError("_enemyDeath is null");

		_audioSource = GetComponent<AudioSource>();

		if (_audioSource == null)
			Debug.LogError("Audio Source is null in Enemy.cs");
		else
			_audioSource.clip = _explosionClip;

		
	}
   
	void Fire()
	{
		_fireRate = Random.Range(3f, 7f);
		_canFire = Time.time + _fireRate;

		GameObject enemy = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
		Laser[] laser = enemy.GetComponentsInChildren<Laser>();

		for (int i = 0; i < laser.Length; i++)
			laser[i].AssignEnemy();
	}
    void Update()
    {
		CalculateMovement();
        
		if( Time.time > _canFire)
		{
			Fire();
		}

		
		
    }
	void CalculateMovement()
	{
		transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);
		//if outside of screen
		if (transform.position.y <= -5)
		{
			float x = Random.Range(-1 * _respawnBoundaryX, _respawnBoundaryX);
			transform.position = new Vector3(x, 8, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		//it's better to use other.tag than other.gameObject.tag
		if(other.tag == "Player")
		{
			Player player = other.transform.GetComponent<Player>();

			if(player != null)
			{
				player.Damage();
			}
			_enemyDeath.SetTrigger("OnEnemyDeath");
			_speed = 0;
			_audioSource.Play();
			Destroy(this.gameObject, 2.633f);
		}
		else if(other.tag == "Laser")
		{
			Destroy(other.gameObject);
			_player.ScoreAdd(_reward);
			_enemyDeath.SetTrigger("OnEnemyDeath");
			_speed = 0;
			_audioSource.Play();
			Destroy(GetComponent<Collider2D>());
			Destroy(this.gameObject, 2.633f);
		}
	}
}
