using UnityEngine;
using System.Collections;

public class GameCameraControl : MonoBehaviour
{
	[SerializeField] Transform target;
	[SerializeField] Camera cam;

	[SerializeField] float xzAxisTargetSpeed;

	[SerializeField] float minZoomLevel;
	[SerializeField] float maxZoomLevel;
	float currentZoomLevel {get{return Vector3.Distance(cam.transform.position,target.transform.position);}}

	public float zoomSpeed;
	public float xAxisSpeed;
	public float yAxisSpeed;

  	void OnEnable()
	{
		TouchManager.TouchMoveEvent += OnTouchMove;
		InputProcessor.AxisXChanged += OnAxisXChanged;
		InputProcessor.AxisZChanged += OnAxisZChanged;
		InputProcessor.ZoomAxisChanged += OnZoomAxisChanged;
	}

	void OnDisable()
	{
		TouchManager.TouchMoveEvent -= OnTouchMove;
	}

  	void OnAxisXChanged(float amount) {
  		var dir = Vector3.Cross(Vector3.up,Vector3.ProjectOnPlane(target.transform.position-cam.transform.position, Vector3.up).normalized);
        target.transform.position += dir * amount * xzAxisTargetSpeed * Time.deltaTime;
        cam.transform.position += dir * amount * xzAxisTargetSpeed * Time.deltaTime;
    }
    void OnAxisZChanged(float amount) {
		var dir = Vector3.ProjectOnPlane(target.transform.position-cam.transform.position, Vector3.up).normalized;
        target.transform.position += dir * amount * xzAxisTargetSpeed * Time.deltaTime;
        cam.transform.position += dir * amount * xzAxisTargetSpeed * Time.deltaTime;
    }

    void OnZoomAxisChanged(float amount) {
    	if((amount > 0 && currentZoomLevel > minZoomLevel)||(amount < 0 && currentZoomLevel < maxZoomLevel)){
    		cam.transform.position += (target.transform.position-cam.transform.position)*amount*zoomSpeed * Time.deltaTime;
    	}
    }

	void OnTouchMove(TouchInfo info)
	{
		cam.transform.RotateAround(target.transform.position, Vector3.down, info.delta.x*Time.deltaTime * xAxisSpeed);
		cam.transform.RotateAround(target.transform.position, Vector3.Cross(cam.transform.position,Vector3.up), info.delta.y*Time.deltaTime * yAxisSpeed);
	}

	void LateUpdate()
	{
		cam.transform.LookAt(target.position);
	}

}
