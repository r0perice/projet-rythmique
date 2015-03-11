//TEST COMMIT PAR ROBIN

using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour 
{
	public static Sound Instance;

	public AudioClip blasterSound;
	public AudioClip glassSound;
	public KeyCode kcode1;
	public KeyCode kcode2;

	void Update(){
		if (Input.GetKey (kcode1)) {
			print ("J'ai appuyé sur A");
			MakeBlasterSound ();
		}
		if (Input.GetKey(kcode2)) {
			print ("J'ai appuyé sur Z");
			MakeGlassSound();
		}
	}

	private void MakeSound(AudioClip originalClip){
		AudioSource.PlayClipAtPoint(originalClip, transform.position);
	}

	public void MakeBlasterSound(){
		MakeSound (blasterSound);
	}

	public void MakeGlassSound(){
		MakeSound (glassSound);
	}
	/*
	public void MakeKalimbaSound()
	{
		MakeSound(kalimbaSound);
	}

	private void MakeSound(AudioClip originalClip)
	{
		AudioSource.PlayClipAtPoint (originalClip, transform.position);
	}

	void OnGUI() {
		if (Event.current.Equals (Event.KeyboardEvent ("a")))
			print("I press a");
			MakeKalimbaSound ();
		
		if (Event.current.Equals(Event.KeyboardEvent("b")))
			print("I press b");
		
	}*/



}