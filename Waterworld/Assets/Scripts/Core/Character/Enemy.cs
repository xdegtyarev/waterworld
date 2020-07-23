using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public Ship ship;
	public bool isArgued;
	public Vector3 LastKnownPosition;
	public float ArgueRange;
	public float GroupArgueRange;
	public float CheckPlayerUpdateLatency;
	public float CheckGroupToArgueUpdateLatency;
	private float nextGroupArgueRangeCheck;
	private float nextPlayerInArgueRangeCheck;
	
	void Update ()
	{
		CheckForPlayer ();
	}
	
	
	void CheckGroupToArgue ()
	{
		if (isArgued) {
			if (Time.timeSinceLevelLoad > nextGroupArgueRangeCheck) {
				nextGroupArgueRangeCheck += CheckGroupToArgueUpdateLatency;
				RaycastHit hitInfo = new RaycastHit ();
				foreach (var o in Physics.OverlapSphere(transform.position,ArgueRange)) {
					if (o.gameObject.GetComponent<Enemy> () != null) {
						o.gameObject.GetComponent<Enemy> ().Argue (LastKnownPosition);
					}
				}
			}
		}
	}
	
	void CheckForPlayer ()
	{
		if (Time.timeSinceLevelLoad > nextPlayerInArgueRangeCheck) {
			nextPlayerInArgueRangeCheck += CheckPlayerUpdateLatency;
			/*
			if(Physics.OverlapSphere(transform.position,ArgueRange).Length > 0){
				foreach (var o in Physics.OverlapSphere(transform.position,ArgueRange)) {
					if (o.gameObject.GetComponent<Player> () != null) {
						isArgued = true;
						LastKnownPosition = o.transform.position;
						break;
					} else {
						LastKnownPosition = transform.position;
						isArgued = false;
					}
				}
			}
			
			if (isArgued) {
				ship.Move (LastKnownPosition);
			}
			*/
		}
	}
	
	void Argue (Vector3 position)
	{
		if (!isArgued) {
			isArgued = true;
			LastKnownPosition = position;
		}
	}
}
