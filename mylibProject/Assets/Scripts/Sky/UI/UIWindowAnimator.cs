using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Animator) )]
public class UIWindowAnimator : UIWindow
{
	
	//Animator State and Transition names we need to check against.
	const string k_OpenTransitionName = "Open";
	const string k_ClosedStateName = "Closed";
	const string k_NormalStateName = "Normal";
	const string k_OpenEndStateName = "OpenEnd";
	const string k_ClosedEndStateName = "ClosedEnd";
	const int layer_Index = 0;

	public override void Init ()
	{
		animator = gameObject.GetComponent<Animator> ();
		base.Init ();
	}

	Animator animator;

	protected override void customIn ()
	{
		animator.Play (k_OpenTransitionName);
		StartCoroutine (Open (animator));
	}

	IEnumerator Open (Animator anim)
	{
		InAction.OnStartMethod ();
		if (anim != null) {
			bool closedStateReached = anim.GetCurrentAnimatorStateInfo (layer_Index).IsName (k_OpenEndStateName);
			while (!closedStateReached) {
				if (!anim.IsInTransition (layer_Index))
					closedStateReached = anim.GetCurrentAnimatorStateInfo (layer_Index).IsName (k_OpenEndStateName);
				yield return new WaitForEndOfFrame ();
			}
		}
		InAction.OnCompleteMethod ();
	}

	protected override void customOut ()
	{
		animator.Play (k_ClosedStateName);
		StartCoroutine (Closed (animator));
	}

	IEnumerator Closed (Animator anim)
	{
		OutAction.OnStartMethod ();
		bool closedStateReached = anim.GetCurrentAnimatorStateInfo (layer_Index).IsName (k_ClosedEndStateName);
		if (anim != null) {
			while (!closedStateReached) {
				if (!anim.IsInTransition (layer_Index))
					closedStateReached = anim.GetCurrentAnimatorStateInfo (layer_Index).IsName (k_ClosedEndStateName);
				yield return new WaitForEndOfFrame ();
			}
		}
		OutAction.OnCompleteMethod ();
	}
}
