using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Plateform : MonoBehaviour
{
	[SerializeField] float touchedAnimationDuration = 1f;
	[SerializeField] float touchedYPos = -10f;
	[SerializeField] bool canBeTouched = true;

	private void OnTriggerEnter(Collider other)
	{
		if (!canBeTouched)
			return;
		if (other.gameObject.tag == "Player")
			TouchedAnimation();
	}

	private void TouchedAnimation()
	{
		float toPosition = transform.position.y + touchedYPos;
		transform.DOMoveY(toPosition, touchedAnimationDuration).SetEase(Ease.InOutElastic).OnComplete(OnAnimComplete);
	}

	private void OnAnimComplete()
	{
		transform.DOKill();
		Destroy(gameObject);
	}
}
