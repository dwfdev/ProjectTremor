using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///		Script Manager:	Denver
///		Description:	Makes gameObjects Collider permanently
///						visible in editor
///		Date Modified:	19/10/2018

public enum eColour {
	CYAN,
	GREEN,
	RED,
	BLACK,
	YELLOW,
	BLUE,
	MAGENTA,
	GRAY,
	WHITE,
	CLEAR,
	GREY
}
	
public class ShowCollider : MonoBehaviour {


	[Tooltip("Colour of collider")]
	[SerializeField] private eColour m_colour;

	void OnDrawGizmos() {

		// select colour
		switch(m_colour) {
			case eColour.CYAN:
				Gizmos.color = Color.cyan;
				break;

			case eColour.GREEN:
				Gizmos.color = Color.green;
				break;

			case eColour.RED:
				Gizmos.color = Color.red;
				break;

			case eColour.BLACK:
				Gizmos.color = Color.black;
				break;

			case eColour.YELLOW:
				Gizmos.color = Color.yellow;
				break;

			case eColour.BLUE:
				Gizmos.color = Color.blue;
				break;

			case eColour.MAGENTA:
				Gizmos.color = Color.magenta;
				break;

			case eColour.GRAY:
				Gizmos.color = Color.gray;
				break;

			case eColour.WHITE:
				Gizmos.color = Color.white;
				break;

			case eColour.CLEAR:
				Gizmos.color = Color.clear;
				break;

			case eColour.GREY:
				Gizmos.color = Color.grey;
				break;

			default:
				Gizmos.color = Color.red;
				break;
		}
		
		// make sure collider is a trigger
		BoxCollider col = GetComponent<BoxCollider>();
		col.isTrigger = true;

		Vector3 drawBoxVector = new Vector3 (
			transform.lossyScale.x * col.size.x,
			transform.lossyScale.y * col.size.y,
			transform.lossyScale.z * col.size.z
		);
		Vector3 drawBoxPosition = transform.position + col.center;

		Gizmos.matrix = Matrix4x4.TRS(drawBoxPosition, transform.rotation, drawBoxVector);
		
		// draw wire frame
		Gizmos.DrawWireCube(Vector3.zero, Vector3.one);

		// shade faces
		Gizmos.color *= new Color(1, 1, 1, 0.25f);
		Gizmos.DrawCube(Vector3.zero, Vector3.one);
	}
}
