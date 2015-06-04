using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Un son est associé à un AudioClip ce qui permet de jouer un son à l'appui de touches.
/// </summary>

public class Sound : MonoBehaviour {

	public AudioClip[] clips; //!< Liste des AudioClips de possède le son.
	private AudioClip clip; //!< AudioClip utile pour jouer un son avec la méthode <a href="http://docs.unity3d.com/ScriptReference/AudioSource.Play.html">Play</a>.
	private float time; //!<  Durée du son en secondes. 
	private string title; //!< Nom du son.

	/// <summary>
	/// La classe  <see cref="Sound"/> permet d'instancier un son qui sera ajouté à une boucle.
	/// </summary>
	/// <param name="newClip"><a href="http://docs.unity3d.com/ScriptReference/AudioClip.html">AudioClip</a> associé au son.</param>
	/// <param name="newTime">Position du son dans le temps.</param>
	/// <param name="newTitle">Titre du son.</param>

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