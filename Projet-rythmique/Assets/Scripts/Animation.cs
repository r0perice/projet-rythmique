using UnityEngine;
using System.Collections;
using System;


public class Animation : MonoBehaviour {

	Animator anim;
	int event_cube = Animator.StringToHash("event_cube");
	int event_cube1 = Animator.StringToHash("event_cube1");
	int event_cube2 = Animator.StringToHash("event_cube2");

	void Start ()
	{
		anim = GetComponent<Animator> ();
	}
	
	
	void Update ()
	{
		//PlayAnimation (i);
	}

	public void PlayAnimation(int i)
	{
		Debug.Log("Switch case");
		switch (i)
		{
			case 0:
				
				anim.SetTrigger(event_cube);
				break;

			case 1:
				anim.SetTrigger(event_cube1);
				break;

			case 2:
				anim.SetTrigger(event_cube2);
				break;

			default:
				break;
		}
	}
}
