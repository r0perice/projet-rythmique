using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Music : MonoBehaviour {
	
	private GameObject emptyObject;
	private List<GameObject> cylinders = new List<GameObject>();
	private List<GameObject> spheres = new List<GameObject>();
	public List<Loop> loops = new List<Loop>();

	public Loop loop;

	public int loopDuration = 5;
	private bool running = true;
	private int loopNumber = 0; // Création de la boucle numéro...
	private int loopSelected = 0; // Selection de la boucle

	// Position du premier cylindre
	public float positionX;
	public float positionY;
	public float positionZ;

	// Taille du cylindre
	public float size;
	private float sizeRatio = 0.06f; 
	private float sizeY; // sizeY = size * sizeRatio
	private float cylinderGap = 0.2f;// Espace entre les cylindres

	// Field Of View
	private float fov;
	private float fovGap = 1.0f;// Variable pour le fov

	// Couleurs
	private Color color1 = Color.black;
	private Color color2 = Color.blue;
	private Color color3 = Color.green;

	// Use this for initialization
	void Start () {
		Debug.Log ("Running...");
		if (!running)
			return;
		// Initialisation camera
		fov = size + fovGap;
		sizeY = size * sizeRatio;
		Camera.main.transform.Translate(new Vector3(positionX-fov,positionY,positionZ));
		Camera.main.transform.Rotate(new Vector3(0f,90f,270f));

		// GameObject vide
		emptyObject = new GameObject();
	}


	void FixedUpdate()
	{
		getInput ();
		foreach (Loop lp in loops) {
			lp.LoopTime = (Time.time / lp.Ratio) % loopDuration;
			lp.playSound ();
		}
		animateCylinder (0.02f * 360 / loopDuration);

	}

	void getInput()
	{
		if (Input.GetKeyDown ("q")) 
		{
			loops[loopSelected].ClipNumber = 0;
			loops[loopSelected].AddSound();
			drawSphere(color1,loopSelected);
		}

		if (Input.GetKeyDown ("s")) 
		{
			loops[loopSelected].ClipNumber = 1;
			loops[loopSelected].AddSound();
			drawSphere(color2,loopSelected);
		}
		
		if (Input.GetKeyDown ("d")) 
		{
			loops[loopSelected].ClipNumber = 2;
			loops[loopSelected].AddSound();
			drawSphere(color3,loopSelected);
		}

		if (Input.GetKeyDown ("a")) 
		{
			loop = gameObject.GetComponent<Loop> ();
			loops.Add(loop);

			drawCylinder(loopNumber);

			if (loopNumber != 0){
				cameraMove();
			}
			// mets en jaune la boucle qui viens d'etre cree
			makeWhite();
			cylinders[loopNumber].GetComponent<Renderer>().material.color = Color.yellow;
			loopSelected = loopNumber;
			// incrément du nombre de boucle
			loopNumber++;

		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			this.makeWhite();
			if (loopSelected<loops.Count-1) {
				loopSelected++;
			}
			cylinders[loopSelected].GetComponent<Renderer>().material.color = Color.yellow;
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			makeWhite();
			if (loopSelected>0) {
				loopSelected--;
			}
			cylinders[loopSelected].GetComponent<Renderer>().material.color = Color.yellow;
		}
	}

	void drawCylinder(int number){
		GameObject cyl = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		cylinders.Add (cyl);
		cyl.transform.position = new Vector3 (positionX, positionY-((size/10.0f+cylinderGap)*number), positionZ);
		cyl.transform.localScale = new Vector3(size, sizeY, size);

	}

	void animateCylinder(float degree){
		for (int i=0; i<cylinders.Count; i++) {
			cylinders [i].transform.Rotate(Vector3.up, degree);
		}
	}

	void drawSphere(Color color, int number){
		GameObject sph = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		spheres.Add (sph);
		// Utilisation gameObjet intermédiaire afin d'éviter le scaling de cylinder
		emptyObject.transform.parent = cylinders [0].transform;
		sph.transform.parent = emptyObject.transform;
		sph.transform.position = new Vector3 (positionX-(size/2.0f), positionY-((size/10.0f+cylinderGap)*number), positionZ);
		sph.transform.localScale = new Vector3(sizeY,sizeY,sizeY);
		sph.GetComponent<Renderer>().material.color = color;
	}

	void makeWhite(){
		foreach (GameObject cylinder in cylinders){
			cylinder.GetComponent<Renderer>().material.color = Color.white;
		}
	}

	// Centre la camera
	void cameraMove(){ 
		Camera.main.transform.Translate(new Vector3((size/10.0f+cylinderGap)/2.0f,0f,0f));
	}
}

