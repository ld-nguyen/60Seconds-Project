using UnityEngine;
using System.Collections;

public class playSoundOnClick : MonoBehaviour {
	private AudioSource audio;
	void Start(){
		audio = this.gameObject.GetComponent<AudioSource> ();
	}
	public void playSound(){
		audio.Play();
	}
}
