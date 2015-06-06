using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Cette classe implémente la partie graphique de l'application. 
/// Elle permet d'instancier des cylindres et des sphères, de les mettre en mouvement, etc ...
/// </summary>

public class Animation : MonoBehaviour {

	private GameObject container; //!< Utiliser pour permet aux cylindres d'être le parent des sphères.
	
	private Dictionary<string, Color> MyColors = new Dictionary<string, Color> (); //!< Dictionnaire de couleur.

	public List<GameObject> cylinders = new List<GameObject>(); //!< Liste des cylindres.

	private float size; //!< Taille des cylindres et sphères.
	private float sizeY = 0.1f; //!< sizeY = size * sizeRatio
	private float cylinderGap = 0.3f; //!< Espace entre les cylindres
	private float gap = 0.2f; //!< Espace la barre d'action et les cylindres. 

	/// <summary>
	/// Initialisation de l'animation.
	/// </summary>
	public void Initialize(){
		AddColor (); 
	}

	/// <summary>
	/// Construction du dictionnaire de couleurs.
	/// </summary>
	public void AddColor(){
		MyColors.Add ("black", Color.black);
		MyColors.Add ("blue", Color.blue);
		MyColors.Add ("red", Color.red);
		MyColors.Add ("green", Color.green);
		MyColors.Add ("white", Color.white);
		MyColors.Add ("yellow", Color.yellow);
	}

	/// <summary>
	/// Dessin de la ligne rouge représentant le moment où le son va être joué.
	/// </summary>
	/// <param name="posX"> Position en X de la barre d'action</param>
	/// <param name="posY"> Position en Y de la barre d'action</param>
	/// <param name="posZ"> Position en Z de la barre d'action</param>
	public void drawLine(float posX, float posY, float posZ){
		GameObject lin;
		lin = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		lin.name = "RedLine"; // Nom de la barre d'action.
		lin.transform.position = new Vector3 (posX, posY, posZ); // Positionnement de la barre d'action.
		lin.transform.localScale = new Vector3 (0.05f, 10f, 0.05f); // Ajustement de la taille de la barre d'action.
		lin.GetComponent<Renderer>().material.color = MyColors["red"]; // Définition de la couleur de la barre d'action.
	}

	/// <summary>
	/// Dessin des sphères représentants les sons.
	/// </summary>
	/// <param name="colorName"> Couleur de la sphère.</param>
	/// <param name="number"> Numéro de la sphère.</param>
	/// <param name="posX"> Position en X de la sphère.</param>
	/// <param name="posY"> Position en Y de la sphère.</param>
	/// <param name="posZ"> Position en Z de la sphère.</param>

	public void drawSphere(string colorName, int number, float posX=0.0f, float posY=0.0f, float posZ=0.0f){
		GameObject sph;
		GameObject go;
		sph = GameObject.CreatePrimitive (PrimitiveType.Sphere); // Création d'un GameObkect en forme de sphère;
		sph.name = "Sphere_" + number; // Numéro de la sphère.
		container.transform.parent = cylinders [number].transform; // Utilisation gameObjet intermédiaire afin d'éviter le scaling de cylinder.
		sph.transform.parent = container.transform;
		sph.transform.position = new Vector3 ((posX+((1.0f+size/10.0f)/2.0f)+gap)-((1.0f+size/10.0f)/2.0f), posY-(cylinderGap*number), posZ); // Positionnement de la sphère.
		sph.transform.localScale = new Vector3(sizeY,sizeY,sizeY); // Ajutement de la taille.
		sph.GetComponent<Renderer>().material.color = MyColors[colorName]; // Application de la couleur.
		go = GameObject.Find(sph.transform.parent.parent.name); // trouve le gameobject grand parent (avec le nom)
		Loop other = (Loop) go.GetComponent(typeof(Loop));
		Debug.Log ("go : " + go);
		other.addSpheres (sph.name);
	}

	/// <summary>
	/// Permet de mettre tous les cylindres de liste de cylindres en blanc.
	/// </summary>
	public void makeWhite(){
		foreach (GameObject cylinder in cylinders){
			cylinder.GetComponent<Renderer>().material.color = MyColors["white"];
		}
	}

	/// <summary>
	/// Permet de mettre un cylindre en jaune.
	/// </summary>
	/// <param name="cylinder"> Cylindre à mettre en jaune.</param>
	public void makeYellow(GameObject cylinder){
		cylinder.GetComponent<Renderer>().material.color = MyColors["yellow"];
	}

	/// <summary>
	/// Dessin d'un cylindre.
	/// </summary>
	/// <param name="number"> Numéro du cylindre.</param>
	/// <param name="posX"> Position en X du cylindre.</param>
	/// <param name="posY"> Position en Y du cylindre.</param>
	/// <param name="posZ"> Position en Z du cylindre.</param>
	public void drawCylinder(int number,string name="Cylinder_", float posX=0.0f, float posY=0.0f, float posZ=0.0f){
		GameObject cyl;
		
		container = new GameObject();
		cyl = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		
		if (name != "Cylinder_") {
			cyl.name = name;
			container.name = "Container_"+ name;
		} 
		else {
			cyl.name = name+number;
			container.name = "Container_" + number;
		}
		
		cyl.transform.position = new Vector3 (posX+((1.0f+size/10.0f)/2.0f)+gap, posY-(cylinderGap*number), posZ); // EXCENTRE
		cyl.transform.localScale = new Vector3(1.0f+size/10.0f, sizeY, 1.0f+size/10.0f);
		
		addCylinders(cyl.name);
		
		cameraMove (posY-cylinderGap);
	}


	/// <summary>
	/// Animation des différents cylindres.
	/// </summary>
	/// <param name="degree"> Rotation du cylindre.</param>
	/// <param name="i"> Numéro du cylindre à faire tourner</param>
	public void animateCylinder(float degree, int i){
		cylinders[i].transform.Rotate(Vector3.up, degree);
	}
	
	/// <summary>
	/// Centrage de la caméra.
	/// </summary>
	/// <param name="dist"> Somme de la largeur de tout les cylindres divisé par 2.</param>
	public void cameraMove(float dist){ 
		if (cylinders.Count != 1) {
			Camera.main.transform.Translate(new Vector3(-dist/2.0f,0f));
		}
	}

	public void addCylinders(string nameToAdd){
		foreach (GameObject go in GameObject.FindObjectsOfType(typeof(GameObject))) {
			if (go.name == nameToAdd)
				cylinders.Add (go);
		}
	}

	public float CylinderGap {
		get {
			return cylinderGap;
		}
	}

	public List<GameObject> Cylinders {
		get {
			return cylinders;
		}
	}
	

	public float Size {
		get {
			return size;
		}
		set {
			size = value;
		}
	}

	public float SizeY {
		get {
			return sizeY;
		}
	}
}