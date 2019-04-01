using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static backend;


public class ApolloTurn : MonoBehaviour
{
	public backend b;
	int randomNumber;
	
	void Update () {
        TurnDefs.Player currentTurn = b.turns.GetCurrentTurn();
        if (Input.GetKeyDown("d") && b.completedAction == false && currentTurn == TurnDefs.Player.TWO)
        { //draw card
            DrawCard();
		}else if(currentTurn == TurnDefs.Player.TWO){
			Turn();
		}
			
			
	}	
	
	public void Turn () {
	
		//Apollo Turn
		//-Draw card
        if((Input.GetKeyDown("up")|| Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right")) && b.completedMove == false)
        {
            Move();
			 
        }
        else if(b.completedMove == true && b.completedAction == true)
        {
			if(!b.said){
				//b.prompt.text = "Your move is already complete! Next Player's turn";
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

    public void DrawCard() {
        int spot = System.Array.IndexOf(b.apollo, 0);
        //b.apollo[spot] = Random.Range(1, 6); temporary
        randomNumber = Random.Range(1, 101);
		//40 iron, 30 lead, 20 bronze, 10 silver
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
		b.spot = System.Array.IndexOf(b.apollo, b.cards[x,y]);
		if(b.spot == -1){
			b.spot = System.Array.IndexOf(b.midas, 5);
			if(b.spot == -1){
				b.prompt.text = "Can't Purchase";
			}else{
				b.prompt.text = "Use Skeleton Key To Purchase? y or n.";
			}
		}else{
			//b.prompt.text = "Want to Purchase? y or n.");
			b.prompt.text = "Want to Purchase? y or n.";
            b.canPurchase = true;
		}
		b.purchaseX = x;
		b.purchaseY = y;
		return false;
		
	}

	
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
        //printownedMap();
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
    }

	public void moveLeft(){
		b.location = findlocation(2);
		if(b.owned[b.location[0],b.location[1]-1] == 2 || b.owned[b.location[0],b.location[1]-1] == 1){
			if(b.owned[b.location[0],b.location[1]-1] == 1){
				b.prompt.text = "COLLISION - Midas Wins!";
				return;
			}
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0],b.location[1]-1] = 2;
			//printpositionMap();
			b.completedMove = true;
		}else if (b.owned[b.location[0],b.location[1]-1] == 0){
			b.prompt.text = "Purchasing";					
			bool passed = purchase(1, b.location[0],b.location[1]-1);
			if(passed){
				//printb.positionMap();
				b.position[b.location[0],b.location[1]] = 0;
				b.position[b.location[0],b.location[1]-1] = 2;
				printownedMap();
			}
		}
	}
	
	public void moveRight(){
		b.location = findlocation(2);
		if(b.owned[b.location[0],b.location[1]+1] == 2 || b.owned[b.location[0],b.location[1]+1] == 1){
			if(b.owned[b.location[0],b.location[1]+1] == 1){
				b.prompt.text = "COLLISION - Midas Wins!";
				return;
			}
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0],b.location[1]+1] = 2;
			//printpositionMap();
			b.completedMove = true;
		}else if (b.owned[b.location[0],b.location[1]+1] == 0){
			b.prompt.text = "Purchasing";
			bool passed = purchase(1, b.location[0],b.location[1]+1);
			if(passed){
				//printb.positionMap();
				b.position[b.location[0], b.location[1]] = 0;
				b.position[b.location[0], b.location[1]+1] = 2;
				printownedMap();
			}
		}
	}
	
	public void moveUp(){
		b.location = findlocation(2);
		if(b.owned[b.location[0]-1,b.location[1]] == 2 || b.owned[b.location[0]-1,b.location[1]] == 1){
			if(b.position[b.location[0]-1,b.location[1]] == 1){
				b.prompt.text = "COLLISION - Midas Wins!";
				return;
			}
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0]-1,b.location[1]] = 2;
			//printpositionMap();
			b.completedMove = true;
		}else if (b.owned[b.location[0]-1,b.location[1]] == 0){
			b.prompt.text = "Purchasing";
			bool passed = purchase(1, b.location[0]-1,b.location[1]);
			if(passed){
				//printb.positionMap();
				b.position[b.location[0],b.location[1]] = 0;
				b.position[b.location[0]-1,b.location[1]] = 2;
				printownedMap();
			}
		}
	}
	
	public void moveDown(){
		b.location = findlocation(2);
		if(b.owned[b.location[0]+1,b.location[1]] == 2 || b.owned[b.location[0]+1,b.location[1]] == 1){
			if(b.owned[b.location[0]+1,b.location[1]] == 1){
				b.prompt.text = "COLLISION - Midas Wins!";
				return;
			}	
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0]+1,b.location[1]] = 2;
			//printpositionMap();
			b.completedMove = true;
		}else if (b.owned[b.location[0]+1,b.location[1]] == 0){
			b.prompt.text = "Purchasing";				
			bool passed = purchase(1, b.location[0]+1,b.location[1]);
			if(passed){
				//printb.positionMap();
				b.position[b.location[0],b.location[1]] = 0;
				b.position[b.location[0]+1,b.location[1]] = 2;
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
				if( b.owned[i,j] == 2) // (or maybe 'object.ReferenceEqual')
				{
					count++;
					//return location;
				}
			}
		}
		location[0] = 9;
		location[1] = 9;
		if(count > 11){
			b.prompt.text = "Apollo is 1 card from winning!";
		}
		return count;
	}
}
