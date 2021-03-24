using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

	public GameObject Target;

	public Vector3 PositionRelativeTo;

    // Update is called once per frame
    void Update()
    {
		transform.position = Target.transform.position + PositionRelativeTo;
    }
}
