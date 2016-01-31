using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class Tone : MonoBehaviour 
{
	public bool BlockStop { get; private set; }
	public AudioMixerGroup MasterGroup, HighlightedGroup;
    public float FadeOutSpeed = 0.05f;
	private DragableObject obj;
	private GameObject glowObj;
	private SpriteRenderer glow;
	private AudioFader fader;

	public bool Draggable { set { obj.Draggable = value; } get { return obj.Draggable; } }
	
	public bool Dragging
	{
		get { return obj.Dragging; }
	}
	
	// Use this for initialization
	void Start () {
	   	obj = GetComponent<DragableObject>();
		fader = GetComponent<AudioFader>();
		obj.OnDragStart += OnDragStart;
		obj.OnDragStop += OnDragStop;

		glowObj = new GameObject ("glow");
		glowObj.transform.parent = this.transform;
		glow = glowObj.AddComponent<SpriteRenderer> ();
		glow.sprite = GetComponent<SpriteRenderer> ().sprite;
		glow.GetComponent<Renderer>().enabled = false;
	}
		
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnDragStart()
	{
		var src = GetComponent<AudioSource> ();
		src.outputAudioMixerGroup.audioMixer.SetFloat ("ToneVolume", -20f);
		src.outputAudioMixerGroup = HighlightedGroup;
		src.volume = 1.0f;
		src.Play();
		BlockStop = true;
	}
	
	void OnDragStop()
	{
		var src = GetComponent<AudioSource> ();
		src.outputAudioMixerGroup.audioMixer.SetFloat ("ToneVolume", 0.0f);
		src.outputAudioMixerGroup = MasterGroup;
		src.Stop();
		BlockStop = false;
	}

	public void Play()
	{
		iTween.PunchScale(gameObject, Vector3.one, 0.5f);
		//iTween.PunchScale(glowObj, Vector3.one, 0.5f);

		GetComponent<AudioSource>().volume = 1.0f;
		fader.Fade(FadeOutSpeed);	
	}
		
	public void SetGreen()
	{
		glow.transform.localPosition = new Vector2 (0, 0);//position = nGetComponent<SpriteRenderer> ().transform.position;
		glow.transform.rotation = GetComponent<SpriteRenderer> ().transform.rotation;
		glow.transform.localScale = new Vector2 (1.4f, 1.4f);//GetComponent<SpriteRenderer> ().transform.localScale;
		glow.color = new Color(0,150,0,0.8f);
		glow.GetComponent<Renderer>().enabled = true;
	}
	
	public void SetWhite()
	{
		glow.GetComponent<Renderer>().enabled = false;
		GetComponent<SpriteRenderer>().color = Color.white;
	}
}

