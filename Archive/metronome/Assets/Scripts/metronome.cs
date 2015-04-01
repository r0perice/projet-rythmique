using UnityEngine;
using System.Collections;

public delegate void MetronomeEvent(metronome metronome);

public class metronome : MonoBehaviour {
    public int Base;
    public int Step;
    public float BPM;
    public int CurrentStep = 1;
    public int CurrentMeasure;

    private float interval;
    private float nextTime;

    public event MetronomeEvent OnTick;
    public event MetronomeEvent OnNewMeasure;

	public AudioClip sound1;
	public AudioClip sound2;


	// Use this for initialization
	void Start () {
		Debug.Log ("Running...");

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey(KeyCode.A)) {
			Debug.Log ("The A key was pressed");
			StartMetronome();
		}

		if (Input.GetKey(KeyCode.Z)) {
			Debug.Log ("The Z key was pressed");
			StopMetronome();
		}
	}

    public void StartMetronome()
    {
        StopCoroutine("DoTick");
        CurrentStep = 1;
        var multiplier = Base / 4f;
        var tmpInterval = 60f / BPM;
        interval = tmpInterval / multiplier;
        nextTime = Time.time;
        StartCoroutine("DoTick");
    }

	public void StopMetronome()
	{
		StopCoroutine("DoTick");
		CurrentStep = 1;
		CurrentMeasure = 0;
	}

    IEnumerator DoTick()
    {
        for (; ; )
        {
            if (CurrentStep == 1 && OnNewMeasure != null)
                OnNewMeasure(this);
            if (OnTick != null)
                OnTick(this);
            nextTime += interval;
            yield return new WaitForSeconds(nextTime - Time.time);
            CurrentStep++;
			// PERSO
			AudioSource.PlayClipAtPoint(sound1, transform.position);
			// PERSO
            if (CurrentStep > Step)
            {
                CurrentStep = 1;
                CurrentMeasure++;
				// PERSO	
				AudioSource.PlayClipAtPoint(sound2, transform.position);
				// PERSO
            }
        }
    }
}
