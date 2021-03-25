using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
	private BasicEnemy.EnemyColor _color;

	public BasicEnemy.EnemyColor ProjectileColor
	{
		get
		{
			return _color;
		}
		set
		{
			_color = value;
		}
	}

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

	//private Rigidbody projectileBody;
    // Start is called before the first frame update
  //  void Awake()
  //  {
		//projectileBody = GetComponent<Rigidbody>();
  //  }

	private void UpdateMovementVector()
	{
		movementVector = _direction.normalized * _speed;
		//projectileBody.velocity = Vector3.zero;
		//projectileBody.AddForce(movementVector);
	}

	private void UpdateColor()
	{
		throw new NotImplementedException();
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
