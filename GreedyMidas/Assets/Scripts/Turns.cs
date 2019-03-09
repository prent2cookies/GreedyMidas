using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using backend;

public class Turns : MonoBehaviour
{
    public TurnDefs player;
    public TurnDefs.Player currentPlayer = TurnDefs.Player.ONE;

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
            MidasTurn.StartTurn();
        }
    }

    public TurnDefs.Player GetCurrentTurn() {
        return currentPlayer;
    }
}
