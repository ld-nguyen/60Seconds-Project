/*
 * Long Duc Nguyen
 * 
 * BackgroundSwitch - Skript das den "Jahresverlauf" im Hintergrund des Spiel steuert. An sich werden in bestimmten Intervallen
 * Grafiken geladen und zwischen denen gefadet. Die Hintergrundgrafiken der "jetzigen" Jahreszeit werden ausgeblendet und
 * die Grafiken der "nächsten" Jahreszeit werden gleichzeitig eingeblendet.
 */


using UnityEngine;
using System.Collections;

public class BackgroundSwitch : MonoBehaviour {
	private string basicLocation = "Sprites/Backgrounds/Game/"; // Assets-Location 

	private float timeLeft; // Timer, der sagt wie viel Zeit noch übrig ist.
	private float fadeSpeed = 0.6f; // Modifikator für die Geschwindigkeit des Fade-Effekts
	private int season = 0; //Wird als Merker für die jeweilige Jahreszeit benutzt. 0 - Spring , 1 - Summer , 2 - Fall, 3 - Winter

	private SpriteRenderer[] currentSeason; //SpriteRenderer Array der Hintergrundkomponenten für die momentane Jahreszeit
	private SpriteRenderer[] nextSeason;

	private GameObject spring_fx; //GameObject mit Partikel System für die jeweilige Jahreszeit
	private GameObject summer_fx;
	private GameObject fall_fx;
	private GameObject winter_fx;

	void Start(){
		//Referenzen zu den Objekten finden
		currentSeason = GameObject.Find ("current").GetComponentsInChildren<SpriteRenderer> ();
		nextSeason = GameObject.Find ("next").GetComponentsInChildren<SpriteRenderer> ();
		timeLeft = World.getInstance ().TimeSystem.TimeLeft;
		summer_fx = this.transform.GetChild (3).gameObject;
		fall_fx = this.transform.GetChild (4).gameObject;
		winter_fx = this.transform.GetChild (5).gameObject;
	}

	void Update(){
	 //Die Transitions zwischen den Jahreszeiten kommen immer in 15er Sekunden Intervallen ab 50 Sekunden
		if(SeasonCheck()){
			FadeOutIn();
			if(currentSeason[0].color.a <= 0.02f){ //Wenn die Objekte in currentSeason fast komplett duchsichtig sind. 
				season++; 							
				setSprites(season);
			}
		}
		timeLeft = World.getInstance ().TimeSystem.TimeLeft; //Verbleibende Zeit im Spiel aus dem TimeSystem holen
	}

	//SeasonCheck() überprüft die Werte von timeLeft und season und gibt einen boolean Wert zurück um eine Transition zwischen zwei Jahreszeiten auszulösen
	bool SeasonCheck(){
		if (timeLeft <= 50 && season == 0 || 
		    timeLeft <= 35 && season == 1 || 
		    timeLeft <= 20 && season == 2) 
		{
			if(season == 0 && timeLeft > 0){ //Wenn die Bedingungen oben erfüllt werden dann wird das PratikelSystem für die nächste Jahreszeit aktiviert. 
				summer_fx.SetActive(true);
			}
			if(season == 1){
				fall_fx.SetActive(true);
			}
			if(season == 2){
				winter_fx.SetActive(true);
			}
			return true;		
		}
	else return false;
	}

	//Methode für den Fade-Effekt im Hintergrund. Für die currentSeason und nextSeason wird eine Farbe als Ziel vorgegeben
	// und dann zwischen der momentanen Farbe des jeweiligen SpriteRenderers und der Zielfarbe eine Lineare Interpolation durchgeführt.
	void FadeOutIn(){
		foreach (SpriteRenderer sp in currentSeason) {
			Color noAlpha = new Color(sp.color.r,sp.color.g,sp.color.b,0.0f); 
			sp.color = Color.Lerp (sp.color, noAlpha, fadeSpeed * Time.deltaTime);
			}
		foreach (SpriteRenderer sp in nextSeason){
			Color fullAlpha = new Color(sp.color.r,sp.color.g,sp.color.b,1.0f);
			sp.color = Color.Lerp (sp.color, fullAlpha, fadeSpeed * Time.deltaTime);
		}
	}

	//Wird am Ende einer Transition aufgerufen und bereitet den nächsten Wechsel vor
	void setSprites(int season){
		switch (season) {
		case 1: // Summer to Fall
			switchSeason("summer",currentSeason,1.0f);
			switchSeason("fall",nextSeason,0.0f);
		break;
		case 2: // Fall to Winter
			switchSeason("fall",currentSeason,1.0f);
			switchSeason("winter",nextSeason,0.0f);
		break;
		case 3: // Winter
			switchSeason("winter",currentSeason,1.0f);
			switchSeason("winter",nextSeason,0.0f);
			break;
		//Frühling zu Sommer ist am Anfang schon vorhanden
		}
	}

	// Lädt Grafiken in die Hintergrundobjekte und gibt ihnen einen alpha Wert
	void switchSeason(string season, SpriteRenderer[] array, float alpha){
		foreach (SpriteRenderer sp in array) {
			string name = sp.name;
			string fileLocation = basicLocation+season+"_"+name;
			Sprite newSprite = Resources.Load<Sprite>(fileLocation);
			sp.sprite = newSprite;
			Color color = new Color (sp.color.r,sp.color.g,sp.color.b,alpha);
			sp.color = color;
		}
	}

		
}
