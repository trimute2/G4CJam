using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class BasicEnemy : MonoBehaviour
{

	public enum EnemyColor
	{
		red,
		green,
		blue
	}

	public EnemyColor color;

	public float MoveDistance;

	public float fireDistance;

	public float speed;

	public float fireRate = 5f;

	public GameObject projectile;

	public float fireHeight;

	[Header("Color Change Stuff")]
	public Material redMat;
	public Material greenMat;
	public Material blueMat;
	public MeshRenderer ColorThings;

	public Action OnDeath;

	private float Cooldown;

	private CharacterController characterController;
	// Start is called before the first frame update

	private Vector3 movementDirection;

    void Start()
    {
		characterController = GetComponent<CharacterController>();
		ChangeColor(color);
    }

    // Update is called once per frame
    void Update()
    {
		movementDirection = Vector3.zero;
		if (PlayerScript.Instance != null)
		{
			Vector3 targetDiff = PlayerScript.Instance.transform.position - transform.position;
			targetDiff.y = 0;
			transform.rotation = Quaternion.LookRotation(targetDiff);
			if (targetDiff.sqrMagnitude > MoveDistance * MoveDistance)
			{
				movementDirection = targetDiff.normalized * speed;
			}
			if (Cooldown < fireRate)
			{
				Cooldown += Time.deltaTime;
			}
			else if (targetDiff.sqrMagnitude <= fireDistance * fireDistance)
			{
				Vector3 firePoint = transform.position;
				firePoint.y += fireHeight;
				GameObject projectileObject = Instantiate(projectile, firePoint, Quaternion.identity);
				EnemyProjectile firingProjectile = projectileObject.GetComponent<EnemyProjectile>();
				firingProjectile.Direction = targetDiff;
				firingProjectile.ProjectileColor = color;
				Cooldown = 0;
			}
		}
    }

	public void ChangeColor(BasicEnemy.EnemyColor newColor)
	{
		color = newColor;
		Material mat = null;
		switch (color)
		{
			case EnemyColor.red:
				mat = redMat;
				break;
			case EnemyColor.green:
				mat = greenMat;
				break;
			case EnemyColor.blue:
				mat = blueMat;
				break;
		}
		ColorThings.material = mat;
	}

	private void FixedUpdate()
	{
		characterController.Move(movementDirection);
	}

	private void OnTriggerEnter(Collider collision)
	{
		PlayerProjectile proj = collision.GetComponent<PlayerProjectile>();
		if(proj != null)
		{
			Die();
		}
	}

	private void Die()
	{
		OnDeath?.Invoke();
		Destroy(gameObject);
	}
}
