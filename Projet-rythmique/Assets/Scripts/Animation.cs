/*using UnityEngine;
using System.Collections;
using System;


public class Animation : MonoBehaviour {

	private Animator anim;
	private int event_cube = Animator.StringToHash("event_cube");
	private int event_cube1 = Animator.StringToHash("event_cube1");
	private int event_cube2 = Animator.StringToHash("event_cube2");

	public void Start ()
	{
		anim = GetComponent<Animator> ();
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
}*/
