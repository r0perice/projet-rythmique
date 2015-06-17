using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// Cette classe permet de définir une boucle qui pourra contenir des sons.
/// </summary>

public class Loop : MonoBehaviour {
	
	private AudioClip[] clipArray; //!< Tableau contenant les audioClips.
	
	private float loopDuration; //!< Durée de la boucle.
	private float loopTime = 0.0f; //!< Temps courant de la boucle.
	private float ratio; //!< Rapport calculé pour avoir des secondes dans Unity.
	private double accuracy = 5.0f; //<!< Précision (utilisée pour la lecture des sons des différentes boucles).
	private float marge = 0.010f; //!< Marge défini pour l'ajustement des sons.
	
	public List<GameObject> spheres = new List<GameObject>(); //!< Liste des sons de la boucle.
	public List<AudioClip> clips = new List<AudioClip>(); //!< Liste des clips dans la boucle.
	
	private Material black; //!< Définition du matériau Noir
	private Material blue; //!< Définition du matériau Bleu
	private Material red; //!< Définition du matériau Rouge
	private Material green; //!< Définition du matériau Vert
	private Material neutre; //!< Définition du matériau Neutre
	
	/// <summary>
	/// Awake this instance.
	/// </summary>
	// Use this for initialization
	void Awake () {
		// Instanciation des clips présents dans le dossier Sounds.
		clipArray = Resources.LoadAll<AudioClip>("Sounds");
		foreach(AudioClip son in clipArray){
			clips.Add(son);
		}
		// Instanciation des couleurs.
		black = (Material)Resources.Load("Materials/Black");
		red = (Material)Resources.Load("Materials/Red");
		blue = (Material)Resources.Load("Materials/Blue");
		green = (Material)Resources.Load("Materials/Green");
		neutre = (Material)Resources.Load("Materials/Neutre");
		/// VERIFIER L'UTILITE DU RATIO.
		ratio = Time.fixedDeltaTime / 0.02f; // Définition du ratio permet d'avoir un loopTime en secondes.
		//ratio = 1;
	}
	
	// Update is called once per frame
	/// <summary>
	/// Fixeds the update.
	/// Utilise la fonction PlaySoud pour jouer les sons.
	/// Calcul le temps actuel de la boucle (loopTime).
	/// </summary>
	void FixedUpdate () {
		playSound ();
		loopTime = (Time.fixedTime / ratio) % loopDuration + 0.010f;
		//loopTime = (Time.time / ratio) % loopDuration + 0.010f;
		Debug.Log ("Looptime : " + loopTime);
	}
	
	/// <summary>
	/// Fonction qui parcourt la liste des sons pour les jouer.
	/// </summary>
	public void playSound()
	{
		foreach (GameObject sph in spheres) {
			if ((Math.Abs (sph.GetComponent<Sound>().Time - loopTime + Time.fixedDeltaTime/ratio) <= Time.fixedDeltaTime*accuracy))
			{
				this.GetComponent<AudioSource> ().clip = sph.GetComponent<Sound>().Clp;
				this.GetComponent<AudioSource> ().Play ();
			}
		}
	}
	/// <summary>
	/// Ajustement d'un son avec un autre son proche.
	/// </summary>
	/// <returns>Retourne le temps ajusté (moyenne des deux sons proches).</returns>
	/// <param name="sph">Utilisation du gameObject contenant le son à ajuster.</param>
	public float AdjustSound(GameObject sph){
		Sound Ssphere = sph.GetComponent<Sound>();
		float adjustedTime = Ssphere.Time;
		for(int i=0; i <= spheres.Count-1; i++){
			
			Sound Sspheres = spheres[i].GetComponent<Sound>();
			/** @bug Si on veut ajuster un son qui se trouve en fin de boucle alors l'ajustement ne va pas se faire en fin de boucle mais au milieu...
				 * 	\n Explications :
				 * 	
				 * 	La boucle fait 5s: 																		|----------|
				 * 	Un son est présent en fin de boucle : 													|---------S|
				 * 	On ajuste avec un nouveau mais le temps que l'on appui il se situe en début de boucle : |S--------S|
				 * 	Le nouveau son ajusté va se trouver en milieu de boucle : 								|----S-----|
				 * 
				 * 
				 * @todo Si le son à ajuster présent dans la liste est trop près de la fin de la boucle alors on ajuste en fin de boucle.
				 * 
				 * Idée de code :
				 * 
				 * if ((Sspheres.Time+marge>loopDuration || Ssphere.Time+marge>loopDuration) && (Sspheres.Time<loopDuration/2.0f || Ssphere.Time<loopDuration/2.0f) ){
				 * adjustedTime = ((Sspheres.Time+Ssphere.Time)-loopDuration)/2.0f;
				 * Destroy(spheres[i]);
				 **/
			
			if ((Math.Abs(Sspheres.Time - Ssphere.Time)<= marge) && (Sspheres.Title == Ssphere.Title)){
				adjustedTime = (Sspheres.Time + Ssphere.Time)/2.0f;
				Destroy(spheres[i]);
				spheres.RemoveAt(i);
			}
		}
		return adjustedTime;
	}
	
	/// <summary>
	/// Ajout d'un son à une boucle.
	/// </summary>
	/// <param name="title"> Titre de l'AudioClip à ajouter à la boucle.</param>
	/// <param name="time"> Paramètre à fournir pour ajouter un son à un temps voulu.</param>
	/// <param name="delay"> Décalage entre l'appui d'une touche et l'ajout d'un son.</param>
	public void AddSound(string title, float time=-1, float delay=0.0f){
		
		GameObject sph = (GameObject)Instantiate (Resources.Load ("Sphere")); // Chargement du prefab Sphere.
		AddColor (title,sph);
		
		sph.transform.parent = this.gameObject.transform.GetChild (0); // Désigne le parent de la Sphere.
		// Positionnement de la Sphere.
		float x = sph.transform.parent.parent.position.x;
		float y = sph.transform.parent.parent.position.y;
		float z = sph.transform.parent.parent.position.z-(sph.transform.parent.parent.localScale.z)/2.0f ;
		sph.transform.position = new Vector3 (x, y, z);
		
		if (time == -1) {
			// Par défaut
			sph.GetComponent<Sound>().Time = loopTime;
			
			//sph.GetComponent<Sound>().Time = loopTime;
			sph.GetComponent<Sound>().Title = title;
			sph.GetComponent<Sound>().Clp = clips.Find (a => a.name == title);
		}
		else {
			// Si un temps a été défini.
			sph.GetComponent<Sound>().Time = time;
			sph.GetComponent<Sound>().Title = title;
			sph.GetComponent<Sound>().Clp = clips.Find (a => a.name == title);
			float angle = (time * 360.0f) / loopDuration; // Calcul de la rotation à effectuer pour placer la Sphere (en fonction du temps).
			sph.transform.RotateAround(sph.transform.parent.parent.position,Vector3.left,angle);
		}
		
		sph.GetComponent<Sound>().Time -= 0.20f;
		if(sph.GetComponent<Sound>().Time < 0.0f){
			sph.GetComponent<Sound>().Time += loopDuration;
		}
		
		if (spheres.Count != 0) {
			float t = AdjustSound(sph);
			if (t != sph.GetComponent<Sound>().Time){
				float angle = ((t-sph.GetComponent<Sound>().Time) * 360.0f) / loopDuration;
				sph.GetComponent<Sound>().Time = t;
				sph.transform.RotateAround(sph.transform.parent.parent.position,Vector3.left,-angle);
			}
		}
		spheres.Add (sph); // Ajout de la Sphere à la liste.
	}
	
	/// <summary>
	/// Ajoute les couleurs./// </summary>
	/// <param name="title">Titre du son.</param>
	/// <param name="go">GameObject associé au son, qui recevra la couleur.</param>
	public void AddColor(string title, GameObject go){
		switch (title) 
		{
		case("bass"):
			go.GetComponent<Renderer> ().material = black;
			break;
		case("bip"):
			go.GetComponent<Renderer> ().material = green;
			break;
		case("clave"):
			go.GetComponent<Renderer> ().material = blue;
			break;
		case("metronome"):
			go.GetComponent<Renderer> ().material = red;
			break;
		default:
			go.GetComponent<Renderer> ().material = neutre;
			break;
		}
	}
	
	public float LoopDuration {
		get {
			return loopDuration;
		}
		set {
			loopDuration = value;
		}
	}
}
