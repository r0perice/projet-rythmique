using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Music : MonoBehaviour {
	
	public List<Loop> loops = new List<Loop>();


	public Loop loop;

	public int loopDuration = 5;
	private bool running = true;
	private int loopNumber = 0; // Création de la boucle numéro...
	private int loopSelected = 0; // Selection de la boucle

	// Couleurs
	private Color color1 = Color.black;
	private Color color2 = Color.blue;
	private Color color3 = Color.green;

	// Use this for initialization
	void Start () {
		Debug.Log ("Running...");
		if (!running)
			return;
		loop = new Loop ();
	}


	void FixedUpdate()
	{
		getInput ();
		foreach (Loop lp in loops) {
			lp.LoopTime = (Time.time / lp.Ratio) % loopDuration;
			lp.playSound ();
		}
		if (loops.Count != 0) {
			loop.animateCylinder (0.02f * 360 / loopDuration);
		}
	}

	void getInput()
	{
		if (Input.GetKeyDown ("q")) 
		{
			loops[loopSelected].ClipNumber = 0;
			loops[loopSelected].AddSound();
			loop.drawSphere(color1,loopSelected);
		}

		if (Input.GetKeyDown ("s")) 
		{
			loops[loopSelected].ClipNumber = 1;
			loops[loopSelected].AddSound();
			loop.drawSphere(color2,loopSelected);
		}
		
		if (Input.GetKeyDown ("d")) 
		{
			loops[loopSelected].ClipNumber = 2;
			loops[loopSelected].AddSound();
			loop.drawSphere(color3,loopSelected);
		}

		if (Input.GetKeyDown ("a")) 
		{
			loop = gameObject.GetComponent<Loop> ();
			loops.Add(loop);

			loop.drawCylinder(loopNumber);

			if (loopNumber != 0){
				loop.cameraMove();
			}
			// mets en jaune la boucle qui viens d'etre cree
			loop.makeWhite();
			loop.Cylinders[loopNumber].GetComponent<Renderer>().material.color = Color.yellow;
			loopSelected = loopNumber;
			// incrément du nombre de boucle
			loopNumber++;

		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) 
		{
			loop.makeWhite();
			if (loopSelected<loops.Count-1) {
				loopSelected++;
			}
			loop.Cylinders[loopSelected].GetComponent<Renderer>().material.color = Color.yellow;
		}

		if (Input.GetKeyDown (KeyCode.DownArrow)) 
		{
			loop.makeWhite();
			if (loopSelected>0) {
				loopSelected--;
			}
			loop.Cylinders[loopSelected].GetComponent<Renderer>().material.color = Color.yellow;
		}
	}


}

