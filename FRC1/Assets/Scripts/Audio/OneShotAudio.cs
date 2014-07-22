using UnityEngine;
using System.Collections;

public class OneShotAudio : MonoBehaviour {
	
	public AudioClip[] clip;
	
	void Start () {
		//audio.PlayOneShot(clip);
		AudioClip selectedClip = clip[Random.Range(0, clip.Length)];
		audio.PlayOneShot(selectedClip);
		Destroy(gameObject, selectedClip.length);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
