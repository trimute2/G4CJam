using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerScript : MonoBehaviour
{

	public float speed = 5;

	public Camera camera;

	//Vector3 HorizontalInputModifier = new Vector3(0.5f, 0, -0.5f);
	//Vector3 VerticalInputModifier = new Vector3(0.5f, 0, 0.5f);

	private Vector3 movementDirection;

	private CharacterController characterController;

	// Start is called before the first frame update
	void Start()
    {
		characterController = GetComponent<CharacterController>();

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
		Debug.Log("test:" + Input.GetJoystickNames().Length);
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
		UpdateRotationWithMouse();

	}

	private void UpdateRotationWithMouse()
	{
		Vector3 mousePos = Input.mousePosition;
		//mousePos.z = 8.746911f;
		mousePos -= camera.WorldToScreenPoint(transform.position);
		mousePos.z = 0;
		mousePos.Normalize();
		//Debug.Log(mousePos);
		float angle = Mathf.Atan2(-mousePos.y, mousePos.x);
		//Debug.Log(angle);
		transform.rotation = Quaternion.Euler(0, angle * Mathf.Rad2Deg,0);
	}

	private void FixedUpdate()
	{
		//transform.position += movementDirection;
		characterController.Move(movementDirection);
	}
}
