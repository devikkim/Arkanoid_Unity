using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public int ballCount;
	public bool moveFlag;
	public Vector3 firstBallPosition;
	// Use this for initialization
	void Start () {
		moveFlag = true;
	}

	// Update is called once per frame
	void Update () {
	}
	void OnCollisionEnter2D(Collision2D col)
	{
		GameObject mCam = GameObject.Find ("Main Camera");;

		if (col.gameObject.name == "Bottom" && mCam.GetComponent<MainCamera> ().movingFlag) {
			GetComponent<Rigidbody2D> ().Sleep ();//ball의 움직임을 초기화
			moveFlag = false;
			mCam.GetComponent<MainCamera> ().flagList.Add (moveFlag);
			//mCam.GetComponent<MainCamera> ().movingFlag = moveFlag;
			//Debug.Log (moveFlag);

			mCam.GetComponent<MainCamera> ().ballPositionList.Add (this.transform.position);

			Vector3 pos = new Vector3 (mCam.GetComponent<MainCamera> ().ballPositionList [0].x,mCam.GetComponent<MainCamera> ().ballPositionList [0].y,mCam.GetComponent<MainCamera> ().ballPositionList [0].z);
			this.transform.position = new Vector3( pos.x,pos.y,pos.z);

		} else
			moveFlag = true;
	}

	void FixedUpdate()
	{
		
	}

}
