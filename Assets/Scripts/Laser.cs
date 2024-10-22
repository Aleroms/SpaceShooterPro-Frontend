using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	[SerializeField]
	private float _speed = 8f;
	private bool _isEnemy = false;
   
    // Update is called once per frame
    void Update()
    {
		if (_isEnemy == false)
			MoveUp();
		else
			MoveDown();
    }
	void MoveDown()
	{

		transform.Translate(Vector3.down * _speed * Time.deltaTime);

		//Chalenge: Initial implementation
		if (transform.position.y <= -8f)
		{
			if (transform.parent != null)
				Destroy(transform.parent.gameObject);

			Destroy(gameObject);
		}
	}
	void MoveUp()
	{

		transform.Translate(Vector3.up * _speed * Time.deltaTime);

		//Chalenge: Initial implementation
		if (transform.position.y >= 9)
		{
			if (transform.parent != null)
				Destroy(transform.parent.gameObject);

			Destroy(gameObject);
		}
	}
	public void AssignEnemy()
	{
		_isEnemy = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player" && _isEnemy)
		{
			Player p = GameObject.Find("Player").GetComponent<Player>();

			if (p != null)
				p.Damage();
		}
	}
}
