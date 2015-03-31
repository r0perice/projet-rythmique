using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Loop : MonoBehaviour
{
	private Sound s;
	public GameObject[] cubes;
	public GameObject cube;
	public Animation a;
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

		a = cube.gameObject.GetComponent<Animation> ();
		s = GetComponent<Sound> ();
	}

	void Update()
	{

		timeDown += Time.deltaTime;
		getInput ();

		if (timeDown >= dureeBoucle) {
		timeDown = 0.0f;
		trierSon();
		ajusterSon(1.0f);
	    }

		playSoundAndAnimation ();

	}

	void getInput()
	{
		if (Input.GetKeyDown ("q")) 
		{
			i = 0;
			AddSound(0);
		}

		if (Input.GetKeyDown ("a")) 
		{
			Debug.Log ("a");
			i = 0;
			AddSound(1);
		}
		
		if (Input.GetKeyDown ("s")) 
		{
			i = 1;
			AddSound(0);
		}
		
		if (Input.GetKeyDown ("d")) 
		{
			i = 2;
			AddSound(0);
		}
	}

	void AddSound(int _weight)
	{
		Sound son = new Sound (s.clips [i], timeDown, 0, s.clips [i].name, i);
		son.weight = _weight;
		sound.Add (son);
	}

	void playSound()
	{
		foreach (Sound son in sound) {
			if (Math.Abs (son.time - timeDown) <= 0.02f) 
			{
				GetComponent<AudioSource> ().clip = son.clip;
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	void playSoundAndAnimation()
	{
		foreach (Sound son in sound) {
			if (Math.Abs (son.time - timeDown) <= 0.02f) 
			{
				GetComponent<AudioSource> ().clip = son.clip;
				GetComponent<AudioSource> ().Play ();
				a.PlayAnimation(son.anim);
			}
		}
	}

	void ajusterSon(float marge) 
		{
			for (int i =0; i<sound.Count(); i++) 
			{
				if (sound[i].weight==1)
				{
					for (int j =0; j<sound.Count(); j++) 
					{
						if((Math.Abs(sound[j].time-sound[i].time))<=marge && sound[j].weight==0) 
						{
							sound.Remove(sound[j]);
						}
					}
				}
			}
		}

		void trierSon() {
			var result = from so in sound
				orderby so.time ascending
					select so;
		}

}
	