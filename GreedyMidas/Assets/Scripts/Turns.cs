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
				
        }else{
            currentPlayer = TurnDefs.Player.ONE;
            //MidasTurn.StartTurn();
			
			    b.completedAction = false;
				b.completedMove = false;
				b.canPurchase = false;
				b.purchaseX = -1;
				b.purchaseY = -1;
				b.spot = -1;
				b.said = false;
			
				m.Turn();
			/*
			
				m.completedAction = false;
				m.completedMove = false;
				m.canPurchase = false;
				m.purchaseX = -1;
				m.purchaseY = -1;
				m.spot = -1;
				m.said = false;
			
				m.Turn();
				*/

        }
    }

    public TurnDefs.Player GetCurrentTurn() {
        return currentPlayer;
    }
}
