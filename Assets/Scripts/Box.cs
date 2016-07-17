using UnityEngine;
using System.Collections;

public class Box : MonoBehaviour {

	public int boxCount;
	public int identityNumber;

	GameObject textObject;
	// Use this for initialization
	void Start () {
		textObject = transform.FindChild ("countText").gameObject;
		textObject.GetComponent<TextMesh> ().text = boxCount.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionExit2D(Collision2D col)
	{
		//Debug.Log (col.gameObject.name);
		if (col.gameObject.name == "ball(Clone)") {
			boxCount--;
			textObject.GetComponent<TextMesh> ().text = boxCount.ToString();
			if (boxCount == 0) {
				//Debug.Log ("destroy");

				GameObject mCam = GameObject.Find("Main Camera");
				mCam.GetComponent<MainCamera> ().boxList.Remove (this.gameObject);
				//Debug.Log ("boxList End count : "+mCam.GetComponent<MainCamera> ().boxList.Count);
				Destroy (this.gameObject);
			}
		}
	}

	void FixedUpdate(){
		
	}
}
