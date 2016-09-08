/*
 * Long Duc Nguyen
 * 
 * FartControl - Kleines Skirpt zum Abspielen eines Sounds für den Furz
 */

using UnityEngine;
using System.Collections;

public class FartControl : MonoBehaviour {
	private AudioSource audio;
	private AudioClip[] clips;

	void Start(){
		audio = this.GetComponent<AudioSource> ();
		clips = Resources.LoadAll<AudioClip> ("Sound/Disease/Fart");
	}

	public void playFartAudio(){
		audio.clip = clips [Random.Range (0, clips.Length)];
		audio.Play ();
	} 
}
