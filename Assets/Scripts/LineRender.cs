using UnityEngine;
using System.Collections;

public class LineRender : MonoBehaviour {

	public float angleValue;

	Vector3 Position2;

	// Use this for initialization
	void Start () {
		angleValue = 0.0f;

		GetComponent<LineRenderer> ().SetWidth (0.02f, 0.02f);
		GetComponent<LineRenderer> ().SetColors (Color.black, Color.black);
		GetComponent<LineRenderer> ().SetPosition (0, transform.position);

		Position2 = new Vector3 (transform.position.x, transform.position.y+3.0f, transform.position.z);


		GetComponent<LineRenderer> ().SetPosition (1, Position2);
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 originPosition = new Vector3 (transform.position.x,transform.position.y,transform.position.z);
		//Debug.Log ("position1 : " + originPosition + "position2 : " + Position2);

		Vector2 v1 = new Vector2(rotate (originPosition, Position2, angleValue).x,rotate (originPosition, Position2, angleValue).y);

		GetComponent<LineRenderer> ().SetPosition (1, v1);
		GameObject mCam = GameObject.Find("Main Camera");
		mCam.GetComponent<MainCamera> ().rotatedFireValue = new Vector2 (v1.x-originPosition.x,v1.y);;
	}

	Vector2 rotate(Vector3 pivotPoint, Vector3 pointToRotate, float angle)
	{
		Vector2 result;
		float nX = (pointToRotate.x - pivotPoint.x);
		float nY = (pointToRotate.y - pivotPoint.y);
		angle = -angle * Mathf.PI / 180;

		result = new Vector2 (Mathf.Cos (angle) * nX - Mathf.Sin (angle) * nY + pivotPoint.x, 
			Mathf.Sin (angle) * nX + Mathf.Cos (angle) * nY + pivotPoint.z);

		return result;
	}

}
