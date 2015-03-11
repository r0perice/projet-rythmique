using UnityEngine;
using System;
using System.Collections.Generic;

public class Sound : MonoBehaviour {
	
	/*Liste des audio clips*/
	public List<AudioClip> audioClips = new List<AudioClip>();
	public AudioClip audioClip1;
	public AudioClip audioClip2;
	public AudioClip audioClip3;

	/*position du son dans la timeline*/
	public float position;
	/*pondération du son*/
	public int weight;

	public Sound() {
				
		audioClips.Add (audioClip1);
		audioClips.Add (audioClip2);
		audioClips.Add (audioClip3);
	}

	public AudioClip getAudioClip(int numeroSon) {
		return audioClips[numeroSon];
	}

}