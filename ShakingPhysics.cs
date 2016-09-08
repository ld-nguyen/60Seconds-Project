/*
 *Long Duc Nguyen 
 *
 *ShakingPhysics -  Gibt dem Objekt an dem dieses Skript rangehangen wird 2D Physik. Die Kräfte werden druch das Schütteln des 
 *Tablets ausgelöst
 *
 *Quellen:
 *	https://www.youtube.com/watch?v=XZWNXsjIvrE - Accelerometer Input
 *	Unity Dokumentation für addForce
 */
using UnityEngine;
using System.Collections;

public class ShakingPhysics : MonoBehaviour {
	private Vector2 inputForce;
	public float velocity;
	void Start () {
	
	}

	void FixedUpdate () {
		inputForce = new Vector2 (Input.acceleration.x*velocity, Input.acceleration.z*velocity); //Tabletbewegung in einen Kraftvektor umrechnen
		rigidbody2D.AddForce (inputForce); //Kraft auf das Objekt anwenden
	}
}
