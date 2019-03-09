using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turns : MonoBehaviour
{

    public TurnDefs.Player currentPlayer = TurnDefs.Player.ONE;

    private void OnGUI()
    {
        CurrentTurn();
    }

    private void CurrentTurn()
    {
        string display = (currentPlayer == TurnDefs.Player.ONE) ? "Player One" : "Player Two";
        if (GUILayout.Button(display + ": Click to change Player"))
        {
            NextTurn();
        }
    }

    private void NextTurn()
    {
        if (currentPlayer == TurnDefs.Player.ONE)
        {
                currentPlayer = TurnDefs.Player.TWO;
        }


        else if (currentPlayer == TurnDefs.Player.TWO)
        {
           
                currentPlayer = TurnDefs.Player.ONE;
        }
    }

}
