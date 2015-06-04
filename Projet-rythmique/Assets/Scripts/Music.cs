using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Classe principale de l'application. C'est elle qui possède un FixedUpdate et qui met à jour tous les autres composants.
/// C'est également dans cette classe que sont récuperés les events claviers.
/// </summary>

public class Music : MonoBehaviour {
	
	public List<Loop> loops = new List<Loop>(); //!< Liste des différentes boucles contenants des sons.

	public Metronome metro; //!< Le métronome.
	public AudioSource audio; //!< Objet <a href="http://docs.unity3d.com/ScriptReference/AudioSource.html">AudioSource</a> qui permet de jouer un son.
	public Animation anim; //!< Permet d'intéragir avec la partie graphique de l'application (ajout de sphères/cylindre, animation des objets ...).

	private int durationTime; //!< Durée de la boucle qui va être créée.
	private bool running = true; //!< L'application est en cours d'exécution.
	private int loopNumber = 0; //!< Numéro de la boucle qui va être créée.
	private int loopSelected = 0; //!< Boucle selectionnée (en jaune).

	public float keyboardDelay = 0.5f; //!< Délai entre l'appui sur une touche et l'ajout d'un son dans une boucle.
	
	public float positionX; //!< Position de la caméra dans la scène en X.
	public float positionY; //!< Position de la caméra dans la scène en Y.
	public float positionZ; //!< Position de la caméra dans la scène en Z.

	private float fov = 2.0f; //!< <a href="http://docs.unity3d.com/ScriptReference/Camera-fieldOfView.html">Champ Visuel</a>.

	/// <summary>
	/// Démarrage du Script (voir <a href="http://docs.unity3d.com/ScriptReference/MonoBehaviour.Start.html">Start</a>).
	/// </summary>
	void Start () {
		Debug.Log ("Running...");
		if (!running)
			return;

		anim = GetComponent<Animation> (); // Instatiation de notre objet Animation.
		anim.Initialize (); // Initialisatin de l'animation.
		anim.drawLine (positionX,positionY,positionZ); // Création de la barre d'action.
		metro = GetComponent<Metronome> (); //Instantiation du métronome.
		metro.Initialize (); // Initialisation de métronome.

		// Initialisation camera
		Camera.main.transform.Translate(new Vector3(positionX-fov,positionY,positionZ));
		Camera.main.transform.Rotate(new Vector3(0f,90f,270f));

		metro.MetronomeSpheres = true; // On indique que l'on veut dessiner les sphères représentants les sons du métronome.
		metro.addMetronome(4); // Ajout d'un métronome.
	}

	/// <summary>
	/// Fonction appelée à chaque frame. Permet de lire les inputs clavier ainsi que de joeur les sons présents dans les boucles.
	/// </summary>
	void FixedUpdate()
	{
		if (metro.MetronomeSpheres == true) {
			metro.addMetronomeSpheres (); // Ajout des sphères du métronome.
		} 

		else {
			getInput (); // Quand le métronome est fini d'être crée alors on peut lire les inputs clavier pour créer de nouvelles boucle et y ajouter des sons.
		}

		for (int i=0; i<loops.Count; i++) {
			loops[i].updateLoopTime(Time.time); // Donne le temps courant à chaque boucle.
			loops[i].playSound (); // Joue les sons des boucles.
			anim.animateCylinder(0.02f * 360 / loops[i].LoopDuration,i); // Anime les cylindres représentants les boucles.
		}
	}

	/// <summary>
	/// Lit les inputs clavier.
	/// </summary>
	void getInput()
	{
		if (Input.GetKeyDown ("m")) // Mute le métronome.
		{
			audio = metro.GetComponent<AudioSource>();
			if (audio.mute)
				audio.mute = false;
			else
				audio.mute = true;
		}


		if (Input.GetKeyDown ("x")) // Effacer une boucle.
		{
			Destroy(loops[loopSelected].gameObject);
			loops.Remove(loops[loopSelected]);
		}

		if (Input.GetKeyDown ("t")) // Augmente la durée de la boucle que l'on veut créer de une seconde.
		{
			durationTime++;
			Debug.Log ("durationTime:"+durationTime);
		}

		if (Input.GetKeyDown ("g")) { // Diminue la durée de la boucle que l'on veut créer de une seconde.
			if (durationTime > 0) {
				durationTime--;
				Debug.Log ("durationTime:"+durationTime);
			}
		}

		if (Input.GetKeyDown ("q")) // Ajout du son numéro 0 dans la boucle sélectionnée.
		{
			loops[loopSelected].AddSound(0,-1,keyboardDelay);
			anim.drawSphere("black",loopSelected,positionX,positionY,positionZ); // Ajout d'une sphère représentant le son.
		}

		if (Input.GetKeyDown ("s")) // Ajout du son numéro 1 dans la boucle sélectionnée.
		{
			loops[loopSelected].AddSound(1,-1,keyboardDelay);
			anim.drawSphere("blue",loopSelected,positionX,positionY,positionZ); // Ajout d'une sphère représentant le son.
		}
		
		if (Input.GetKeyDown ("d")) // Ajout du son numéro 2 dans la boucle sélectionnée.
		{
			loops[loopSelected].AddSound(2,-1,keyboardDelay);
			anim.drawSphere("green",loopSelected,positionX,positionY,positionZ); // Ajout d'une sphère représentant le son.
		}

		if (Input.GetKeyDown ("a")) // Ajout d'une boucle
		{
			if(durationTime <= 0)
			{
				Debug.Log ("IL FAUT DEFINIR UNE DUREE DE BOUCLE !!");
			}
			else
			{

				anim.Size = durationTime; // La taille du cylindre.
				anim.drawCylinder(loopNumber,positionX,positionY,positionZ); // Dessin du cylindre représentant la boucle.
				GameObject go = (GameObject)anim.Cylinders[loopNumber]; // On récupère le dernier cylindre.
				go.AddComponent<Loop>(); // On ajoute le Script Loop sur le GameObject.
				go.AddComponent<AudioSource>(); // On ajoute le Script Sound sur le GameObject.

				Loop cc = (Loop) go.GetComponent<Loop>(); // On crée une boucle.
				cc.Initialize (); // Initialisation de la boucle.
				cc.LoopDuration = durationTime; // La durée de la boucle est égale à la durée définie par l'appui des touches t/g.


				anim.makeWhite(); // Met en blanc toutes les boucles.
				anim.makeYellow(anim.Cylinders[loopNumber]); // Mets en jaune la boucle qui viens d'etre créée.
				loopSelected = loopNumber;
				loopNumber++;// Incrémente le nombre de boucle.
				durationTime = 0; // Remise à zéro de la durée de la prochaine boucle créée.
				loops.Add(cc); // Ajout de la boucle à la liste de boucles.
			}
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) // Sélection de la boucle de gauche.
		{
			if (loopSelected>1) {
				loopSelected--;
				anim.makeWhite();
				anim.makeYellow(anim.Cylinders[loopSelected]);
				anim.Size = loops[loopSelected].LoopDuration; // Remet les sphères au bon niveau.
			}
		}

		if (Input.GetKeyDown (KeyCode.RightArrow)) // Sélection de la boucle de droite.
		{
			if (loopSelected < loops.Count-1) {
				loopSelected++;
				anim.makeWhite();
				anim.makeYellow(anim.Cylinders[loopSelected]);
				anim.Size = loops[loopSelected].LoopDuration; // Remet les sphères au bon niveau.
			}
		}
	}

	public int LoopNumber {
		get {
			return loopNumber;
		}
		set {
			loopNumber = value;
		}
	}

	public int LoopSelected {
		get {
			return loopSelected;
		}
		set {
			loopSelected = value;
		}
	}
}