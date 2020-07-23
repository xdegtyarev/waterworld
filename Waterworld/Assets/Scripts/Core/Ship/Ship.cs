using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Ship : MonoBehaviour
{
	public Slot[] slots;
	public Item[] cargo;

	public float armor;
	public float weight;
	public float totalWeight;
	public float carryCapacity;

	public NavMeshAgent nav;

	public float GetTotalWeight()
	{
		return 0;
	}

	public void Attack(ITargetable target)
	{

	}

	public void Move(Vector3 target)
	{
		float speed = 0f;
		foreach (Engine e in from Slot s in slots where s.equipment is Engine select s.equipment) {
			speed += e.traction * totalWeight;
		}
	}
}
