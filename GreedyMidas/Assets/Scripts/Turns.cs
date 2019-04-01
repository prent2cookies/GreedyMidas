using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static backend;

public class Turns : MonoBehaviour
{
    public TurnDefs player;
    public TurnDefs.Player currentPlayer = TurnDefs.Player.ONE;
	public backend b;
	public MidasTurn m;
	public ApolloTurn a;
	
    void OnGUI(){
        CurrentTurn();
    }

    void CurrentTurn() {
        string display;

        if (currentPlayer == TurnDefs.Player.ONE) {
            display = "Player One";
        }
        else{
            display =  "Player Two";
        }

        if (GUILayout.Button(display + ": Click to change Player")){
            NextTurn();
        }
		
		if (GUILayout.Button("Draw a Card")){
            if(currentPlayer == TurnDefs.Player.ONE && b.completedAction == false){
				m.DrawCard();
			}else if(currentPlayer == TurnDefs.Player.TWO && b.completedAction == false){
				a.DrawCard();
			}
        }
		
		if(GUILayout.Button("Purchase: Yes")){
            if(currentPlayer == TurnDefs.Player.ONE && b.canPurchase == true){
				m.Purchase();
			}else if(currentPlayer == TurnDefs.Player.TWO && b.canPurchase == true){
				a.Purchase();
			}
        }else if(GUILayout.Button("Purchase: No")){
			
		}
    }

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
			
			b.TurnPrompt.text = "Midas Turn";
        }
    }

    public TurnDefs.Player GetCurrentTurn() {
        return currentPlayer;
    }
}
