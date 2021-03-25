using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class BasicEnemy : MonoBehaviour
{


	public float MoveDistance;

	public float speed;

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
		if(targetDiff.sqrMagnitude > MoveDistance * MoveDistance)
		{
			movementDirection = targetDiff.normalized * speed;
		}
    }

	private void FixedUpdate()
	{
		characterController.Move(movementDirection);
	}
}
