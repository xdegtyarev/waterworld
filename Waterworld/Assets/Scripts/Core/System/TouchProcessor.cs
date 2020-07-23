using UnityEngine;
using System.Collections;

public class TouchProcessor : MonoBehaviour
{
		public static bool inputEnabled = true;
		public static event System.Action<Vector3> waterTapEvent;
		public static event System.Action<GameObject> enemyTapEvent;

}