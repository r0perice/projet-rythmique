using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Animation : MonoBehaviour {

	private GameObject emptyObject;

	// Dictionnaire de couleur
	public Dictionary<string, Color> MyColors = new Dictionary<string, Color>();
	

	// Listes
	public List<GameObject> cylinders = new List<GameObject>();
	private List<GameObject> spheres = new List<GameObject>();

	// Taille du cylindre
	private float size;
	//private float sizeRatio = 0.06f; // rapport rayon largeur
	private float sizeY = 0.1f; // sizeY = size * sizeRatio
	private float cylinderGap = 0.3f;// Espace entre les cylindres

	// GameObject vide
	public void Initialize(){
		emptyObject = new GameObject ();
		emptyObject.name = "EmptyObject";
		AddColor ();
	}

	public void AddColor(){
		MyColors.Add ("black", Color.black);
		MyColors.Add ("blue", Color.blue);
		MyColors.Add ("red", Color.red);
		MyColors.Add ("green", Color.green);
		MyColors.Add ("white", Color.white);
		MyColors.Add ("yellow", Color.yellow);
	}

	public void drawLine(float posX, float posY, float posZ){
		GameObject lin = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		lin.name = "RedLine";
		lin.transform.position = new Vector3 (posX-size/2, posY, posZ);
		lin.transform.localScale = new Vector3 (0.1f, 10f, 0.1f);
		lin.GetComponent<Renderer>().material.color = MyColors["red"];
	}

	public void drawSphere(string colorName, int number, float posX, float posY, float posZ){
		GameObject sph = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		// Utilisation gameObjet intermédiaire afin d'éviter le scaling de cylinder
		emptyObject.transform.parent = cylinders [0].transform;
		sph.transform.parent = emptyObject.transform;
		sph.transform.position = new Vector3 (posX-(size/2.0f), posY-(cylinderGap*number), posZ);
		sph.transform.localScale = new Vector3(sizeY,sizeY,sizeY);
		sph.GetComponent<Renderer>().material.color = MyColors[colorName];
	}
	
	public void makeWhite(){
		foreach (GameObject cylinder in cylinders){
			cylinder.GetComponent<Renderer>().material.color = MyColors["white"];
		}
	}

	public void makeYellow(GameObject cylinder){
		cylinder.GetComponent<Renderer>().material.color = MyColors["yellow"];
	}

	public void drawCylinder(int number, float posX, float posY, float posZ){
		//sizeY = size * sizeRatio;
		GameObject cyl = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		cyl.transform.position = new Vector3 (posX, posY-(cylinderGap*number), posZ);
		cyl.transform.localScale = new Vector3(size, sizeY, size);
		cylinders.Add (cyl);
	}

	public void animateCylinder(float degree, int i){
		cylinders[i].transform.Rotate(Vector3.up, degree);
	}

	// ACCESSEURS
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

	public List<GameObject> Spheres {
		get {
			return spheres;
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


}