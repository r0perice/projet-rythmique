using UnityEngine;
using System.Collections.Generic;
using System;

public delegate void LoopEvent(loop loop);

public class loop : MonoBehaviour {
	
	private float timeDown = 0.0f; 
	private bool running = true;
	private List<Tuple<float,AudioClip>> liste = new List<Tuple<float,AudioClip>>();
	private Sound sound1 = new Sound();
	private int dureeBoucle= 10;

	public class Tuple<T,U>
	{
		public T Item1 { get; private set; }
		public U Item2 { get; private set; }
		
		public Tuple(T item1, U item2)
		{
			Item1 = item1;
			Item2 = item2;
		}
	}
	
	public static class Tuple
	{
		public static Tuple<T, U> Create<T, U>(T item1, U item2)
		{
			return new Tuple<T, U>(item1, item2);
		}
	}

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
			Debug.Log ("The A key was pressed");
			Tuple<float,AudioClip> couple = new Tuple<float,AudioClip>(timeDown,sound1.getAudioClip(0));
			liste.Add(couple);
			Debug.Log(sound1.getAudioClip(0).name);
			//liSound.Add(sound1.getAudioClip(0));
			//liFloat.Add(timeDown);
		}	

		if (Event.current.Equals (Event.KeyboardEvent ("z"))) {
			Debug.Log ("The Z key was pressed");
			Debug.Log (liste[0].Item1);
			Debug.Log (liste[0].Item2);
		}
	
	}

}