using System.Linq;
using UnityEngine;
using System.Reflection;
public class Slot: MonoBehaviour {
	public IEquipable equipment;
	void Awake(){
		foreach(string o in Assembly
		          .GetAssembly(typeof(IEquipable))
		          .GetTypes()
		          .Where(t => typeof(IEquipable).IsAssignableFrom(t) && t != typeof(IEquipable))
		          .Select(t => t.Name).ToArray())
		{
			Debug.Log(o);
		}
		
	}
}
