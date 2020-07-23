using UnityEngine;
using System.Collections;

public class FollowingCamera : MonoBehaviour {
	public float updateDelay;
	float nextUpdate;
	float xCoord;
	public float angularValue;
	public Transform target;

	// Use this for initialization
	void OnEnable () {
		TouchManager.TouchMoveEvent += onMove;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Vector3.Lerp(transform.position, target.position, 1f*Time.deltaTime);
	}

	void onMove(TouchInfo touch){
		if((transform.rotation.eulerAngles.x + touch.delta.y) > 320f && (transform.rotation.eulerAngles.x + touch.delta.y) < 360f)
		{
			xCoord = touch.delta.y;
		}
		else
		{
			xCoord = 0f;
		}
		transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(transform.rotation.eulerAngles + (new Vector3(xCoord,touch.delta.x,0) * angularValue)), Time.deltaTime*5f);
	}


	void OnDisable () {
		TouchManager.TouchMoveEvent -= onMove;
	}
}
