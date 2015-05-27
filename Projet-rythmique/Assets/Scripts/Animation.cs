using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Animation : MonoBehaviour {

	private GameObject emptyObject;

	// Dictionnaire de couleur
	private Dictionary<string, Color> MyColors = new Dictionary<string, Color>();
	

	// Listes
	public List<GameObject> cylinders = new List<GameObject>();
	private List<GameObject> spheres = new List<GameObject>();

	// Taille du cylindre
	private float size;
	private float sizeY = 0.1f; // sizeY = size * sizeRatio
	private float cylinderGap = 0.3f;// Espace entre les cylindres
	private float gap = 0.2f; // Espace 

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
		lin.transform.position = new Vector3 (posX, posY, posZ);
		lin.transform.localScale = new Vector3 (0.1f, 10f, 0.1f);
		lin.GetComponent<Renderer>().material.color = MyColors["red"];
	}

	public void drawSphere(string colorName, int number, float posX, float posY, float posZ){
		GameObject sph = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		// Utilisation gameObjet intermédiaire afin d'éviter le scaling de cylinder
		Debug.Log ("parent :");
		emptyObject.transform.parent = cylinders [number].transform;
		sph.transform.parent = emptyObject.transform;
		sph.transform.position = new Vector3 ((posX+((1.0f+size/10.0f)/2.0f)+0.3f)+(size/2.0f)+(sizeY/2.0f), posY-(cylinderGap*number), posZ); // CENTRE
		//sph.transform.position = new Vector3 (posX+0.3f, posY-(cylinderGap*number), posZ); // EXCENTRE
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
		GameObject cyl = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		cyl.transform.position = new Vector3 (posX+((1.0f+size/10.0f)/2.0f)+0.3f, posY-(cylinderGap*number), posZ); // EXCENTRE
		//cyl.transform.position = new Vector3 (posX, posY-(cylinderGap*number), posZ); // CENTRE
		cyl.transform.localScale = new Vector3(1.0f+size/10.0f, sizeY, 1.0f+size/10.0f);
		cylinders.Add (cyl);
		cameraMove (posY-cylinderGap);
	}

	public void animateCylinder(float degree, int i){
		Debug.Log("i: "+i+" degree: "+degree);
		cylinders[i].transform.Rotate(Vector3.up, degree);
	}

	
	// Centre la camera
	public void cameraMove(float dist){ 
		if (cylinders.Count != 1) {
		Camera.main.transform.Translate(new Vector3(-dist/2.0f,0f));
		}
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