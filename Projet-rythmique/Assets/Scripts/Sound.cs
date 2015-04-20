using UnityEngine;
using System.Collections;
using System;


public class Sound : MonoBehaviour {

	public AudioClip[] clips; //Liste des audio clips
	private AudioClip clip;
	private float time; //Duree du son
	private string title; //Nom du son
	private int animation; //Animation

	public Sound(AudioClip newClip, float newTime, string newTitle, int newAnim) 
	{
		clip = newClip;
		time = newTime;
		title = newTitle;
		animation = newAnim;
		title = newClip.name;

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

	public int Animation {
		get {
			return animation;
		}
		set {
			animation = value;
		}
	}
}