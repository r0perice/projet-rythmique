using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Un son est associé à un AudioClip ce qui permet de jouer un son à l'appui de touches.
/// </summary>
/// 
public class Sound : MonoBehaviour {
	
	private AudioClip clp; //!< AudioClip utile pour jouer un son avec la méthode <a href="http://docs.unity3d.com/ScriptReference/AudioSource.Play.html">Play</a>.
	private float time; //!<  Durée du son en secondes. 
	private string title; //!< Nom du son.

	public float Time {
		get {
			return time;
		}
		set {
			time = value;
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

	public AudioClip Clp {
		get {
			return clp;
		}
		set {
			clp = value;
		}
	}
}

