using UnityEngine;
using System;

public class TouchManager : MonoBehaviour {
	const int MaxTouches = 5;
	static TouchInfo[] touchCache = new TouchInfo[MaxTouches];
	static TouchInfo touch;
	static bool mouseButtonIsDown;

	static float touchDistance;

	public delegate void TouchManagerEventHandler (TouchInfo touch);

	public static event TouchManagerEventHandler TouchBeganEvent;
	public static event TouchManagerEventHandler TouchEndedEvent;
	public static event TouchManagerEventHandler TouchMoveEvent;
	public static event TouchManagerEventHandler TouchHoldEvent;
	public static event TouchManagerEventHandler MoveRightEvent;
	public static event TouchManagerEventHandler MoveLeftEvent;
	public static event TouchManagerEventHandler MoveUpEvent;
	public static event TouchManagerEventHandler MoveDownEvent;
	public static event Action<float> PinchInEvent;
	public static event Action<float> PinchOutEvent;

	void Update () {
		ProcessTouches ();
	}

	static void ProcessTouches () {
		touch = new TouchInfo ();
		if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) {
			ProcessRealTouches ();
		} else {
			ProcessMouseEvents ();
		}
	}

	static void ProcessMouseEvents () {
		//PRESS
		if (Input.GetMouseButtonDown (0)) {
			mouseButtonIsDown = true;
			touch.id = 0;
			touch.position = Input.mousePosition;
			touch.phase = TouchPhase.Began;
			touchCache [0] = touch;

			if (TouchBeganEvent != null) {
				TouchBeganEvent (touch);
			}
		}
		//RELEASE
		if (Input.GetMouseButtonUp (0)) {
			if (mouseButtonIsDown) {
				mouseButtonIsDown = false;
				touch = touchCache [0];
				touch.phase = TouchPhase.Ended;
				touch.UpdatePosition (Input.mousePosition);

				if (TouchEndedEvent != null) {
					TouchEndedEvent (touch);
				}

				touchCache [0] = null;
			}
		}
		//MOVE
		if (mouseButtonIsDown) {
			touch = touchCache [0];
			touch.UpdatePosition (Input.mousePosition);
			
			if (touch.IsStationary) {
				touch.phase = TouchPhase.Stationary;
				touchCache [0] = touch;
				if (TouchHoldEvent != null) {
					TouchHoldEvent (touch);
				}
			} else {
				touch.phase = TouchPhase.Moved;
				touchCache [0] = touch;
				if (TouchMoveEvent != null) {
					TouchMoveEvent (touch);
				}
				
				switch (touch.GetTouchMoveDirection ()) {
					case TouchInfo.TouchMoveDirection.UP:
						if (MoveUpEvent != null) {
							MoveUpEvent (touch);
						}
						break;
					case TouchInfo.TouchMoveDirection.RIGHT:
						if (MoveRightEvent != null) {
							MoveRightEvent (touch);
						}
						break;
					case TouchInfo.TouchMoveDirection.DOWN:
						if (MoveDownEvent != null) {
							MoveDownEvent (touch);
						}
						break;
					case TouchInfo.TouchMoveDirection.LEFT:
						if (MoveLeftEvent != null) {
							MoveLeftEvent (touch);
						}
						break;
					case TouchInfo.TouchMoveDirection.OTHER:
						break;
				}
			}
		}
	}
		
	static void ProcessRealTouches () {
		if (Input.touchCount >= 2) {
			ProcessPinch();
		}else if(Input.touchCount > 0){
			int i = 0;
			touchDistance = 0f;
			Touch unityTouch = Input.GetTouch (i);

			switch (unityTouch.phase) {
			case TouchPhase.Began:
				touch.id = unityTouch.fingerId;
				touch.position = unityTouch.position;
				touch.phase = unityTouch.phase;
				touchCache [touch.id] = touch;
				if (TouchBeganEvent != null) {
					TouchBeganEvent (touch);
				}
				break;
			case TouchPhase.Moved:
				touch = touchCache [unityTouch.fingerId];
				touch.UpdatePosition (unityTouch.position);
					
				if (touch.IsStationary) {
					goto case TouchPhase.Stationary;
				}

				touch.phase = TouchPhase.Moved;
				touchCache [touch.id] = touch;
				if (TouchMoveEvent != null) {
					TouchMoveEvent (touch);
				}

				switch (touch.GetTouchMoveDirection ()) {
				case TouchInfo.TouchMoveDirection.UP:
					if (MoveUpEvent != null) {
						MoveUpEvent (touch);
					}
					break;
				case TouchInfo.TouchMoveDirection.RIGHT:
					if (MoveRightEvent != null) {
						MoveRightEvent (touch);
					}
					break;
				case TouchInfo.TouchMoveDirection.DOWN:
					if (MoveDownEvent != null) {
						MoveDownEvent (touch);
					}
					break;
				case TouchInfo.TouchMoveDirection.LEFT:
					if (MoveLeftEvent != null) {
						MoveLeftEvent (touch);
					}
					break;
				case TouchInfo.TouchMoveDirection.OTHER:
					break;
				}
				break;
			case TouchPhase.Stationary:
				touch = touchCache [unityTouch.fingerId];
				touch.phase = TouchPhase.Stationary;
				touch.UpdatePosition (unityTouch.position);	
				touchCache [touch.id] = touch;
				if (TouchHoldEvent != null) {
					TouchHoldEvent (touch);
				}
				break;
			case TouchPhase.Ended:
				touch = touchCache [unityTouch.fingerId];
				touch.phase = TouchPhase.Ended;
				touch.UpdatePosition (unityTouch.position);
				if (TouchEndedEvent != null) {
					TouchEndedEvent (touch);
				}
				touchCache [unityTouch.fingerId] = null;
				break;
			}
		}
	}

	static float pinchThreshold = 0.01f;

	static void ProcessPinch(){
		if (touchDistance < pinchThreshold && touchDistance > -pinchThreshold) { 
			touchDistance = Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

		} else {
			float delta = touchDistance - Vector2.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
			touchDistance += delta;
			if (delta > pinchThreshold) {
				PinchInEvent(Mathf.Abs(delta));
			} else if (delta < -pinchThreshold) {
				PinchOutEvent(Mathf.Abs(delta));
			}
		}
	}	
}