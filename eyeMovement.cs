/*
 * Long Duc Nguyen
 * 
 * EyeMovement - Skript das die Augen bewegt wenn der Spieler irgendwo auf das Tablet tippt.
 * 
 * Camera.main.ScreenToWorldPoint stammt aus der Antowrt einer Frage in "Unity Answers". Link ist leider unbekannt.
 * Input.touches aus Lauras Skripten und https://www.youtube.com/watch?v=SrCUO46jcxk
 */

using UnityEngine;
using System.Collections;

public class eyeMovement : MonoBehaviour {
	private Transform irisL; //Position der Iris im Auge (WorldSpace)
	private Transform irisR;

	private Vector3 irisCenterL; // Ursprüngliche Position der Iris im Auge (WorldSpace)
	private Vector3 irisCenterR;

	private Vector3 touchPosWorld; //Position des Touches im World Space
	private Vector3 touchPosScreen; //Position des Touches auf dem Screen

	private Vector3 leftEyeToTarget; //Vektor vom Auge zur Touch-Position
	private Vector3 rightEyeToTarget;

	private Vector3 targetPosL; //Tatsächlicher Bewegungsvektor
	private Vector3 targetPosR;

	private bool leftEye; // War der Touch auf der Linken Hälfte des Screens oder auf der rechten?

	void Start(){
		irisL = GameObject.Find("TX_EyeLeft/iris").transform; //Irisobjekte der Augen holen
		irisR = GameObject.Find ("TX_EyeRight/iris").transform;
		irisCenterL = irisL.position; //Startpositionen der Iris festlegen
		irisCenterR = irisR.position;
		targetPosL = irisCenterL;	//Zielposition der Iris = Startposition
		targetPosR = irisCenterR; 
	}
	void Update(){
		foreach (Touch t in Input.touches) {
			if(t.phase == TouchPhase.Began){
				touchPosScreen = t.position; //ScreenSpace Position aus den Touch Input holen
				touchPosWorld = Camera.main.ScreenToWorldPoint(touchPosScreen); //Umwandeln der Koordinaten in Worldspace
				//touchPosWorld.z = 0f; 
				leftEyeToTarget = irisCenterL-touchPosWorld; 
				rightEyeToTarget = irisCenterR-touchPosWorld;
				if(leftEyeToTarget.magnitude <= rightEyeToTarget.magnitude){
					leftEye = true;
					leftEyeToTarget = Limit(leftEyeToTarget);
				} else { 
					leftEye = false;
					rightEyeToTarget = Limit(rightEyeToTarget);
				}
			}
			if(leftEye){
				targetPosL = (irisCenterL-leftEyeToTarget);
				targetPosR = targetPosL;
				targetPosR.x += 125f; //Abstand zwischen den beiden Augen
			}else{
				targetPosR = (irisCenterR - rightEyeToTarget);
				targetPosL = targetPosR;
				targetPosL.x -= 125f;
			}
		}
		//Debug.Log ("left"+targetPosL+" right "+targetPosR);
		irisL.position = Vector3.Lerp (irisL.position, targetPosL, Time.deltaTime*2);
		irisR.position = Vector3.Lerp (irisR.position, targetPosR, Time.deltaTime*2);
	}
	private Vector3 Limit(Vector3 v){
		v.x *= 0.2f;
		v.y *= 0.2f;
		if (v.y >= 13f) {
			v.y = 13f;
		}
		if (v.y <= -13f) {
			v.y = -13f;
		}
		if (v.x <= -13f) {
			v.x = -13f;
		}
		if (v.x >= 13f) {
			v.x = 13f;
		}
		//Debug.Log (v);
		return v;
 	}
}