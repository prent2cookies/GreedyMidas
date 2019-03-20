using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static backend;


public class MidasTurn : MonoBehaviour
{
	public backend b;
	
	void Update () {
        TurnDefs.Player currentTurn = b.turns.GetCurrentTurn();
        if (Input.GetKeyDown("d") && b.completedAction == false && currentTurn == TurnDefs.Player.ONE)
        { //draw card
            DrawCard();
		}else if(currentTurn == TurnDefs.Player.ONE) {
			Turn();
		}
			
			
	}	
		
	//Midas Turn
		//-Draw card
		/*for(int i = 0; i < 10; i++){
			//Radomnize a # 1-5 and put in midas Array
			int spot = System.Array.IndexOf(midas, 0);
			midas[spot]= Random.Range(1, 6);
			b.prompt.text = "At " + spot + " val = " + midas[spot]);
		}*/
		
		//-Move
			//Player inputs a direction to move via arrow keys.
			//If b.owned at that b.location is 1, move. If 2, prevent
			//If 0:
				//Optional: Open Door?
				//check if midas array has the correct # of keys needed.
				//If so, remove keys from inventory
				//update b.owned at that b.location to a 1
	
	public void Turn () {
	
		//Midas Turn
		//-Draw card
        if((Input.GetKeyDown("up")|| Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right")) && b.completedMove == false)
        {
            Move();
			 
        }
        else if(b.completedMove == true && b.completedAction == true)
        {
			if(!b.said){
				b.prompt.text = "Your move is already complete! Next Player's turn";
				b.said = true;
			}

        }

       if(b.canPurchase == true && Input.GetKeyDown("y"))
			{
				Purchase();
				b.completedMove = true;
			}
		else if(b.canPurchase == true && Input.GetKeyDown("n"))
		{
			b.prompt.text = "Rejected";
			b.completedMove = true;
		}

    }

    void DrawCard() {
        int spot = System.Array.IndexOf(b.midas, 0);
        b.midas[spot] = Random.Range(1, 6);
        //b.prompt.text = "At " + spot + " val = " + b.midas[spot]);
		b.MidasText.text = "";
		for(int i=0; i < b.midas.Length; i++){
			if(b.midas[i] != 0){
				b.MidasText.text += b.midas[i].ToString() + ", ";
			}
		}
        b.completedAction = true;
    }

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
	
	
	public int[] findlocation(int player){
		int found_i = -1;
		int[] location = new int[2];
		for(int i = 0; i < 5 && found_i < 0; ++i)
		{
			for(int j = 0; j < 5; ++j)
			{
				if( b.position[i,j] == player) // (or maybe 'object.ReferenceEqual')
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
	
		
	public void printpositionMap(){
		//b.prompt.text = "b.position Map";
		b.prompt.text = "";
		for (int i = 0; i < 5; i++)
			{
				b.prompt.text += b.position[i,0] + "\t" + b.position[i,1] + "\t" + b.position[i,2] + "\t" + b.position[i,3] + "\t" + b.position[i,4];
			}
	}
	
	
	public void printownedMap(){
		//b.prompt.text = "b.owned Map";
		b.prompt.text = "";
		for (int i = 0; i < 5; i++)
			{
				b.prompt.text += b.owned[i,0] + "\t" + b.owned[i,1] + "\t" + b.owned[i,2] + "\t" + b.owned[i,3] + "\t" + b.owned[i,4];
			}
	}
	
	public bool purchase(int player, int x, int y){
		b.spot = System.Array.IndexOf(b.midas, b.cards[x,y]);
		if(b.spot == -1){
			b.prompt.text = "Can't Purchase";
		}else{
			b.prompt.text = "Want to Purchase? y or n.";
            b.canPurchase = true;
		}
		b.purchaseX = x;
		b.purchaseY = y;
		return false;
		
	}

	
	void Purchase()
    {
        b.prompt.text = "Purchased";
        b.owned[b.purchaseX, b.purchaseY] = 1;
        b.midas[b.spot] = 0;
        b.canPurchase = false;
        b.position[b.location[0], b.location[1]] = 0;
        b.position[b.purchaseX, b.purchaseY] = 1;
        b.completedMove = true;
        b.completedAction = true;
		b.purchaseX = -1;
		b.purchaseY = -1;
        printownedMap();
		b.MidasText.text = "";
		for(int i=0; i < b.midas.Length; i++){
			if(b.midas[i] != 0){
				b.MidasText.text += b.midas[i].ToString() + ", ";
			}
		}
    }

	public void moveLeft(){
		b.location = findlocation(1);
		
		if(b.owned[b.location[0],b.location[1]-1] == 1){
			if(b.position[b.location[0],b.location[1]-1] == 2){
				b.prompt.text = "COLLISION - Midas Wins!";
				return;
			}
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0],b.location[1]-1] = 1;
			printpositionMap();
			b.completedMove = true;
		}else if(b.owned[b.location[0],b.location[1]-1] == 2){
			b.prompt.text = "Claimed by an Enemy.";
			//break;
		}else if (b.owned[b.location[0],b.location[1]-1] == 0){
			b.prompt.text = "Purchasing";					
			bool passed = purchase(1, b.location[0],b.location[1]-1);
			if(passed){
				//printb.positionMap();
				b.position[b.location[0],b.location[1]] = 0;
				b.position[b.location[0],b.location[1]-1] = 1;
				printownedMap();
			}
		}
	}
	
	public void moveRight(){
		b.location = findlocation(1);
		
		if(b.owned[b.location[0],b.location[1]+1] == 1){
			if(b.position[b.location[0],b.location[1]+1] == 2){
				b.prompt.text = "COLLISION - Midas Wins!";
				return;
			}
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0],b.location[1]+1] = 1;
			printpositionMap();
			b.completedMove = true;
		}else if(b.owned[b.location[0],b.location[1]+1] == 2){
			b.prompt.text = "Claimed by an Enemy.";
			//break;
		}else if (b.owned[b.location[0],b.location[1]+1] == 0){
			b.prompt.text = "Purchasing";
			bool passed = purchase(1, b.location[0],b.location[1]+1);
			if(passed){
				//printb.positionMap();
				b.position[b.location[0], b.location[1]] = 0;
				b.position[b.location[0], b.location[1]+1] = 1;
				printownedMap();
			}
		}
	}
	
	public void moveUp(){
		b.location = findlocation(1);

		if(b.owned[b.location[0]-1,b.location[1]] == 1){
			if(b.position[b.location[0]-1,b.location[1]] == 2){
				b.prompt.text = "COLLISION - Midas Wins!";
				return;
			}
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0]-1,b.location[1]] = 1;
			printpositionMap();
			b.completedMove = true;
		}else if(b.owned[b.location[0]-1,b.location[1]] == 2){
			b.prompt.text = "Claimed by an Enemy.";
			//break;
		}else if (b.owned[b.location[0]-1,b.location[1]] == 0){
			b.prompt.text = "Purchasing";
			bool passed = purchase(1, b.location[0]-1,b.location[1]);
			if(passed){
				//printb.positionMap();
				b.position[b.location[0],b.location[1]] = 0;
				b.position[b.location[0]-1,b.location[1]] = 1;
				printownedMap();
			}
		}
	}
	
	public void moveDown(){
		b.location = findlocation(1);
		if(b.owned[b.location[0]+1,b.location[1]] == 1){
			if(b.position[b.location[0]+1,b.location[1]] == 2){
				b.prompt.text = "COLLISION - Midas Wins!";
				return;
			}
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0]+1,b.location[1]] = 1;
			printpositionMap();
			b.completedMove = true;
		}else if(b.owned[b.location[0]+1,b.location[1]] == 2){
			b.prompt.text = "Claimed by an Enemy.";
			//break;
		}else if (b.owned[b.location[0]+1,b.location[1]] == 0){
			b.prompt.text = "Purchasing";				
			bool passed = purchase(1, b.location[0]+1,b.location[1]);
			if(passed){
				//printb.positionMap();
				b.position[b.location[0],b.location[1]] = 0;
				b.position[b.location[0]+1,b.location[1]] = 1;
				printownedMap();
			}
		}
		
	}
	
	public int checkWin(){
		int count = 0;
		int[] location = new int[2];
		for(int i = 0; i < 5; ++i)
		{
			for(int j = 0; j < 5; ++j)
			{
				if( b.owned[i,j] == 1) // (or maybe 'object.ReferenceEqual')
				{
					count++;
					//return location;
				}
			}
		}
		location[0] = 9;
		location[1] = 9;
		if(count > 11){
			b.prompt.text = "Midas is 1 card from winning!";
		}
		return count;
	}
}
