﻿using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public Sprite slow;
	public Sprite normal;
	public Sprite fast;
	public Sprite jump;

	public int health;
	public int reward;
	public float slow_duration = 0.0f;
	private float speed;
	private float originalSpeed;
	private float finalSpeed = 0.0f;
	private float effectValue = 0.0f;
	private GameObject game;

	public enum enemyType{
		TYPE_NORMAL = 0,
		TYPE_SLOW,
		TYPE_FAST,
		TYPE_JUMP,
		TYPE_MAX
	}
	private enemyType type;

	// Use this for initialization
	void Start () {
		game = GameObject.Find ("Game");
	}
	
	// Update is called once per frame
	void Update () {
		// If health less than zero destroy object
		if (health <= 0) {
			Destroy (gameObject);
			game.GetComponent<Game>().resources += reward;
		}
		if (slow_duration > 0.0f) {
			slow_duration -= Time.deltaTime;
			GetComponent<SpriteRenderer>().color = Color.blue;
			finalSpeed = speed * effectValue;
		} else {
			effectValue = 0.0f;
			//speed = originalSpeed;
			finalSpeed = speed;
			GetComponent<SpriteRenderer>().color = Color.white;
		}
	}

	public float getSpeed(){
		return speed;
	}

	public float getOriginalSpeed() {
		return originalSpeed;
	}

	public float getFinalSpeed(){
		return finalSpeed;
	}

	public void setSpeed(float new_speed) {
		this.speed = new_speed;
	}

	public void setEffectValue(float effectValue){
		this.effectValue = effectValue;
	}

	public void setType(enemyType newType){
		type = newType;

		switch(type){
		case enemyType.TYPE_NORMAL:
			health = 25;
			reward = 5;
			originalSpeed = 1.0f;
			speed = originalSpeed;
			gameObject.name = "Normal";
			GetComponent<SpriteRenderer>().sprite = normal;
			break;

		case enemyType.TYPE_SLOW:
			health = 50;
			reward = 5;
			originalSpeed = 0.5f;
			speed = originalSpeed;
			gameObject.name = "Slow";
			GetComponent<SpriteRenderer>().sprite = slow;
			break;

		case enemyType.TYPE_FAST:
			health = 10;
			reward = 5;
			originalSpeed = 1.5f;
			speed = originalSpeed;
			gameObject.name = "Fast";
			GetComponent<SpriteRenderer>().sprite = fast;
			break;

		case enemyType.TYPE_JUMP:
			health = 35;
			reward = 5;
			originalSpeed = 1.0f;
			speed = originalSpeed;
			gameObject.name = "Jump";
			GetComponent<SpriteRenderer>().sprite = jump;
			break;
		}

		gameObject.tag = "Enemy";
	}

	public enemyType getType(){
		return type;
	}
}
