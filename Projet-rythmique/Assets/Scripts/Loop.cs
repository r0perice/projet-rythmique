using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Cette classe permet de définir une boucle qui pourra contenir des sons.
/// </summary>

public class Loop : MonoBehaviour {

	public Animation anim; //!< 
	public float loopTime = 0.0f; //!< Temps courant de la boucle.
	private float ratio; //!< 
	private int clipNumber; //!< Numéro du clip.
	private int loopDuration; //!< Durée de la boucle.
	private double accuracy = 5.0f; //<! Précision (utilisée pour la lecture des sons des différentes boucles).
	
	public List<Sound> sounds = new List<Sound>(); //!< Liste des sons de la boucle.
	public List<GameObject> spheres = new List<GameObject>();


	/// <summary>
	/// Initialisation de la boucle.
	/// </summary>
	public void Initialize()
	{
		ratio = Time.fixedDeltaTime / 0.02f; // Définition du ratio permet d'avoir un loopTime en secondes.
	}

	/// <summary>
	/// Ajout d'un son à une boucle.
	/// </summary>
	/// <param name="clipNumber"> Numéro de l'AudioClip à ajouter à la boucle.</param>
	/// <param name="lt"> Paramètre à fournir pour ajouter un son à un temps voulu.</param>
	/// <param name="delay"> Décalage entre l'appui d'une touche et l'ajout d'un son.</param>
	public void AddSound(int clipNumber, float lt=-1.0f, float delay=0.0f)
	{
		GameObject go = GameObject.Find("Main Camera"); // Pour accéder au script Sound.
		Sound other = (Sound) go.GetComponent(typeof(Sound)); // Pour accéder aux AudioClips.

		float t = lt;
		if (lt <= -1.0f) {
			t = loopTime; // Pour ajouter le son au temps donné en paramètre de la fonction.
		}

		t -= (delay / ratio) % loopDuration; // Application du décalage entre l'appui sur une touche et l'ajout d'un son

		Sound son = new Sound(other.Clips [clipNumber], t, other.Clips [clipNumber].name);
		son.Time = adjustSound(son, 0.025f); // Récupération du temps ajusté ou non celon besoin.
		sounds.Add (son); // Ajout du son à la liste de sons.
	}

	/// <summary>
	/// Fonction qui parcourt la liste des sons pour les jouer.
	/// </summary>
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

	/// <summary>
	/// Ajustement du son si nécessaire. 
	/// Si à l'appui d'une touche un son se trouve déjà proche du 
	/// </summary>
	/// <returns>The sound.</returns>
	/// <param name="son">Son.</param>
	/// <param name="marge">Marge.</param>
	public float adjustSound(Sound son, float marge) 
	{
		float adjustedTime = son.Time;

		if (sounds.Count != 0) {
			for (int i =0; i<sounds.Count(); i++) {

				if ((Math.Abs (sounds [i].Time - son.Time)) <= marge) {
					if (sounds [i].Title == son.Title) {

						adjustedTime = (sounds [i].Time + son.Time) / 2f;
						sounds.RemoveAt (i);
						//Destroy (anim.Spheres [i]);
						spheres.RemoveAt (i);
					}
				}
			}
		}
		return adjustedTime;
	}

	public void addSpheres(string nameToAdd){
		foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject))) {
			if (go.name == nameToAdd)
				spheres.Add (go);	
		}
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

	public float updateLoopTime(float t)
	{
		return loopTime = (t / ratio) % loopDuration;
	}

	public List<GameObject> Spheres {
		get {
			return spheres;
		}
		set {
			spheres = value;
		}
	}
}
	