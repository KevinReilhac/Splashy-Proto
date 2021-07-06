using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private Transform playerModel = null;
	[Header("Parameters")]
	[SerializeField] private float forwardSpeed = 10f;
	[SerializeField] private float yAmplitude = 4f;
	[SerializeField] private float ySpeed = 1f;
	[SerializeField] private float xMoveSpeed = 3f;

	private Vector3 modelStartPosition = Vector3.zero;
	private bool asStart = false;
	private float time = 0f;

	private void Awake()
	{
		modelStartPosition = transform.position;
		Lean.Touch.LeanTouch.OnFingerUpdate += GestureHandler;
		Lean.Touch.LeanTouch.OnFingerDown += StartPlay;
	}

	private void OnDestroy()
	{
		Lean.Touch.LeanTouch.OnFingerUpdate -= GestureHandler;
	}

	private void StartPlay(Lean.Touch.LeanFinger touchData)
	{
		asStart = true;
		Lean.Touch.LeanTouch.OnFingerDown -= StartPlay;
	}

	private void Update()
	{
		if (asStart)
		{
			UpdateYPosition();
			UpdateZPosition();
		}
	}

	private void GestureHandler(Lean.Touch.LeanFinger gestureData)
	{
		Vector3 currentPosition = transform.position;

		currentPosition.x += gestureData.SwipeScreenDelta.x * xMoveSpeed * Time.deltaTime;
		transform.position = currentPosition;
	}

	private void UpdateZPosition()
	{
		Vector3 currentPosition = transform.position;

		currentPosition.z += Time.deltaTime * forwardSpeed;
		transform.position = currentPosition;
	}

	private void UpdateYPosition()
	{
		time += Time.deltaTime;
		Vector3 currentPosition = playerModel.transform.position;

		currentPosition.y = modelStartPosition.y + Mathf.Abs(Mathf.Sin(time * ySpeed) * yAmplitude);
		playerModel.transform.position = currentPosition;
	}
}
