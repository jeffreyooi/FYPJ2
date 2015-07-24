﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Core : MonoBehaviour {

	int health;
	private GameObject game;
	private GameObject player;
	public Text healthValue;
	public ParticleSystem explosionEffect;

	// Use this for initialization
	void Start () {
		health = 10;
		healthValue.text = "Health: " + health;
		game = GameObject.Find ("Game");
		if (Application.loadedLevelName == "Game")
			player = GameObject.Find ("Player");
		else if (Application.loadedLevelName == "Multiplayer") {
//			if (gameObject.tag == "Player 1")
//				player = GameObject.Find ("Player 1");
//			else if (gameObject.tag == "Player 2")
//				player = GameObject.Find ("Player 2");
			player = GameObject.Find (gameObject.tag);
		}
	}
	
	// Update is called once per frame
	void Update () {
		healthValue.text = "Health: " + health;
		if (health <= 0) {
			ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity) as ParticleSystem;
			Destroy (explosion.gameObject, explosion.startLifetime);
			Destroy (gameObject);
			Time.timeScale = 0.5f;
			//GameObject lose = GameObject.Find ("GameOver");
			//lose.GetComponent<Text>().enabled = true;
			if (!player.GetComponent<Player1>().losegame) {
				player.GetComponent<Player1>().losegame = true;
				player.GetComponent<Player1>().EnableLosingScreen();
			}
		}
	}

	public int CurrentHealth() {
		return health;
	}

	public void DecreaseHealth() {
		if (CurrentHealth () > 0)
			health -= 1;
	}

	void OnTriggerEnter2D(Collider2D other) {
		// Check if collides with enemy
		if (other.gameObject.tag == "Enemy" || other.GetComponent<Enemy>()) {
			DecreaseHealth ();
			player.GetComponent<Player1>().enemyLeft --;
			Destroy (other.gameObject);
		} 
	}
}
