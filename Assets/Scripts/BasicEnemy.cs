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

	private float Cooldown;

	private CharacterController characterController;
	// Start is called before the first frame update

	private Vector3 movementDirection;

    void Start()
    {
		characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
		movementDirection = Vector3.zero;
		Vector3 targetDiff = PlayerScript.Instance.transform.position - transform.position;
		targetDiff.y = 0;
		if(targetDiff.sqrMagnitude > MoveDistance * MoveDistance)
		{
			movementDirection = targetDiff.normalized * speed;
		}
		if(Cooldown < fireRate)
		{
			Cooldown += Time.deltaTime;
		}
		else if(targetDiff.sqrMagnitude <= fireDistance * fireDistance)
		{
			GameObject projectileObject = Instantiate(projectile, transform.position, Quaternion.identity);
			EnemyProjectile firingProjectile = projectileObject.GetComponent<EnemyProjectile>();
			firingProjectile.Direction = targetDiff;
			firingProjectile.ProjectileColor = color;
			Cooldown = 0;
		}
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
		Destroy(gameObject);
	}
}
