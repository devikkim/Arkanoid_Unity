using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MainCamera : MonoBehaviour {

	// public valuable
	public GameObject boxs; 			// cube prefab
	public GameObject balls; 			// ball prefab
	public GameObject targetLine;		// line prefab
	public bool movingFlag; 				// true : moving, false : stop
	public Vector2 rotatedFireValue;	// rotated line vector value
	public List<GameObject> ballList; 	// saving list ball object
	public List<GameObject> boxList;	// savinve list cube object
	public int stageCount;				// Stage - Count
	public List<bool> flagList;
	public List<Vector3> ballPositionList;				// reset ball position value


	// local valuable
	GameObject line;					// line object

	bool shootFlag;
	bool firstStageFlag;
	int shootBallNumber = 0;


	private float fireRate = 0.1f;
	private float nextFire = 0.0f;
	// Use this for initialization
	void Start () {
		
		stageCount = 1;
		movingFlag = false;
		shootFlag = false;

		ballList = new List<GameObject> ();
		boxList = new List<GameObject> ();
		flagList = new List<bool> ();
		ballPositionList = new List<Vector3> ();
		ballPositionList.Add (new Vector3 (3.0f, 1.0f, 0.0f));
		// Generating cube function
		GenerateWall ();
		// Generating ball function
		GenerateBall ();
		// Generating line function
		GenerateLine ();
	}
	
	// Update is called once per frame
	void FixedUpdate()
	{
		// if ball was stopping
		if (movingFlag == true) {
			
			//Debug.Log ("flagList : "+flagList.Count);
			//Debug.Log ("stageCount : "+stageCount);

			if (flagList.Count == stageCount) {
				movingFlag = false;
				flagList.Clear ();
			}
			//Debug.Log (movingFlag);
			if (movingFlag == false) { // stop everything ball
				StageChange();

				GenerateBall ();
				GenerateWall ();
				GenerateLine ();
				ResetBallPosition ();
				//Debug.Log (stageCount);
			} 

		}
		//Debug.Log (movingFlag);
	}
	void Update () {

		// fire button is space bar key
		if (shootFlag && Time.time > nextFire ) {
			nextFire = Time.time + fireRate;
			ShootBall (shootBallNumber);
			shootBallNumber++;
			if (shootBallNumber == stageCount) {
				shootBallNumber = 0;
				shootFlag = false;
			}
		}


		if (Input.GetKey (KeyCode.Space)) {
			Destroy (line);

			shootFlag = true;
			movingFlag = true;
			ballPositionList.Clear ();
		}

		// fire angle button left, right button
		if (Input.GetKey (KeyCode.LeftArrow)) {
			//Debug.Log ("left");
			if(line != null)
				line.GetComponent<LineRender>().angleValue -= 0.5f;
		}

		if (Input.GetKey (KeyCode.RightArrow)) {
			if(line != null)
				line.GetComponent<LineRender>().angleValue += 0.5f;
		}

	}

	void ShootBall(int i)
	{
		Vector2 angleVector = new Vector2( rotatedFireValue.x, rotatedFireValue.y);
		GameObject ball1 = ballList [i];
		ball1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (angleVector.x*2.0f, angleVector.y*2.0f);
		ball1.GetComponent<Ball> ().moveFlag = true;
	}
	void GenerateWall()
	{
		var count = Random.Range (1, 5);
		for (int i = 0; i < count; i++) {
			var x1 = Random.Range (1, 5);
			GameObject box1 = (GameObject)Instantiate (boxs, new Vector3 (x1, 7.0f, 0.0f), Quaternion.identity);
			box1.GetComponent<Box> ().boxCount = stageCount;
		
			boxList.Add (box1);
			//Debug.Log ("boxList start count : "+boxList.Count);
		}
	}
	void GenerateBall()
	{
		GameObject ball = (GameObject)Instantiate(balls, ballPositionList[0], Quaternion.identity);
		ball.GetComponent<Ball> ().ballCount = stageCount;
		ballList.Add (ball);
	}

	void GenerateLine()
	{
		line = (GameObject)Instantiate(targetLine, new Vector3 (ballList[0].transform.position.x,ballList[0].transform.position.y,-0.1f), Quaternion.identity);
	}

	void StageChange()
	{
		for (int i = 0; i < boxList.Count; i++) { // change box position 
			Vector3 boxPos = boxList [i].transform.position;
			boxPos.y--;

			if (boxPos.y == 1)
				Debug.Log ("Game Over");

			boxList [i].transform.position = boxPos;
		}
		stageCount++;
	}
		
	void ResetBallPosition()
	{
		
	}

}
