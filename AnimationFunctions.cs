/*
 * Long Duc Nguyen
 * 
 * Statische Methoden für verschiedene Sachen. Die Methoden werden meistens in den Minispiel Skripten aufgerufen.
 * 
 * DiseaseSolved_SetPoints wurde von Markus Wolterdorf geschrieben.
 * 
 * Benutzte Quellen:
 * http://unity3d.com/learn/tutorials/modules/beginner/scripting/instantiate
 * Unity Dokumentationen
 */


using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimationFunctions : MonoBehaviour {
	private static int points;

	public static void diseaseSolvedFX(Vector3 pos){
		GameObject prefab = Instantiate(Resources.Load("Prefabs/SolvedDisease_new"),pos,new Quaternion()) as GameObject;
		string newPointText;
		Debug.LogWarning ("LÖSE: " + points);
		if (points == 0) {newPointText = "Super";}
		else if (points == 1) {newPointText = "+" + points;}
		else { newPointText = "+" + points; }
		prefab.GetComponentInChildren<Text> ().text = newPointText;
		Handheld.Vibrate ();

		points = 0;
	}

	public static void diseaseSolved_SetPoints(int p){
		World world = World.getInstance ();
		if ( (world.PointSystem.Points + p) > PointSystem.START_VALUE_FOR_POINTS) {
			int tooMuch = (world.PointSystem.Points+p) - PointSystem.START_VALUE_FOR_POINTS;
			int diff = p - tooMuch;
			points = diff;
		} else {
			points = p;
		}
	}

	public static void diseaseSolvedFace(){
		Animator anim = GameObject.Find ("head").GetComponent<Animator>();
		anim.SetTrigger("diseaseSolved");
	}

	public static void diseasePukeFace(){
		Animator anim = GameObject.Find ("head").GetComponent<Animator>();
		anim.SetTrigger("pukeTrigger");
	}

	public static void coughFaceAnim(){
		Animator anim = GameObject.Find ("head").GetComponent<Animator>();
		anim.SetTrigger("coughTrigger");
	}

	public static void coughStartAnim(){
		Animator anim = GameObject.Find ("head").GetComponent<Animator>();
		anim.SetTrigger("coughStartTrigger");
	}

	public static void cariesMouth(bool isActivated){
		Animator anim = GameObject.Find ("head").GetComponent<Animator> ();
		anim.SetBool("hasCaries",isActivated);
	}

	public static void activateFracture(){
		AudioSource sound  = GameObject.Find ("SoundSource").GetComponent<AudioSource> ();
		AudioClip[] boneCracks = Resources.LoadAll<AudioClip>("Sound/Disease/Fracture");
		sound.clip = boneCracks[Random.Range(0,boneCracks.Length)];
		sound.Play ();
	}

	public static void playBell(){
		AudioSource sound  = GameObject.Find ("SoundSource").GetComponent<AudioSource> ();
		AudioClip bell = Resources.Load<AudioClip>("Sound/Effects/Bell01");
		sound.clip = bell;
		sound.Play ();
	}

	public static void stomachSound(){
		AudioSource sound  = GameObject.Find ("SoundSource").GetComponent<AudioSource> ();
		AudioClip stomach = Resources.Load<AudioClip>("Sound/Effects/Events_StomachSound01");
		sound.clip = stomach;
		sound.Play ();
	}

}
