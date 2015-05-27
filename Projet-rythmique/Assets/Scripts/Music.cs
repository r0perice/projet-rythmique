using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Music : MonoBehaviour {
	
	public List<Loop> loops = new List<Loop>();
	
	public Loop loop;
	public Animation anim;
	public GameObject go;

	private bool metronomeSpheres = false;
	private int metronomeSpheresCount = 0;
	private int durationTime;
	private bool running = true;
	private int loopNumber = 0; // Création de la boucle numéro...
	private int loopSelected = 0; // Selection de la boucle
	
	// Camera position
	public float positionX;
	public float positionY;
	public float positionZ;

	// Field Of View
	private float fov;
	private float fovGap = 1.0f;// Variable pour le fov

	// Use this for initialization
	void Start () {
		Debug.Log ("Running...");
		if (!running)
			return;
		//loop = GetComponent<Loop> ();
		anim = GetComponent<Animation> ();
		anim.Initialize ();
		anim.drawLine (positionX,positionY,positionZ); // barre rouge

		// Initialisation camera
		fov = anim.Size + fovGap;
		Camera.main.transform.Translate(new Vector3(positionX-fov,positionY,positionZ));
		Camera.main.transform.Rotate(new Vector3(0f,90f,270f));

		addMetronome (5);

	}

	void FixedUpdate()
	{
		getInput ();
		for (int i=0; i<loops.Count; i++) 
		{
			loops[i].LoopTime = (Time.time / loops[i].Ratio) % loops[i].LoopDuration;
			loops[i].playSound ();
			anim.animateCylinder(0.02f * 360 / loops[i].LoopDuration,i);

		}

		if (metronomeSpheres == false) {
			addMetronomeSpheres();
		}
	}

	void getInput()
	{
		if (Input.GetKeyDown ("t"))
		{
			durationTime++;
		}

		if (Input.GetKeyDown ("g")) {
			if (durationTime > 0) {
				durationTime--;
			}
		}

		if (Input.GetKeyDown ("q")) 
		{
			loops[loopSelected].ClipNumber = 0;
			loops[loopSelected].AddSound();
			anim.drawSphere("black",loopSelected,positionX,positionY,positionZ);
		}

		if (Input.GetKeyDown ("s")) 
		{
			loops[loopSelected].ClipNumber = 1;
			loops[loopSelected].AddSound();
			anim.drawSphere("blue",loopSelected,positionX,positionY,positionZ);
		}
		
		if (Input.GetKeyDown ("d")) 
		{
			loops[loopSelected].ClipNumber = 2;
			loops[loopSelected].AddSound();
			anim.drawSphere("green",loopSelected,positionX,positionY,positionZ);
		}

		if (Input.GetKeyDown ("a")) 
		{
			if(durationTime <= 0)
			{
				Debug.Log ("IL FAUT DEFINIR UNE DUREE DE BOUCLE !!");
			}
			else
			{
				anim.Size = durationTime;
				anim.drawCylinder(loopNumber,positionX,positionY,positionZ);
				GameObject go = (GameObject)anim.Cylinders[loopNumber];
				go.AddComponent<Loop>();
				go.AddComponent<Sound>();
				go.AddComponent<AudioSource>();

				Loop cc = (Loop) go.GetComponent<Loop>();
				cc.Initialize ();
				cc.LoopDuration = durationTime;
				cc.Accuracy = 5.0f;
		
				if (loopNumber != 0){
					//loop.cameraMove();
				}
				// mets en jaune la boucle qui viens d'etre cree
				anim.makeWhite();
				anim.Cylinders[loopNumber].GetComponent<Renderer>().material.color = Color.yellow;
				loopSelected = loopNumber;
				// incrément du nombre de boucle
				loopNumber++;
				// remise à zéro de durationTime
				durationTime = 0;
				// ajout à la liste
				loops.Add(cc);
			}
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			anim.makeWhite();
			if (loopSelected<loops.Count-1 && loopSelected+1!=0) {
				loopSelected++;
			}
			anim.makeYellow(anim.Cylinders[loopSelected]);
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			anim.makeWhite();
			if (loopSelected>0 && loopSelected-1!=0) {
				loopSelected--;
			}
			anim.makeYellow(anim.Cylinders[loopSelected]);
		}
	}

	void addMetronome (int dureeMetronome)
	{
		anim.Size = dureeMetronome;
		anim.drawCylinder(0,0,0,0);
		GameObject go = (GameObject)anim.Cylinders[0];
		go.AddComponent<Loop>();
		go.AddComponent<Sound>();
		go.AddComponent<AudioSource>();

		Loop cc = (Loop) go.GetComponent<Loop>();
		cc.Initialize ();
		cc.LoopDuration = dureeMetronome;
		cc.Accuracy = 5.0f;

		loops.Add(cc);
		loopNumber++;

		loops[0].ClipNumber = 0;
		
		loops[0].AddSound(0.0f);
		loops[0].AddSound (1.25f);
		loops[0].AddSound (2.5f);
		loops[0].AddSound (3.75f);

	}

	void addMetronomeSpheres() {
		if (Math.Abs (loops [0].sounds [0].Time - loops [0].LoopTime + 
		              Time.fixedDeltaTime / loops [0].Ratio) <= Time.fixedDeltaTime * loops [0].Accuracy) {
			anim.drawSphere ("black", 0, positionX, positionY, positionZ);
			metronomeSpheresCount++;
		}
		
		if (Math.Abs (loops [0].sounds [1].Time - loops [0].LoopTime + 
		              Time.fixedDeltaTime / loops [0].Ratio) <= Time.fixedDeltaTime * loops [0].Accuracy) {
			anim.drawSphere ("black", 0, positionX, positionY, positionZ);
			metronomeSpheresCount++;
		}
		
		if (Math.Abs (loops [0].sounds [2].Time - loops [0].LoopTime + 
		              Time.fixedDeltaTime / loops [0].Ratio) <= Time.fixedDeltaTime * loops [0].Accuracy) {
			anim.drawSphere ("black", 0, positionX, positionY, positionZ);
			metronomeSpheresCount++;
		}
		
		if (Math.Abs (loops [0].sounds [3].Time - loops [0].LoopTime + 
		              Time.fixedDeltaTime / loops [0].Ratio) <= Time.fixedDeltaTime * loops [0].Accuracy) {
			anim.drawSphere ("black", 0, positionX, positionY, positionZ);
			metronomeSpheresCount++;
		}

		if (metronomeSpheresCount == loops [0].sounds.Count) {
			metronomeSpheres = true;
		}
	}

}

