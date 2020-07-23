using UnityEngine;
using System;
public interface ITargetable{
	Transform GetTransform();
	GameObject GetGameObject();
	IDamagable GetIDamagable();
	event Action OnDestroy;
}
