using UnityEngine;
using System;
using System.Collections;

public class InputProcessor : MonoBehaviour {
	public static event Action<float> AxisXChanged;
	public static event Action<float> AxisZChanged;
	public static event Action<float> ZoomAxisChanged;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.A)){
			AxisXChanged(-1f);
		}
		if(Input.GetKey(KeyCode.D)){
			AxisXChanged(1f);
		}
		if(Input.GetKey(KeyCode.W)){
			AxisZChanged(1f);
		}
		if(Input.GetKey(KeyCode.S)){
			AxisZChanged(-1f);
		}
		if(Mathf.Abs(Input.mouseScrollDelta.y)>0f){
			ZoomAxisChanged(Input.mouseScrollDelta.y);
		}
	}
}
