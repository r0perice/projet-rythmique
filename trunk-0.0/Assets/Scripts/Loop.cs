using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Loop : MonoBehaviour
{
	private Sound s;
	private float timeDown = 0.0f; 
	private bool running = true;

	private int i=0;
	public List<Sound> sound = new List<Sound>();
	public int dureeBoucle;

	void Start()
	{
		Debug.Log ("Running...");
		if (!running)
			return;

		s = GetComponent<Sound>();
	}

	void Update()
	{

		timeDown += Time.deltaTime;
		getInput ();

		if (timeDown >= dureeBoucle) {
		timeDown = 0.0f;
	    }

		playSound ();

	}

	void getInput()
	{
		if (Input.GetKeyDown ("q")) 
		{
			Debug.Log ("The Q key was pressed");
			i = 0;
			AddSound();
		}
		
		if (Input.GetKeyDown ("s")) 
		{
			Debug.Log ("The S key was pressed");
			i = 1;
			AddSound();
		}
		
		if (Input.GetKeyDown ("d")) 
		{
			Debug.Log ("The D key was pressed");
			i = 2;
			AddSound();
		}
		
		if (Input.GetKeyDown ("z"))
		{
			Debug.Log ("The Z key was pressed");
			var result = 	from so in sound
				orderby so.time ascending
					select so;
			
			foreach (var so in result)
				Debug.Log (String.Format("timeDown : {0} / clip : {1}",so.time,so.clip));
			
		}
		
		if (Input.GetKeyDown ("a"))
		{
			Debug.Log ("The A key was pressed");
			Debug.Log (String.Format("sound.time : {0} ",sound[0].time));
			
		}
		
		if (Input.GetKeyDown ("e"))
		{
			Debug.Log ("The E key was pressed");
			Debug.Log (timeDown);
			
		}
	}
	void AddSound()
	{
		//Sound(AudioClip[] newClip, float newTime, int newWeight, string newName)
		sound.Add(new Sound(s.clips[i],timeDown,0,s.clips[i].name));
	}

	void playSound(){
		foreach (Sound son in sound)
		{
			if (Math.Abs(son.time-timeDown)<=0.02f)
			{
				GetComponent<AudioSource>().clip = son.clip;
				GetComponent<AudioSource>().Play();
			}
		}
	}



}
	