using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
//Resets the variables of the game to the initialized state
//ie, Reloads the scene
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
public class Restart : MonoBehaviour
{
	
	public bool isRestart;
	public bool isQuit;
		
	public void PlayGame() {
		if (isQuit) {
			Application.Quit();
		} if(isRestart) {
			SceneManager.LoadScene("MainScene");			
			GetComponent<Renderer>().material.color=Color.cyan;
		}
	}
}
