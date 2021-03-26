using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(AudioSource))]
public class PlayerScript : MonoBehaviour
{

	public int health = 3;

	public float speed = 5;

	public float fireRate = 5f;

	public Camera playerCamera;

	public GameObject projectile;

	public float fireHeight;

	public TestUI testUI;

	public Transform shield;

	public GameObject DamageParticle;

	[Header("Sounds")]
	public AudioClip[] FireSounds;
	public AudioClip BlockSound;
	public AudioClip DamageSound;

	//Vector3 HorizontalInputModifier = new Vector3(0.5f, 0, -0.5f);
	//Vector3 VerticalInputModifier = new Vector3(0.5f, 0, 0.5f);

	private Vector3 movementDirection;

	private CharacterController characterController;

	private AudioSource audioSource;

	private float Cooldown;

	private int ammunition;

	public static PlayerScript Instance
	{
		get;
		private set;
	}

	private BasicEnemy.EnemyColor frontColor;

	// Start is called before the first frame update
	void Start()
    {
		characterController = GetComponent<CharacterController>();
		audioSource = GetComponent<AudioSource>();
		Instance = this;
		frontColor = BasicEnemy.EnemyColor.red;
		ammunition = 0;
		//testUI.SetFront(frontColor);
	}

    // Update is called once per frame
    void Update()
    {

		/*
		Vector3 movementDirection = Vector3.zero;
		movementDirection += HorizontalInputModifier * Input.GetAxis("Horizontal");
		movementDirection += VerticalInputModifier * Input.GetAxis("Vertical");

		transform.position += movementDirection;*/

		Vector3 directionInput = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		movementDirection = directionInput.normalized * speed;
		//Debug.Log("test:" + Input.GetJoystickNames()[0]);
		/* if(Input.GetJoystickNames().Length > 0)
		 {
			 //do rotation if joystick is connected
			 Debug.Log("test:" + Input.GetJoystickNames().Length);
		 }
		 else
		 {
			 Vector3 mousePos = Input.mousePosition;
			 Debug.Log(mousePos);
		 }*/
		bool handledInput = false;
		if (Input.GetJoystickNames().Length > 0)
		{
			for(int i = 0; i < Input.GetJoystickNames().Length; i++)
			{
				if(Input.GetJoystickNames()[i] != "")
				{
					//Debug.Log("t");
					handledInput = true;
					Vector2 inputLook = new Vector2(Input.GetAxis("LookX"), Input.GetAxis("LookY"));
					if(inputLook.x != 0 && inputLook.y != 0)
					{
						inputLook.Normalize();
						SetRotation(inputLook.x, inputLook.y);
					}
					else
					{
						if(Mathf.Abs(Input.GetAxisRaw("LookX")) > 0.19f || Mathf.Abs(Input.GetAxisRaw("LookY")) > 0.19f)
						{
							inputLook = new Vector2(Input.GetAxisRaw("LookX"), Input.GetAxisRaw("LookY"));
							inputLook.Normalize();
							SetRotation(inputLook.x, inputLook.y);
						}
					}
				}
			}
		}
		if (!handledInput)
		{
			UpdateRotationWithMouse();
		}
		if (Cooldown < fireRate)
		{
			Cooldown += Time.deltaTime;
		}else if (ammunition >0 && (Input.GetButtonDown("Fire1") || Input.GetAxis("ControllerFire") > 0.3))
		{
			Vector3 firePoint = transform.position;
			firePoint.y += fireHeight;
			GameObject projectileObject = Instantiate(projectile, firePoint, Quaternion.identity);
			PlayerProjectile firingProjectile = projectileObject.GetComponent<PlayerProjectile>();
			firingProjectile.Direction = transform.forward;
			audioSource.PlayOneShot(FireSounds[Random.Range(0,FireSounds.Length)]);

			ammunition--;
			Cooldown = 0;
		}

		if (Input.GetButtonDown("Fire2") || Input.GetAxis("ControllerShield") > 0.3)
		{
			RotateShield();
		}
	}

	private void RotateShield()
	{
		int shieldColor = (int)frontColor;
		shieldColor = (shieldColor + 1) % 3;
		frontColor = (BasicEnemy.EnemyColor)shieldColor;
		shield.Rotate(0, -120, 0);
		//testUI.SetFront(frontColor);
	}

	private void UpdateRotationWithMouse()
	{
		Vector3 mousePos = Input.mousePosition;
		//mousePos.z = 8.746911f;
		mousePos -= playerCamera.WorldToScreenPoint(transform.position);
		mousePos.z = 0;
		mousePos.Normalize();
		//Debug.Log(mousePos);
		SetRotation(mousePos.x, -mousePos.y);
	}

	private void SetRotation(float x, float y)
	{
		//Debug.Log(x +","+ y);
		float angle = Mathf.Atan2(y, x);
		//Debug.Log(angle);
		transform.rotation = Quaternion.Euler(0, (angle * Mathf.Rad2Deg)+90, 0);
	}

	private void FixedUpdate()
	{
		//transform.position += movementDirection;
		characterController.Move(movementDirection);
	}

	private void OnTriggerEnter(Collider other)
	{
		EnemyProjectile projectile = other.GetComponent<EnemyProjectile>();
		if(projectile != null && !projectile.MunitionHandled)
		{
			/*Vector3 difference = projectile.transform.position - transform.position;
			float impactAngle = Mathf.Atan2(difference.z, difference.x);
			if(difference.x < 0)
			{
				impactAngle += Mathf.PI;
			}else if(difference.z < 0)
			{
				impactAngle += Mathf.PI * 2;
			}
			impactAngle *= Mathf.Rad2Deg;
			Debug.Log(impactAngle);
			Debug.Log(transform.eulerAngles.y);
			float impactShield = impactAngle - transform.rotation.eulerAngles.y + 60;
			impactShield = ((impactShield%360)+360)%360; //prior totoday i dont think i realized mod didn follow the euclidean definition
			Debug.Log(impactShield);
			int shield = Mathf.FloorToInt(impactShield / 120);

			int hit = ((int)frontColor + shield)%3;*/
			Vector3 toPosition = (projectile.transform.position - transform.position).normalized;
			float angleToPosition = Vector3.SignedAngle(transform.right, toPosition,Vector3.up)+150;
			angleToPosition = ((angleToPosition % 360) + 360) % 360;
			int shield = Mathf.FloorToInt(angleToPosition / 120);
			int hit = ((int)frontColor + shield) % 3;
			/*switch (hit)
			{
				case 0:
					Debug.Log("hit red shield");
					break;
				case 1:
					Debug.Log("hit green shield");
					break;
				case 2:
					Debug.Log("hit blue shield");
					break;
			}*/
			if ((int)projectile.ProjectileColor == hit)
			{
				audioSource.PlayOneShot(BlockSound);
				ammunition++;
				
			}
			else
			{
				TakeDamage();
			}

			projectile.MunitionHandled = true;
		}
	}

	private void TakeDamage()
	{
		health--;
		audioSource.PlayOneShot(DamageSound);
		Vector3 firePoint = transform.position;
		firePoint.y += fireHeight;
		Instantiate(DamageParticle, firePoint, Quaternion.identity);
		if(health <= 0)
		{
			Destroy(gameObject);
		}
	}
}
