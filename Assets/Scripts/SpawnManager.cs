using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
	[SerializeField]
	private float _seconds = 5f;
	[SerializeField]
	private GameObject _enemyPrefab;
	[SerializeField]
	private GameObject _enemyContainer;
	[SerializeField]
	private GameObject[] _powerups;
	private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {

	}
	public void StartSpawning()
	{
		StartCoroutine(SpawnEnemyRoutine());
		StartCoroutine(SpawnPowerupRoutine());
	}

	IEnumerator SpawnEnemyRoutine()
	{
		yield return new WaitForSeconds(3f);
		while(_stopSpawning == false)
		{
			Vector3 positionSpawn = new Vector3(Random.Range(-8f, 8f), 10,0);
			GameObject newEnemy = Instantiate(_enemyPrefab, transform.position + positionSpawn, Quaternion.identity);
			newEnemy.transform.parent = _enemyContainer.transform;
			yield return new WaitForSeconds(_seconds);
		}
	}
	IEnumerator SpawnPowerupRoutine()
	{
		yield return new WaitForSeconds(3f);
		while (_stopSpawning == false)
		{
			Vector3 pos = new Vector3(Random.Range(-8f, 8f), 10, 0);
			int rand = Random.Range(0, 3);
			GameObject powerup = Instantiate(_powerups[rand], transform.position + pos, Quaternion.identity);
			
		yield return new WaitForSeconds(Random.Range(3,7));
		}
		//every 3-7 s spawn powerup
	}
	public void OnPlayerDeath()
	{
		_stopSpawning = true;
	}
}
