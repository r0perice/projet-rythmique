using UnityEngine;
using System.Collections;
using System;

public class Music : MonoBehaviour
{
	public Loop[] loops;
	public Loop loop;

	public Music() {
		loop = GetComponent<Loop>();
	}

}

