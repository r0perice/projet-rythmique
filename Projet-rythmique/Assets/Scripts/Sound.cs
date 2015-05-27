using UnityEngine;
using System.Collections;
using System;


public class Sound : MonoBehaviour {

	public AudioClip[] clips; //Liste des audio clips
	private AudioClip clip;
	private float time; //Duree du son
	private string title; //Nom du son

	public Sound(AudioClip newClip, float newTime, string newTitle) 
	{
		clip = newClip;
		time = newTime;
		title = newTitle;

	}

	public float Time {
		get {
			return time;
		}
		set {
			time = value;
		}
	}

	public AudioClip Clip {
		get {
			return clip;
		}
		set {
			clip = value;
		}
	}

	public string Title {
		get {
			return title;
		}
		set {
			title = value;
		}
	}

	public AudioClip[] Clips {
		get {
			return clips;
		}
	}
}