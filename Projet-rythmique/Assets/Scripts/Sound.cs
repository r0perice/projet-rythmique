using UnityEngine;
using System.Collections;
using System;


public class Sound : MonoBehaviour {
	
	/*Liste des audio clips*/
	public AudioClip[] clips;
	public AudioClip clip;
	public float time { get; set; }
	public int weight;
	private string title;

	public Sound(AudioClip newClip, float newTime, int newWeight, string newTitle) 
	{
		clip = newClip;
		time = newTime;
		weight = newWeight;
		title = newTitle;
	}


}