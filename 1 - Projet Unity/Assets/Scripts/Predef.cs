using UnityEngine;
using System.Collections;

/// <summary>
/// Cette classe permet de charger des boucles prédéfinies.
/// </summary>

public class Predef : MonoBehaviour {

	private Music music; //!< Instance de Music qui sert à ajouter des sons dans la boucle depuis ce script.

	void Awake () {
		music = this.gameObject.GetComponent<Music> ();
	}

	/// <summary>
	/// Ajoute des boucles prédéfinies à l'aide d'un switch case.
	/// </summary>
	/// <param name="name">Nom de la boucle à charger.</param>
	public void AddPredef(string name){
		switch(name)
		{
		case("Metronome"): // Un métronome de 60bpm avec 4 mesures.
			music.Bpm = 60.0f;
			music.Mesure = 4.0f;
			music.AddLoop ();
			music.loops [music.LoopSelected].GetComponent<Loop> ().AddSound ("metronome", 0.0f);
			music.loops [music.LoopSelected].GetComponent<Loop> ().AddSound ("metronome", 1.0f);
			music.loops [music.LoopSelected].GetComponent<Loop> ().AddSound ("metronome", 2.0f);
			music.loops [music.LoopSelected].GetComponent<Loop> ().AddSound ("metronome", 3.0f);
			break;

		default:
			break;
		}
	}

}
