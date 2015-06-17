using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Cette classe implémente la partie graphique de l'application. 
/// Elle permet d'instancier des cylindres et des sphères, de les mettre en mouvement, etc ...
/// </summary>

public class Animation : MonoBehaviour {

	private Loop lp;  //!< Instance de Loop qui sert à accéder aux paramètres de la boucle.

	void Start () {
		lp = this.gameObject.GetComponent<Loop> ();
	}
	

	void FixedUpdate () {
		this.transform.Rotate (Vector3.up, 0.02f* 360 / lp.LoopDuration ); // Fait tourner la boucle.
	}


}
