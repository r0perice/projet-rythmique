using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

/// <summary>
/// Classe principale de l'application. C'est elle qui possède un FixedUpdate et qui met à jour tous les autres composants.
/// C'est également dans cette classe que sont récuperés les events claviers.
/// La ligne rouge représente le moment ou les sons vont etre joués.
/// Les cylindres représentent les boucles.
/// Les sphères représentent les sons.
/// </summary>

public class Music : MonoBehaviour {

	private GameObject line; //!< La ligne.
	private Vector3 posLine; //!< Position de la ligne.

	private GameObject loop; //!< Le cylindre.

	private Predef predef;  //!< Instance de Predef qui sert à accéder aux boucles prédéfinies.

	public List<GameObject> loops = new List<GameObject>(); //!< Liste des boucles.

	public AudioSource audioSource; //!< Objet <a href="http://docs.unity3d.com/ScriptReference/AudioSource.html">AudioSource</a> qui permet de jouer un son.

	private Material yellow; //!< Définition du matériau Jaune.
	private Material neutre; //!< Définition du matériau Neutre.

	private float durationTime; //!< Durée de la boucle qui va être créée.
	private int loopNumber = 0; //!< Numéro de la boucle qui va être créée.
	private int loopSelected = -1; //!< Boucle selectionnée (en jaune).
	private float bpm  = 0.0f; //!< Nombre de BPM (battements par minute).
	private float mesure = 0.0f; //!< Nombre de mesure.

	// WiiMote functions
	[DllImport ("UniWii")]
	private static extern void wiimote_start();
	
	[DllImport ("UniWii")]
	private static extern void wiimote_stop();
	
	[DllImport ("UniWii")]
	private static extern int wiimote_count();
	
	
	
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonA(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonB(int which);
	
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccX(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccY(int which);
	[DllImport ("UniWii")]
	private static extern byte wiimote_getAccZ(int which);
	
	[DllImport ("UniWii")]
	private static extern float wiimote_getRoll(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getPitch(int which);
	[DllImport ("UniWii")]
	private static extern float wiimote_getYaw(int which);
	
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonUp(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonLeft(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonRight(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonDown(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButton1(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButton2(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonPlus(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonMinus(int which);
	[DllImport ("UniWii")]
	private static extern bool wiimote_getButtonHome(int which);
	
	// Wii attributes
	private String wii_display;
	private bool A;
	private bool B;
	private int X,Y,Z;
	private float R,P,W;
	
	private bool isPressable = true;
	public int pressableTime = 20;
	private int nbOfWiimote = 0;

	private bool condInf = false;
	private bool condSup = false;



	void Start () {
		wiimote_start ();
		line = (GameObject)Instantiate (Resources.Load ("Line"));
		posLine = line.transform.position;
		yellow = (Material)Resources.Load("Materials/Yellow");
		neutre = (Material)Resources.Load("Materials/Neutre");
		predef = this.gameObject.GetComponent<Predef> ();
		predef.AddPredef("Metronome"); // Ajout de la boucle prédéfinies Métronome.
	}

	void FixedUpdate()
	{
		getInput ();
		if (nbOfWiimote > 0) {
			getInputWiimote(0);
			unablePressKey ();
		}
	}
	/// <summary>
	/// Cette fonction permet d'empecher l'appuie sur une touche de la wii pendant un temps 
	/// 'pressableTime' afin de ne pas ajouter un son à chaque update 
	/// si on reste appuyer sur un bouton de la Wii Remote.
	/// </summary>
	void unablePressKey(){
		if (!isPressable) {
			pressableTime--;
		}
		if (pressableTime == 0) {
			pressableTime = 20;
			isPressable = true;  
		}
	}

	/// <summary>
	/// Dessine la fenetre graphique
	/// </summary>
	void OnGUI(){
		nbOfWiimote = wiimote_count ();
		
		if (nbOfWiimote > 0) {
			wii_display = "Wiimote detected " + isPressable; 
		} else
			wii_display = "Press the '1' and '2' buttons on your Wii Remote.";
		Rect rect = GUILayout.Window (0, new Rect (20, 20, 150, 150), drawGUI, "Options");
	}

	/// <summary>
	/// Dessine les boutons et récupère les événements associés.
	/// </summary>
	/// <param name="id">Identifiant de la fenetre.</param>
	void drawGUI(int id){
		String value = durationTime.ToString();
		GUILayout.Label (wii_display);
		GUILayout.Label ("Duration of the loop (in s) : " + (60.0f/bpm * mesure).ToString());
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("-")) {
			if (bpm > 0) {
				bpm-=10.0f;
			}
		}
		GUILayout.Label ("Nb of BpM : " + bpm.ToString ());
		if (GUILayout.Button ("+")) {
			bpm+=10.0f;
		}
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal ();
		if (GUILayout.Button ("-")) {
			if (mesure > 0) {
				mesure--;
			}
		}
		GUILayout.Label ("Nb of measure : " + mesure.ToString ());
		if (GUILayout.Button ("+")) {
			mesure++;
		}
		GUILayout.EndHorizontal ();
		if (value.Length > 0) {
			durationTime = int.Parse (value);
		}
		if (GUILayout.Button ("Add a Loop")) {
			if(bpm <= 0 || mesure <= 0)
			{
				Debug.Log ("IL FAUT DEFINIR LES BPM ET LES MESURES !!");
			}
			else
			{
				AddLoop ();
			}
		}
		//GUILayout.Label ("mouvement : " + getMovment());
		GUILayout.Label ("accelerometer Z : " + Z);
		GUILayout.Label (condInf + "  " + condSup );
	}


	/// <summary>
	/// Recupère les appuis sur les boutons de la wiimote.
	/// </summary>
	/// <param name="which">Le numéro de la WiiMote.</param>

	void getInputWiimote(int which){
		if (wiimote_getButtonPlus(which) && isPressable) { //Augmente le nombre de BpM.
			bpm+=10.0f;
			isPressable = false;
		}
		if (wiimote_getButtonMinus(which) && isPressable) { //Diminue le nombre de BpM.
			if (bpm > 0) {
				bpm-=10.0f;
			}
		}

		if (wiimote_getButtonLeft (which) && isPressable) { // Diminue le nombre de mesures.
			if (mesure > 0) {
				mesure--;
			}
			isPressable = false;
		}

		if (wiimote_getButtonRight (which) && isPressable) { // Augmente le nombre de mesures.

			mesure++;
			isPressable=false;
		}
		
		if (wiimote_getButtonA(which) && isPressable && !wiimote_getButtonB(which)) //Joue un son.
		{
			playSoundWithMovement("bass");
		}
		
		if (wiimote_getButtonB(which) && isPressable && !wiimote_getButtonA(which)) //Joue un son.
		{
			playSoundWithMovement("clave");
		}
		
		if (wiimote_getButtonA(which) && wiimote_getButtonB(which) && isPressable) //Joue un son.
		{
			playSoundWithMovement("bip");
		}
		
		if (wiimote_getButtonHome(which) && isPressable) //Crée une boucle.
		{
			if(bpm <= 0 || mesure <= 0)
			{
				Debug.Log ("IL FAUT DEFINIR LES BPM ET LES MESURES !!");
			}
			else
			{
				AddLoop ();
			}
		}
		
		if (wiimote_getButtonUp(which) && isPressable) //Change de boucle.
		{
			if (loopSelected>0) {
				loopSelected--;
				GameObject go = GameObject.Find("Loop "+loopSelected);
				MakeYellow(go);
			}
			isPressable = false;
		}
		
		if (wiimote_getButtonDown(which) && isPressable) //Change de boucle.
		{
			if (loopSelected < loops.Count-1) {
				loopSelected++;
				GameObject go = GameObject.Find("Loop "+loopSelected);
				MakeYellow(go);
			}
			isPressable = false;
		}
		
	}


	/*bool disableMouvement(){

	}*/

	bool playSoundWithMovement(String son){
		int z = wiimote_getAccZ (0);
			if(z<50){
				condInf = true;
			}
			if(z>200){
				condSup = true;
			}
			if(condInf && condSup){
				condInf=false;
				condSup=false;
			isPressable = false;
				loops[loopSelected].GetComponent<Loop>().AddSound(son);
			return true;
			}
			return false;
	}

	/// <summary>
	/// Lit les inputs clavier.
	/// </summary>

	void getInput()
	
	{
		if (Input.GetKeyDown ("m")) // Mute une boucle.
		{
			GameObject go = GameObject.Find(loops[loopSelected].name);
			audioSource = go.GetComponent<AudioSource>();

			if (audioSource.mute)
				audioSource.mute = false;
			else
				audioSource.mute = true;
		}

		if (Input.GetKeyDown ("x")) // Effacer une boucle.
		{
			if (loops.Count==0) {
				Debug.Log ("Pas de boucle à supprimer");
			}
			else {
				DestroyLoop();
				Replace(loopSelected);
			}
		}

		if (Input.GetKeyDown ("t")) // Augmente les BPM de la boucle.
		{
			bpm+=10.0f;
			Debug.Log ("bpm : "+bpm);
		}
		
		if (Input.GetKeyDown ("g")) { // Diminue les BPM de la boucle.
			if (bpm > 0) {
				bpm-=10.0f;
				Debug.Log ("bpm : "+bpm);
			}
		}

		if (Input.GetKeyDown ("y")) // Augmente le nombre de mesure de la boucle.
		{
			mesure++;
			Debug.Log ("mesure : "+mesure);
		}
		
		if (Input.GetKeyDown ("h")) { // Diminue le nombre de mesure de la boucle.
			if (mesure > 0) {
				mesure--;
				Debug.Log ("mesure : "+mesure);
			}
		}

		if (Input.GetKeyDown ("q")) // Ajout du son bass.
		{
			loops[loopSelected].GetComponent<Loop>().AddSound("bass");
		}
		
		if (Input.GetKeyDown ("s")) // Ajout du son clave.
		{
			loops[loopSelected].GetComponent<Loop>().AddSound("clave");
		}
		
		if (Input.GetKeyDown ("d")) // Ajout du son bip.
		{
			loops[loopSelected].GetComponent<Loop>().AddSound("bip");
		}

		if (Input.GetKeyDown ("a")) // Ajout d'une boucle
		{
			if(bpm <= 0 || mesure <= 0)
			{
				Debug.Log ("IL FAUT DEFINIR LES BPM ET LES MESURES !!");
			}
			else
			{
				AddLoop ();
			}
		}

		if (Input.GetKeyDown (KeyCode.LeftArrow)) // Sélection de la boucle de gauche.
		{
			if (loopSelected>0) {
				loopSelected--;
				GameObject go = GameObject.Find("Loop "+loopSelected);
				MakeYellow(go);
			}
		}
		
		if (Input.GetKeyDown (KeyCode.RightArrow)) // Sélection de la boucle de droite.
		{
			if (loopSelected < loops.Count-1) {
				loopSelected++;
				GameObject go = GameObject.Find("Loop "+loopSelected);
				MakeYellow(go);
			}
		}
	}

	/// <summary>
	/// Met à jour la liste des boucles.
	/// </summary>
	/// <param name="index">Permet de choisir entre l'ajout et la suppression d'une boucle.</param>
	public void UpdateArray(int index = -1){
		if (index == -1) {
			loops.Clear(); // Vide la liste.
			// Re-parcourt la scène afin de trouver les boucles (= cylindres).
			foreach(GameObject go in (UnityEngine.GameObject.FindGameObjectsWithTag("Loop"))){
				loops.Add(go);
			}
		}
		else {
			loops.Remove(loops[index]); // Enlève de la liste la boucle sélectionnée.
		}
		CameraMove (); // Replace la caméra.

	}

	/// <summary>
	/// Met en surbrillance la boucle sélectionnée.
	/// </summary>
	/// <param name="go">La boucle à mettre en surbrillance</param>
	public void MakeYellow(GameObject go){
		// Repasse tout en neutre.
		foreach (GameObject obj in loops) {
			obj.GetComponent<Renderer> ().material = neutre;
		}
		go.GetComponent<Renderer>().material = yellow; // Met en jaune uniquement la boucle sélectionnée.
	}

	/// <summary>
	/// Détruit la boucle sélectionnée.
	/// </summary>
	public void DestroyLoop(){
		loopNumber--;
		GameObject go = GameObject.Find("Loop "+loopSelected);
		Destroy(go);
		UpdateArray (loopSelected);
	}

	/// <summary>
	/// Re-positionne les boucles après une suppression.
	/// </summary>
	/// <param name="select">Numéro de la boucle qui a été supprimée.</param>
	public void Replace(int select){
		// Translation de toutes les boucles situées à droite de celle supprimée.
		for (int i=select; i<=loops.Count-1; i++){
			loops[i].transform.Translate(Vector3.up*0.3f);
		}
		// Re-nommage des boucles.
		for (int j=0; j<=loops.Count-1; j++) {
			loops[j].name = ("Loop "+j);
		}
		// Surbrillance de celle de droite ou de gauche.
		if (loopSelected == loopNumber) {
			MakeYellow (loops [loopSelected - 1]);
			loopSelected--;
		} 
		else {
			MakeYellow (loops [loopSelected]);
		}
	}

	/// <summary>
	/// Ajoute des boucles.
	/// </summary>
	public void AddLoop(){
		durationTime = 60.0f/bpm * mesure; // durée de la boucle.
		// Instanciation d'un cylindre, nommage, ajout à la liste et positionnement.
		loop = (GameObject)Instantiate (Resources.Load ("Loop"));
		loop.name = ("Loop "+loopNumber);
		UpdateArray ();
		loop.transform.position = new Vector3(0.3f*loops.Count,0,posLine.z+(1.0f+durationTime/10.0f)/2.0f+0.3f);
		loop.transform.localScale = new Vector3((1.0f+durationTime/10.0f),0.1f,(1.0f+durationTime/10.0f));

		MakeYellow(loop); // Surbrillance du nouveau cylindre.

		loop.GetComponent<Loop> ().LoopDuration = durationTime; // Définition de la durée.

		// RAZ des BPM et des mesures.
		bpm = 0;
		mesure = 0;

		// Incrémentation du nombre de boucles et la sélection.
		loopNumber++;
		loopSelected++;
	}

	/// <summary>
	/// Repositionnement de la caméra.
	/// </summary>
	public void CameraMove(){
		this.gameObject.transform.position = new Vector3 (0.3f + 0.15f * (loops.Count - 1), 0.0f, -3.0f);
	}

	public float DurationTime {
		get {
			return durationTime;
		}
		set {
			durationTime = value;
		}
	}

	public int LoopSelected {
		get {
			return loopSelected;
		}
	}

	public float Bpm {
		get {
			return bpm;
		}
		set {
			bpm = value;
		}
	}

	public float Mesure {
		get {
			return mesure;
		}
		set {
			mesure = value;
		}
	}
}