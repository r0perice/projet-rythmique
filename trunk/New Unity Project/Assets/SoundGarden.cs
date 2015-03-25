using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoundGarden : MonoBehaviour {
	private Dictionary<float,AudioClip> dico;
	private float timer = 0.0f;
	public float loop;
	public List<AudioClip> AudioList;
	GameObject instance;
	
	public SoundList sl;//on peut aussi drag'n' drop soundlist dans l'éditeur
	// Use this for initialization
	void Start () {
		sl = GetComponent<SoundList>();
		foreach(AudioClip a in sl.audio){
			AudioList.Add(a);
		}
		dico = new Dictionary<float,AudioClip >();
	}
	
	public void Particles(Color c){
		if(instance!=null){
			Destroy(instance);
		}
		instance = Instantiate(Resources.Load("Particle", typeof(GameObject))) as GameObject;
		instance.transform.FindChild("Particle System").GetComponent<ParticleSystem>().startColor = c;
		
	}
	
	public void getInput(){
		
		if(Input.GetKeyDown(KeyCode.A)){
			if(dico!=null && !dico.ContainsKey(timer)){
				dico.Add(timer,AudioList[0]);
				GetComponent<AudioSource>().clip = AudioList[0];
				GetComponent<AudioSource>().Play();
				
			}
		}
		if(Input.GetKeyDown(KeyCode.Z)){
			if(dico!=null&&!dico.ContainsKey(timer)){
				dico.Add(timer,AudioList[1]);
				GetComponent<AudioSource>().clip = AudioList[1];
				GetComponent<AudioSource>().Play();
			}
		}
		if(Input.GetKeyDown(KeyCode.E)){
			if(dico!=null && !dico.ContainsKey(timer)){
				dico.Add(timer,AudioList[2]);
				GetComponent<AudioSource>().clip = AudioList[2];
				GetComponent<AudioSource>().Play();
			}
		}
		if(Input.GetKeyDown(KeyCode.R)){
			if(dico!=null && !dico.ContainsKey(timer)){
				dico.Add(timer,AudioList[3]);
				GetComponent<AudioSource>().clip = AudioList[3];
				GetComponent<AudioSource>().Play();
			}
		}
		
	}
	// Update is called once per frame
	void Update () {
		timer+= Time.deltaTime;
		getInput();
		if(timer>=loop){
			timer = 0.0f;
		}
		
		foreach(float f in dico.Keys){
			if(Mathf.Abs(f-timer)<=0.02f){
				GetComponent<AudioSource>().clip = dico[f];
				GetComponent<AudioSource>().Play();
				Particles(new Color(timer/5.0f,timer/10.0f,1.0f));
			}
		}
	Debug.Log(timer);
	}
}
