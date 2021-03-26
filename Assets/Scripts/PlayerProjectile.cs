using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//im not sure if i really need a seperate class for enemy and player projectile but for now it is quicker and easier to copy paste than to think about it
public class PlayerProjectile : MonoBehaviour
{
	[SerializeField]
	private float _speed;

	public float Speed
	{
		get
		{
			return _speed;
		}
		set
		{
			_speed = value;
			UpdateMovementVector();
		}
	}
	private Vector3 _direction;
	public Vector3 Direction
	{
		get
		{
			return _direction;
		}
		set
		{
			_direction = value;
			UpdateMovementVector();
		}
	}

	private Vector3 movementVector;

	private void UpdateMovementVector()
	{
		movementVector = _direction.normalized * _speed;
		//projectileBody.velocity = Vector3.zero;
		//projectileBody.AddForce(movementVector);
	}

	private void FixedUpdate()
	{
		transform.position += movementVector;
	}

	private void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
	}

}
