/////<summary>
/////		Script Manager:	Denver
/////		Description:	Handles the movement of the player using the mouse
/////		Date Modified:	20/09/2018
/////</summary>

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PlayerActor : MonoBehaviour
//{
//	[Tooltip("Scales raw mouse movement.")]
//	[SerializeField]
//	private float fMouseSensitivity;

//	[Tooltip("Lessens jitter. Too high a value makes it unresponsive.")]
//	[SerializeField]
//	private float fMouseSmoothing;

//	private float fMouseSmooth;

//	// Use this for initialization
//	void Start()
//	{

//	}

//	// Update is called once per frame
//	void Update()
//	{

//		#region Mouse Movement
//		// get the movement of the mouse
//		Vector3 v3MouseMovement = new Vector3(Input.GetAxisRaw("Mouse X"), 0, Input.GetAxisRaw("Mouse Y"));

//		// scale it by sensitivity
//		v3MouseMovement = Vector3.Scale(v3MouseMovement, new Vector3(fMouseSensitivity, 0, fMouseSensitivity));

//		// create a smoothed movement vector to move the player by
//		fMouseSmooth.x = Mathf.Lerp(mouseSmooth.x, v3MouseMovement.x, 1 / fMouseSmoothing);
//		fMouseSmooth.z = Mathf.Lerp(mouseSmooth.z, v3MouseMovement.z, 1 / fMouseSmoothing);

//		// move player
//		transform.Translate(fMouseSmooth * Time.deltaTime);
//		#endregion

//	}
//}
