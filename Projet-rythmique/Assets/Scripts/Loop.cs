using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class Loop : MonoBehaviour
{
	private Sound sound;
	private GameObject emptyObject;

	private float adjustedTime = 0.0f;
	private bool adjustment = false;
	private float loopTime = 0.0f;
	private float ratio;
	private int clipNumber;
	private int loopDuration;
	public double accuracy;

	public GameObject[] cubes;
	public List<Sound> sounds = new List<Sound>();
	public List<Animation> animations = new List<Animation>();
	public List<GameObject> cylinders = new List<GameObject>();
	private List<GameObject> spheres = new List<GameObject>();

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
	

	public void Start()
	{
		Debug.Log ("START LOOP");
		drawLine ();


		ratio = Time.fixedDeltaTime / 0.02f;
		sound = GetComponent<Sound> ();

		// Initialisation camera
		fov = size + fovGap;
		sizeY = size * sizeRatio;
		Camera.main.transform.Translate(new Vector3(positionX-fov,positionY,positionZ));
		Camera.main.transform.Rotate(new Vector3(0f,90f,270f));

		// GameObject vide
		emptyObject = new GameObject();
	}

	
	public void AddSound()
	{
		Sound son = new Sound (sound.clips[clipNumber], loopTime, sound.clips [clipNumber].name, clipNumber);
		adjustSound(son, 0.025f);
		Sound _son = new Sound (sound.clips[clipNumber], adjustedTime, sound.clips[clipNumber].name, clipNumber);
		sounds.Add (_son);
		son = null;
		adjustment = false;
	}

	public void playSound()
	{
		foreach (Sound son in sounds) {
			if ((Math.Abs (son.Time - loopTime + Time.fixedDeltaTime/ratio) <= Time.fixedDeltaTime*accuracy))
			{
				Debug.Log ("name:"+son.Title+" time:"+son.Time);
				GetComponent<AudioSource> ().clip = son.Clip;
				GetComponent<AudioSource> ().Play ();
			}
		}
	}
	

	public void adjustSound(Sound son, float marge) 
	{
		if (sounds.Count != 0) {
			for (int i =0; i<sounds.Count(); i++) {
				if ((Math.Abs (sounds[i].Time - loopTime)) <= marge) {
					if (sounds[i].Title == son.Title) {
						adjustment = true;
						adjustedTime = (sounds[i].Time + loopTime) / 2f;
						// PROBLEME : si sound trop proche de la durée de la boucle
						sounds.RemoveAt(i);
						Destroy(spheres[i]);
						spheres.RemoveAt(i);

					}
				} else if (adjustment == false) {
					adjustedTime = loopTime;
				}
			}
		} else {
			adjustedTime = loopTime;
		}
	}


	public void drawCylinder(int number){
		GameObject cyl = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		cylinders.Add (cyl);
		cyl.transform.position = new Vector3 (positionX, positionY-((size/10.0f+cylinderGap)*number), positionZ);
		cyl.transform.localScale = new Vector3(size, sizeY, size);
		
	}

	public void drawLine(){
		GameObject lin = GameObject.CreatePrimitive (PrimitiveType.Cylinder);
		lin.transform.position = new Vector3 (positionX-size/2, positionY, positionZ);
		lin.transform.localScale = new Vector3 (0.1f, 10f, 0.1f);
		lin.GetComponent<Renderer>().material.color = Color.red;
	}
	
	public void animateCylinder(float degree){
		for (int i=0; i<cylinders.Count; i++) {
			cylinders [i].transform.Rotate(Vector3.up, degree);
		}
	}
	
	public void drawSphere(Color color, int number){
		GameObject sph = GameObject.CreatePrimitive (PrimitiveType.Sphere);
		spheres.Add (sph);
		// Utilisation gameObjet intermédiaire afin d'éviter le scaling de cylinder
		emptyObject.transform.parent = cylinders [0].transform;
		sph.transform.parent = emptyObject.transform;
		sph.transform.position = new Vector3 (positionX-(size/2.0f), positionY-((size/10.0f+cylinderGap)*number), positionZ);
		sph.transform.localScale = new Vector3(sizeY,sizeY,sizeY);
		sph.GetComponent<Renderer>().material.color = color;
	}
	
	public void makeWhite(){
		foreach (GameObject cylinder in cylinders){
			cylinder.GetComponent<Renderer>().material.color = Color.white;
		}
	}
	
	// Centre la camera
	public void cameraMove(){ 
		Camera.main.transform.Translate(new Vector3((size/10.0f+cylinderGap)/2.0f,0f,0f));
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

	public int ClipNumber {
		get {
			return clipNumber;
		}
		set {
			clipNumber = value;
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

	public List<GameObject> Cylinders {
		get {
			return cylinders;
		}
	}
}
	