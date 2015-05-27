using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Loop : MonoBehaviour
{
	private Sound sound;
	public Animation anim;

	private float adjustedTime = 0.0f;
	private bool adjustment = false;
	public float loopTime = 0.0f;
	private float ratio;
	private int clipNumber;
	private int loopDuration;
	private double accuracy;
	
	public List<Sound> sounds = new List<Sound>();
	
	public void Initialize()
	{
		ratio = Time.fixedDeltaTime / 0.02f;
		sound = GetComponent<Sound> ();

	}
	
	public void AddSound(float lt=-1)
	{
		GameObject go = GameObject.Find("Main Camera");
		Sound other = (Sound) go.GetComponent(typeof(Sound));

		if (lt == -1) {
			Sound son = new Sound (other.Clips [clipNumber], loopTime, other.Clips [clipNumber].name);
			adjustSound (son, 0.025f);
		
		} else {
			Sound son = new Sound (other.Clips [clipNumber], lt, other.Clips [clipNumber].name);
			adjustedTime=lt;
		}


		Sound _son = new Sound (other.Clips[clipNumber], adjustedTime, other.Clips[clipNumber].name);
		sounds.Add (_son);
		other = null;
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

	public void adjustSound(Sound son, float marge) 
	{
		if (sounds.Count != 0) {
			for (int i =0; i<sounds.Count(); i++) {
				if ((Math.Abs (sounds[i].Time - loopTime)) <= marge) {
					if (sounds[i].Title == son.Title) {
						adjustment = true;
						adjustedTime = (sounds[i].Time + loopTime) / 2f;
						// PROBLEME : si sound trop proche de la durée de la boucle
						sounds.RemoveAt(i);
						Destroy(anim.Spheres[i]);
						anim.Spheres.RemoveAt(i);
					}
				} else if (adjustment == false) {
					adjustedTime = loopTime;
				}
			}
		} else {
			adjustedTime = loopTime;
		}
	}

	// Centre la camera
	/*public void cameraMove(){ 
		Camera.main.transform.Translate(new Vector3((size/10.0f+cylinderGap)/2.0f,0f,0f));
	}*/

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

	public int LoopDuration {
		get {
			return loopDuration;
		}
		set {
			loopDuration = value;
		}
	}

	public double Accuracy {
		get {
			return accuracy;
		}
		set {
			accuracy = value;
		}
	}
}
	