using UnityEngine;
using System.Collections.Generic;
using System;

public delegate void LoopEvent(Loop loop);

public class Loop : MonoBehaviour {
	
	private float timeDown = 0.0f; 
	private bool running = true;
	private List<FakeTuple> liste = new List<FakeTuple> ();
	private int dureeBoucle= 10;


	// Use this for initialization
	void Start () {
		Debug.Log ("Running...");
		if (!running)
			return;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

	//	while (i<=liSound.Count) {
		//
	//		i++;
	//	}


		//mesure le temps

		timeDown += Time.deltaTime;
		//Debug.Log ("time" + timeDown);

		if (timeDown >= dureeBoucle) {
			timeDown=0.0f;
		}
	}

	private void MakeSound(AudioClip originalClip){
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}
	
	void OnGUI(){
		if (Event.current.Equals(Event.KeyboardEvent("a"))) {
			//var sound1 = new GameObject();
			//sound1.AddComponent<Sound>();
			Debug.Log ("The A key was pressed");
			//Debug.Log(sound1.getAudioClip(0));
			//FakeTuple listeA = new FakeTuple(timeDown,sound1.GetComponent().getAudioClip(0));
			liste.Add(listeA);
		}	

		if (Event.current.Equals (Event.KeyboardEvent ("z"))) {
			Debug.Log ("The Z key was pressed");
			Debug.Log(liste[0].getAudio());
		}
	
	}

}