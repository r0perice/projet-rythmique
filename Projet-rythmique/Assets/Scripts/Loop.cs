using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Loop : MonoBehaviour
{
	private Sound sound;
	private Animation animation;

	private float adjustedTime = 0.0f;
	private bool adjustment = false;
	private float loopTime = 0.0f;
	private float ratio;
	private int clipNumber;
	private int loopDuration;
	public double accuracy;

	public GameObject[] cubes;
	public List<Sound> sounds = new List<Sound>();
	public List<Animation> animations = new List<Animation>();



	public Loop(int _loopDuration) 
	{
		this.loopDuration = _loopDuration;
	}


	public void Start()
	{
		ratio = Time.fixedDeltaTime / 0.02f;

		for (int j = 0; j<cubes.Count(); j++) {
			animation = cubes[j].gameObject.GetComponent<Animation> ();
			animations.Add(animation);
		}

		sound = GetComponent<Sound> ();
	}

	
	public void AddSound()
	{
		//Debug.Log ("s.clips[i]:" + s.clips [i]);
		//Debug.Log ("looptime:" + loopTime);
		//Debug.Log ("i:"+i);
		Sound son = new Sound (sound.clips[clipNumber], loopTime, sound.clips [clipNumber].name, clipNumber);
		adjustSound(son, 0.25f);
		Sound _son = new Sound (sound.clips[clipNumber], adjustedTime, sound.clips[clipNumber].name, clipNumber);
		sounds.Add (_son);
		son = null;
		adjustment = false;
	}

	public void playSound()
	{
		foreach (Sound son in sounds) {
			if ((Math.Abs (son.Time - loopTime + Time.fixedDeltaTime/ratio) <= Time.fixedDeltaTime*accuracy))
			{
				GetComponent<AudioSource> ().clip = son.Clip;
				GetComponent<AudioSource> ().Play ();
			}
		}
	}

	public void playSoundAndAnimation()
	{
		foreach (Sound son in sounds) {
			if ((Math.Abs (son.Time - loopTime + Time.fixedDeltaTime/ratio) <= Time.fixedDeltaTime*accuracy))
			{
				GetComponent<AudioSource> ().clip = son.Clip;
				GetComponent<AudioSource> ().Play ();
				//animations[son.Animation].PlayAnimation(son.Animation);
			}
		}
	}

	public void adjustSound(Sound son, float marge) 
	{
		if (sounds.Count != 0) {
			for (int i =0; i<sounds.Count(); i++) {
				if ((Math.Abs (sounds[i].Time - loopTime)) <= marge) {
					if (sounds[i].Title == son.Title) {
						adjustment = true;
						adjustedTime = (sounds[i].Time + loopTime) / 2;
						sounds.RemoveAt (i);
					}
				} else if (adjustment == false) {
					adjustedTime = loopTime;
				}
			}
		} else {
			adjustedTime = loopTime;
		}
	}

	public void trierSon() {
			var result = from so in sounds
				orderby so.Time ascending
					select so;
	}

	public float Ratio {
		get {
			return ratio;
		}
		set {
			ratio = value;
		}
	}

	public float LoopTime {
		get {
			return loopTime;
		}
		set {
			loopTime = value;
		}
	}

	public int ClipNumber {
		get {
			return clipNumber;
		}
		set {
			clipNumber = value;
		}
	}	

	public Animation Animation {
		get {
			return animation;
		}
		set {
			animation = value;
		}
	}

	public int LoopDuration {
		get {
			return loopDuration;
		}
		set {
			loopDuration = value;
		}
	}
}
	