using UnityEngine;
using System.Collections;

public class Weapon : Item,IEquipable
{
	public float range;
	public float reloadTime;
	public float accuracy;
	public float damage;

	public GameObject fxPrefab;
	public Transform fxPivot;
	public ITargetable target;

	private float nextAttackTime;

	public void SetTarget(ITargetable t)
	{
		target = t;
		target.OnDestroy += ResetTarget;
		nextAttackTime = Time.timeSinceLevelLoad;
	}

	public void Update()
	{
		if(target != null)
		{
			if(Time.timeSinceLevelLoad >= nextAttackTime){
				if(Vector3.Distance(transform.position,target.GetTransform().position) <= range){
					Shoot ();
				}
			}
		}
	}

	void Shoot()
	{	
		transform.LookAt(target.GetTransform());
		//Destroy(Instantiate(fxPrefab,fxPivot.position,fxPivot.rotation),0.5f);
		if(UnityEngine.Random.Range(0f,1f)<accuracy){
			target.GetIDamagable().Hit(damage);
		}else{
			Debug.Log("Miss");
		}
		Reload();
	}

	void Reload(){
		nextAttackTime = Time.timeSinceLevelLoad + reloadTime;
	}

	void ResetTarget()
	{
		target = null;
	}
}

