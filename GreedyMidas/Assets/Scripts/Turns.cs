using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static backend;

public class Turns : MonoBehaviour
{
    public TurnDefs player;
    public TurnDefs.Player currentPlayer = TurnDefs.Player.ONE;
	public backend b;
	
	void Start(){	
		//b = gameObject.AddComponent<backend>();
	}
	
    void OnGUI()

    {
        CurrentTurn();
    }

    void CurrentTurn() {
        string display;

        if (currentPlayer == TurnDefs.Player.ONE) {
            display = "Player One";
        }
        else
        {
            display =  "Player Two";
        }

        if (GUILayout.Button(display + ": Click to change Player"))
        {
            NextTurn();
        }
    }

    void NextTurn() {
        if (currentPlayer == TurnDefs.Player.ONE)
        {
            currentPlayer = TurnDefs.Player.TWO;
            ApolloTurn.StartTurn();
			
        }


        else
        {
            currentPlayer = TurnDefs.Player.ONE;
            //MidasTurn.StartTurn();
			    b.completedAction = false;
				b.completedMove = false;
				b.canPurchase = false;
				b.purchaseX = -1;
				b.purchaseY = -1;
				b.spot = -1;
				b.said = false;
			
			b.Turn();
			
        }
    }

    public TurnDefs.Player GetCurrentTurn() {
        return currentPlayer;
    }
}
