using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static backend;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class ApolloTurn : MonoBehaviour
{
	public backend b;
	int randomNumber;

    void Update () {
        TurnDefs.Player currentTurn = b.turns.GetCurrentTurn();
        if (Input.GetKeyDown("d") && b.completedAction == false && currentTurn == TurnDefs.Player.TWO)
        {
            DrawCard();
		}else if(currentTurn == TurnDefs.Player.TWO){
			Turn();
		}		
	}


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Keyboard controls for turn
    //UI button controls outlined in Turns.cs
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void Turn () {
	
		//Apollo Turn
		//-Draw card
        if((Input.GetKeyDown("up")|| Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right")) && b.completedMove == false)
        {
            Move();
			 
        }

        //check for win state after completing turn
        else if(b.completedMove == true && b.completedAction == true)
        {
			if(!b.said){
				checkWin();
				b.said = true;
			}

        }

		if(b.canPurchase == true && Input.GetKeyDown("y") && b.completedMove == false)
			{
				Purchase();
				b.completedMove = true;
			}
		else if(b.canPurchase == true && Input.GetKeyDown("n") && b.completedMove == false)
		{
			b.prompt.text = "Rejected";
			b.completedMove = true;
		}

    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Draws a randomized card
    //Adds drawn card to player inventory
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void DrawCard() {
        int spot = System.Array.IndexOf(b.apollo, 0);
        randomNumber = Random.Range(1, 101);

        //determines what type of card is drawn by the number given
		if(randomNumber <= 35){
			b.apollo[spot] = 4;
		}else if(randomNumber <= 65){
			b.apollo[spot] = 3;
		}else if(randomNumber <= 85){
			b.apollo[spot] = 1;
		}else if(randomNumber <= 95){
			b.apollo[spot] = 2;
		}else{
			b.apollo[spot] = 5;
		}

        //prints key inventory
		b.ApolloText.text = "Apollo's Keys:\n";
		int sum = 0;
		for(int j = 1; j < 6; j++){
			for(int i=0; i < b.apollo.Length; i++){
				if(b.apollo[i] == j){
					sum++;
				}
				
			}
			b.ApolloText.text += sum.ToString() + " " + b.colorText[j-1 ] + "\n";
			sum = 0;
		}
        b.ApolloCard1.text = GetCards(1);
        b.ApolloCard2.text = GetCards(2);
        b.ApolloCard3.text = GetCards(3);
        b.ApolloCard4.text = GetCards(4);
        b.ApolloCard5.text = GetCards(5);
        b.completedAction = true;
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Keyboard-controlled movement
    //Button-controlled movement is outlined in Turns.cs
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Move() {
			if (Input.GetKeyDown("up")){
				moveUp();
			}
			else if (Input.GetKeyDown("down")){
				moveDown();
			}	
			else if (Input.GetKeyDown("left")){
				moveLeft();
			}
			else if (Input.GetKeyDown("right")){
				moveRight();
			}
			
	}

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Gets the current location of a player
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public int[] findlocation(int player){
		int found_i = -1;
		int[] location = new int[2];
		for(int i = 0; i < 5 && found_i < 0; ++i)
		{
			for(int j = 0; j < 5; ++j)
			{
				if( b.position[i,j] == player)
				{
					location[0] = i;
					location[1] = j;
					return location;
				}
			}
		}
		location[0] = 9;
		location[1] = 9;
		return location;
	}

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Prints position of players on map
    //used for debugging
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~		
    public void printpositionMap(){
		b.prompt.text = "";
		for (int i = 0; i < 5; i++)
			{
				b.prompt.text += b.position[i,0] + "\t" + b.position[i,1] + "\t" + b.position[i,2] + "\t" + b.position[i,3] + "\t" + b.position[i,4];
			}
	}

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Prints text of owned and unowned tiles
    //Used for debugging
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void printownedMap(){
		b.prompt.text = "";
		for (int i = 0; i < 5; i++)
			{
				b.prompt.text += b.owned[i,0] + "\t" + b.owned[i,1] + "\t" + b.owned[i,2] + "\t" + b.owned[i,3] + "\t" + b.owned[i,4];
			}
	}

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Attempts purchase of unowned tile
    //Returns bool true for success, false for failure
    //fails when there are insufficient cards for purchase
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public bool purchase(int player, int x, int y){
		b.spot = System.Array.IndexOf(b.apollo, b.cards[x,y]);

        //runs if insufficient cards to purchase or player must use skeleton key to purchase
		if(b.spot == -1){
			b.spot = System.Array.IndexOf(b.apollo, 5);
			if(b.spot == -1){
				b.prompt.text = "Can't Purchase";
			}else{
				b.prompt.text = "Use Skeleton Key To Purchase? y or n.";
				b.canPurchase = true;
			}

        //runs if player can purchase, confirms player choice.
		}else{
			b.prompt.text = "Want to Purchase? y or n.";
            b.canPurchase = true;
		}
		b.purchaseX = x;
		b.purchaseY = y;
		return false;
		
	}

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Updates variables after player confirms a valid purchase
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void Purchase()
    {
		int[] loc = new int[2];
        b.prompt.text = "Purchased";
        b.owned[b.purchaseX, b.purchaseY] = 2;
        b.apollo[b.spot] = 0;
        b.canPurchase = false;
		loc = findlocation(2);
        b.position[loc[0], loc[1]] = 0;
        b.position[b.purchaseX, b.purchaseY] = 2;
        b.completedMove = true;
		b.purchaseX = -1;
		b.purchaseY = -1;
		b.ApolloText.text = "";
		int sum = 0;
		for(int j = 1; j < 6; j++){
			for(int i=0; i < b.apollo.Length; i++){
				if(b.apollo[i] == j){
					sum++;
				}		
			}
			b.ApolloText.text += sum.ToString() + " " + b.colorText[j-1] + "\n";
			sum = 0;
		}
        b.ApolloCard1.text = GetCards(1);
        b.ApolloCard2.text = GetCards(2);
        b.ApolloCard3.text = GetCards(3);
        b.ApolloCard4.text = GetCards(4);
        b.ApolloCard5.text = GetCards(5);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Attempts movement, left direction
    //If not owned, attempts purchase
    //Fails if insufficient cards or invalid location
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void moveLeft(){
		b.location = findlocation(2);
		if(b.owned[b.location[0],b.location[1]-1] == 2 || b.owned[b.location[0],b.location[1]-1] == 1){

            //if Midas and Apollo are in the same room then Midas wins.
            if (b.position[b.location[0],b.location[1]-1] == 1){
				b.prompt.text = "COLLISION - Midas Wins!";
				SceneManager.LoadScene("MidasWins");
				return;
			}

            //adjust player location
            b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0],b.location[1]-1] = 2;
			b.completedMove = true;
		}else if (b.owned[b.location[0],b.location[1]-1] == 0){  //attempts purchase since room is not owned
            b.prompt.text = "Purchasing";					
			bool passed = purchase(1, b.location[0],b.location[1]-1);

            //only runs if purchase was successful, adjusts player location
            if (passed){
				b.position[b.location[0],b.location[1]] = 0;
				b.position[b.location[0],b.location[1]-1] = 2;
				printownedMap();
			}
		}
	}

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Attempts movement, right direction
    //If not owned, attempts purchase
    //Fails if insufficient cards or invalid location
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void moveRight(){
		b.location = findlocation(2);
		if(b.owned[b.location[0],b.location[1]+1] == 2 || b.owned[b.location[0],b.location[1]+1] == 1){

            //if Midas and Apollo are in the same room then Midas wins.
            if (b.position[b.location[0],b.location[1]+1] == 1){
				b.prompt.text = "COLLISION - Midas Wins!";
				SceneManager.LoadScene("MidasWins");
				return;
			}

            //adjust player location
            b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0],b.location[1]+1] = 2;
			b.completedMove = true;
		}else if (b.owned[b.location[0],b.location[1]+1] == 0){  //attempts purchase since room is not owned
            b.prompt.text = "Purchasing";
			bool passed = purchase(1, b.location[0],b.location[1]+1);

            //only runs if purchase was successful, adjusts player location
            if (passed){
				b.position[b.location[0], b.location[1]] = 0;
				b.position[b.location[0], b.location[1]+1] = 2;
				printownedMap();
			}
		}
	}

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Attempts movement, upwards direction
    //If not owned, attempts purchase
    //Fails if insufficient cards or invalid location
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void moveUp(){
		b.location = findlocation(2);
		if(b.owned[b.location[0]-1,b.location[1]] == 2 || b.owned[b.location[0]-1,b.location[1]] == 1){

            //if Midas and Apollo are in the same room then Midas wins.
            if (b.position[b.location[0]-1,b.location[1]] == 1){
				b.prompt.text = "COLLISION - Midas Wins!";
				SceneManager.LoadScene("MidasWins");
				return;
			}

            //adjust player location
            b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0]-1,b.location[1]] = 2;
			b.completedMove = true;
		}else if (b.owned[b.location[0]-1,b.location[1]] == 0){ //attempts purchase since room is not owned
            b.prompt.text = "Purchasing";
			bool passed = purchase(1, b.location[0]-1,b.location[1]);

            //only runs if purchase was successful, adjusts player location
            if (passed){
				b.position[b.location[0],b.location[1]] = 0;
				b.position[b.location[0]-1,b.location[1]] = 2;
				printownedMap();
			}
		}
	}

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Attempts movement, downward direction
    //If not owned, attempts purchase
    //Fails if insufficient cards or invalid location
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public void moveDown(){
		b.location = findlocation(2);

		if(b.owned[b.location[0]+1,b.location[1]] == 2 || b.owned[b.location[0]+1,b.location[1]] == 1){

            //if Midas and Apollo are in the same room then Midas wins.
			if(b.position[b.location[0]+1,b.location[1]] == 1){
				b.prompt.text = "COLLISION - Midas Wins!";
				SceneManager.LoadScene("MidasWins");
				return;
			}

            //adjust player location
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0]+1,b.location[1]] = 2;
			b.completedMove = true;
       	}else if (b.owned[b.location[0]+1,b.location[1]] == 0){ //attempts purchase since room is not owned
			b.prompt.text = "Purchasing";				
			bool passed = purchase(1, b.location[0]+1,b.location[1]);

            //only runs if purchase was successful, adjusts player location
            if (passed){
				b.position[b.location[0],b.location[1]] = 0;
				b.position[b.location[0]+1,b.location[1]] = 2;
				printownedMap();
			}
		}
		
	}

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Checks status of game, checks for Apollo winstate
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public int checkWin(){
		int count = 0;
		int[] location = new int[2];
		for(int i = 0; i < 5; ++i)
		{
			for(int j = 0; j < 5; ++j)
			{
				if( b.owned[i,j] == 2)
				{
					count++; //holds the amount of rooms that Apollo owns
				}
			}
		}
		location[0] = 9;
		location[1] = 9;
		if(count >= 13){
			b.prompt.text = "Apollo wins!";
			SceneManager.LoadScene("ApolloWins");
		}else if(count == 12){
			b.prompt.text = "Apollo is 1 card from winning!";
		}
		return count;
	}


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Returns number value in form of a string
    //Used to display player card inventories
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    public string GetCards(int j)
    {
        int sum = 0;
        for (int i = 0; i < b.apollo.Length; i++)
        {
            if (b.apollo[i] == j)
            {
                sum++;
            }
        }

        return sum.ToString();
    }


}
