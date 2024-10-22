using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
	[SerializeField]
	private float _speed = 3f;
	[SerializeField]
	private int _powerupID; //0 = TripleShot 1 = Speed 2 = Shield

	[SerializeField]
	private AudioClip _powerupClip;


    void Update()
    {
		transform.Translate(Vector3.down * Time.deltaTime * _speed);

		if (transform.position.y <= -5)
		{
			Destroy(gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			Destroy(gameObject);

			Player player = other.GetComponent<Player>();

			if (player != null)
			{
				switch(_powerupID)
				{
					case 0:
						player.TripleShotActive();
						break;
					case 1:
						player.SpeedBostActive();
						break;
					case 2:
						player.ShieldsActive();
						break;
				}
			}
			AudioSource.PlayClipAtPoint(_powerupClip, transform.position);
		}
	}
	
}
