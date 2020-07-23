using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
	public Ship playersShip;
	public static Game instance;
	public GameObject tapFxPrefab;

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		TouchProcessor.waterTapEvent += MoveTapEvent;
		TouchProcessor.enemyTapEvent += HandleEnemyTapEvent;
	}

	void HandleEnemyTapEvent (GameObject obj)
	{
		Debug.Log ("SHOOTING");
		playersShip.Attack((ITargetable)obj.GetComponent(typeof(ITargetable)));
	}

	public void MoveTapEvent (Vector3 position)
	{
		playersShip.Move(position);
		tapFxPrefab.transform.position = position;
	}

	void OnDisable()
	{
		TouchProcessor.waterTapEvent -= MoveTapEvent;
		TouchProcessor.enemyTapEvent -= HandleEnemyTapEvent;
	}
}
