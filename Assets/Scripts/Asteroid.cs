using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
	[SerializeField]
	private float _speed = 3f;
	[SerializeField]
	private GameObject _explosion;
	private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
		_spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

		if(_spawnManager == null)
		{
			Debug.LogError("Spawn Manager is Null");
		}
    }

    // Update is called once per frame
    void Update()
    {
		transform.Rotate(new Vector3(0, 0, 1), -1 * 30f * _speed * Time.deltaTime);
    }
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Laser")
		{
			Instantiate(_explosion, transform.position, Quaternion.identity);
			Destroy(other.gameObject);
			_spawnManager.StartSpawning();
			Destroy(gameObject, 0.25f);
		}
	}
}
