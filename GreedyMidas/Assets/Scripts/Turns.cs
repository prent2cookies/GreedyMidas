using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static backend;

public class Turns : MonoBehaviour
{
    //stores current player, general player logic
    public TurnDefs player;
    public TurnDefs.Player currentPlayer = TurnDefs.Player.ONE;

    //connects Midas and Apollo scripts, backend script
	public backend b;
	public MidasTurn m;
	public ApolloTurn a;

    //toggled tutorial page
    public GameObject Panel;
    public GameObject Instructions;
    public string labelText = "Play!";

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Calls CurrentTurn to set up UI buttons
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void OnGUI(){
        CurrentTurn();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Sets up the UI buttons and logic on the main screen
    //Checks for turn completion
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void CurrentTurn() {
        string display; //text on main screen indicating what player is currently going

        //indicates that we are in game (turns to continue/play if on instructions screen)
        if (labelText == "Help")
        {
            if (currentPlayer == TurnDefs.Player.ONE)
            {
                display = "Midas Player (yellow)";
            }
            else
            {
                display = "Apollo Player (blue)";
            }

            if (GUILayout.Button(display + ": Click to change Player"))
            {
                NextTurn(); //after completing turn, resets necessary variables and checks for winstate. Switches turns.
            }

            if (GUILayout.Button("Draw a Card"))
            {
                if (currentPlayer == TurnDefs.Player.ONE && b.completedAction == false)
                {
                    m.DrawCard(); //logic specific to MidasTurn script. Randomly draws a card.
                }
                else if (currentPlayer == TurnDefs.Player.TWO && b.completedAction == false)
                {
                    a.DrawCard(); //logic specific to ApolloTurn script. Randomly draws a card.
                }
            }

            if (GUILayout.Button("Purchase: Yes"))
            {
                if (currentPlayer == TurnDefs.Player.ONE && b.canPurchase == true)
                {
                    m.Purchase(); //logic specific to MidasTurn script. Completes room purchase.
                }
                else if (currentPlayer == TurnDefs.Player.TWO && b.canPurchase == true)
                {
                    a.Purchase(); //logic specific to ApolloTurn script. Completes room purchase.
                }
            }
            else if (GUILayout.Button("Purchase: No")) //purchase is not possible with cards player has, or room is already owned
            {
                b.prompt.text = "Rejected";
                b.completedMove = true;
            }
        }

        //logic for entering and exiting the instructions screen
        if (GUILayout.Button(labelText))
        {
            if(labelText == "Help") {
                Instructions.gameObject.SetActive(true);
                Panel.gameObject.SetActive(true);
                labelText = "Continue";
            }
            else
            {
                Instructions.gameObject.SetActive(false);
                Panel.gameObject.SetActive(false);
                labelText = "Help";
            }
        }

        //quits application
        if (GUILayout.Button("Exit"))
        {
            Application.Quit();
        }
    }


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Runs after completing turn
    //Resets variables, checks win state, transfers control to other player
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void NextTurn() {
        if (currentPlayer == TurnDefs.Player.ONE){
            currentPlayer = TurnDefs.Player.TWO;
		
			b.completedAction = false;
			b.completedMove = false;
			b.canPurchase = false;
			b.purchaseX = -1;
			b.purchaseY = -1;
			b.spot = -1;
			b.said = false;
			
			a.Turn();
			if(m.checkWin() >= 13){
				Debug.Log("Midas Wins!");
				return;
			}
			if(a.checkWin() >= 13){
				Debug.Log("Apollo Wins!");
				return;
			}
			
			b.TurnPrompt.text = "Apollo Turn";
        }else{
            currentPlayer = TurnDefs.Player.ONE;
			b.completedAction = false;
			b.completedMove = false;
			b.canPurchase = false;
			b.purchaseX = -1;
			b.purchaseY = -1;
			b.spot = -1;
			b.said = false;
		
			m.Turn();
			if(m.checkWin() >= 13){
				Debug.Log("Midas Wins!");
				return;
			}
			if(a.checkWin() >= 13){
				Debug.Log("Apollo Wins!");
				return;
			}
			
			b.TurnPrompt.text = "Midas Turn";
        }
    }


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Gets player currently using their turn
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public TurnDefs.Player GetCurrentTurn() {
        return currentPlayer;
    }
}
