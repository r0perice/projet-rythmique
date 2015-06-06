using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Cette classe permet d'instancier un métronome.
/// </summary>

public class Metronome : MonoBehaviour {

	private bool metronomeSpheres = false; //!< Pour indiquer si toutes les sphères associées au métronome ont été dessinnes.
	private int metronomeSpheresCount = 0; //!< Nombre de sphères associées au métr
	
	public Music music; //!< Objet Music qui permet d'ajouter une boucle à la liste de boucle ainsi que des sons à cette liste.

	/// <summary>
	/// Initialisation du métronome.
	/// </summary>
	public void Initialize() {
		music = GetComponent<Music> ();
	}

	/// <summary>
	/// Ajout des sons des sons du métronome.
	/// </summary>
	/// <param name="dureeMetronome"> Durée du métronome (en secondes).</param>
	public void addMetronome (int dureeMetronome) {

		music.anim.Size = dureeMetronome; // La taille du cylindre.
		music.anim.drawCylinder(0,"Metronome"); // Dessin du cylindre représentant le métronome au centre de la scène.
		GameObject go = (GameObject)music.anim.Cylinders[0]; // On récupère le premier cylindre.
		go.AddComponent<Loop>(); // On ajoute le Script Loop sur le GameObject.
		go.AddComponent<Sound>(); // On ajoute le Script Sound sur le GameObject.
		go.AddComponent<AudioSource>(); // On ajoute le Script AudioSource sur le GameObject.
		
		Loop cc = (Loop) go.GetComponent<Loop>(); // On crée une boucle.
		cc.Initialize (); // Initialisation de la boucle.
		cc.LoopDuration = dureeMetronome; // La durée de la boucle est égale à la durée du métronome.
		
		music.loops.Add(cc); // On ajoute la loop à la liste de boucle de Music.

		music.LoopNumber++; // Incrémentation du nombre de boucle.
		music.LoopSelected = music.LoopNumber; // Focus sur la dernière boucle créée.

		// Ajout des sons à la boucle du métronome.
		music.loops[0].AddSound(3,0.0f);
		music.loops[0].AddSound (3,1.0f);
		music.loops[0].AddSound (3,2.0f);
		music.loops[0].AddSound (3,3.0f);
		
	}

	/// <summary>
	/// Ajout des sphères représentants les sons du métronome.
	/// </summary>
	public void addMetronomeSpheres() {

		//Les boucles sont ajoutées lorsque le son doit être joué.
		if (Math.Abs (music.loops [0].sounds [0].Time - music.loops [0].LoopTime + 
		              Time.fixedDeltaTime / music.loops [0].Ratio) <= Time.fixedDeltaTime * music.loops [0].Accuracy) { 
			music.anim.Size = music.loops[0].LoopTime;
			music.anim.drawSphere ("red", 0);
			metronomeSpheresCount++;
		}
		
		if (Math.Abs (music.loops [0].sounds [1].Time - music.loops [0].LoopTime + 
		              Time.fixedDeltaTime / music.loops [0].Ratio) <= Time.fixedDeltaTime * music.loops [0].Accuracy) {
			music.anim.Size = music.loops[0].LoopTime;
			music.anim.drawSphere ("red", 0);
			metronomeSpheresCount++;
		}
		
		if (Math.Abs (music.loops [0].sounds [2].Time - music.loops [0].LoopTime + 
		              Time.fixedDeltaTime / music.loops [0].Ratio) <= Time.fixedDeltaTime * music.loops [0].Accuracy) {
			music.anim.Size = music.loops[0].LoopTime;
			music.anim.drawSphere ("red", 0);
			metronomeSpheresCount++;
		}
		
		if (Math.Abs (music.loops [0].sounds [3].Time - music.loops [0].LoopTime + 
		              Time.fixedDeltaTime / music.loops [0].Ratio) <= Time.fixedDeltaTime * music.loops [0].Accuracy) {

			music.anim.Size = music.loops[0].LoopTime;
			music.anim.drawSphere ("red", 0);
			metronomeSpheresCount++;
		}
		
		if (metronomeSpheresCount >= music.loops [0].sounds.Count) { // Si le nombre de sphères est égal au nombre de son dans la boucle du métronome alors on arrête d'en dessiner.
			metronomeSpheres = false;
		}
	}

	public bool MetronomeSpheres {
		get {
			return metronomeSpheres;
		}
		set {
			metronomeSpheres = value;
		}
	}
}