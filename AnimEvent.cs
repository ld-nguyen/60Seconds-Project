/*
 * Long Duc Nguyen
 * 
 * AnimEvent - Funktionen die beim Abspielen einiger Animationen ausgeführt werden. Die Funktionen werden mit Hilfe
 * von Animation Events aufgerufen.
 */

using UnityEngine;
using System.Collections;

public class AnimEvent : MonoBehaviour {
	private AudioSource audio; //
	private Animator anim;
	private int index;

	void Start(){
				audio = this.gameObject.GetComponent<AudioSource> ();
				anim = this.gameObject.GetComponent<Animator> ();
		}

	// Die folgene Methode gibt es nur weil die Unity AnimationEvents nur einen Parameter haben dürfen. Wird in playEmoteAudio gebraucht.
	public void getNextInt(int max){
		index = Random.Range(0,max);
	}

	//Spielt einen Sound aus einem Pool von möglichen Sounddateien für eine Emotion. Der index wird in der Methode getNextInt() gesetzt.
	public void playEmoteAudio(string emote){
		AudioClip sound = Resources.Load<AudioClip>("Sound/Monster/"+emote+index);
		audio.clip = sound;
		audio.Play ();
	}

	public void setNextAnimIndex(){
		anim.SetInteger("animIndex",Random.Range(0,2)); // Setzt den animIndex Parameter auf einen zufälligen Wert.
	}
	void Update() {
		int healthPoints = World.getInstance().PointSystem.Points;
		anim.SetInteger ("lifePoints", healthPoints);
	}
}
