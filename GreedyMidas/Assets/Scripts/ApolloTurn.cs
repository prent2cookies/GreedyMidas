using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static backend;


public class ApolloTurn : MonoBehaviour
{
	public backend b;
	
	void Update () {
        TurnDefs.Player currentTurn = b.turns.GetCurrentTurn();
        if (Input.GetKeyDown("d") && b.completedAction == false && currentTurn == TurnDefs.Player.TWO)
        { //draw card
            DrawCard();
		}else if(currentTurn == TurnDefs.Player.TWO){
			Turn();
		}
			
			
	}	
		
	//apollo Turn
		//-Draw card
		/**for(int i = 0; i < 10; i++){
			//Radomnize a # 1-5 and put in apollo Array
			int spot = System.Array.IndexOf(apollo, 0);
			apollo[spot]= Random.Range(1, 6);
			Debug.Log("At " + spot + " val = " + apollo[spot]);
		}*/
		
		//-Move
			//Player inputs a direction to move via arrow keys.
			//If b.owned at that b.location is 1, move. If 2, prevent
			//If 0:
				//Optional: Open Door?
				//check if apollo array has the correct # of keys needed.
				//If so, remove keys from inventory
				//update b.owned at that b.location to a 1
	
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
				Debug.Log("Your move is already complete! Next Player's turn");
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
			Debug.Log("Rejected");
			b.completedMove = true;
		}

    }

    void DrawCard() {
        int spot = System.Array.IndexOf(b.apollo, 0);
        b.apollo[spot] = Random.Range(1, 6);
        Debug.Log("At " + spot + " val = " + b.apollo[spot]);
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
		Debug.Log("b.position Map");
		for (int i = 0; i < 5; i++)
			{
				Debug.Log(b.position[i,0] + "\t" + b.position[i,1] + "\t" + b.position[i,2] + "\t" + b.position[i,3] + "\t" + b.position[i,4]);
			}
	}
	
	
	public void printownedMap(){
		Debug.Log("b.owned Map");
		for (int i = 0; i < 5; i++)
			{
				Debug.Log(b.owned[i,0] + "\t" + b.owned[i,1] + "\t" + b.owned[i,2] + "\t" + b.owned[i,3] + "\t" + b.owned[i,4]);
			}
	}
	
	public bool purchase(int player, int x, int y){
		b.spot = System.Array.IndexOf(b.apollo, b.cards[x,y]);
		if(b.spot == -1){
			Debug.Log("Can't Purchase");
		}else{
			Debug.Log("Want to Purchase? y or n.");
            b.canPurchase = true;
		}
		b.purchaseX = x;
		b.purchaseY = y;
		return false;
		
	}

	
	void Purchase()
    {
        Debug.Log("Purchased");
        b.owned[b.purchaseX, b.purchaseY] = 2;
        b.apollo[b.spot] = 0;
        b.canPurchase = false;
        b.position[b.location[0], b.location[1]] = 0;
		b.position[b.purchaseX, b.purchaseY] = 2;
        b.completedMove = true;
        b.completedAction = true;
		b.purchaseX = -1;
		b.purchaseY = -1;
        printownedMap();
    }

	public void moveLeft(){
		b.location = findlocation(2);
		if(b.owned[b.location[0],b.location[1]-1] == 2){
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0],b.location[1]-1] = 2;
			printpositionMap();
			b.completedMove = true;
		}else if(b.owned[b.location[0],b.location[1]-1] == 1){
			Debug.Log("Ahh hell naw.");
			//break;
		}else if (b.owned[b.location[0],b.location[1]-1] == 0){
			Debug.Log("Purchasing");					
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
		if(b.owned[b.location[0],b.location[1]+1] == 2){
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0],b.location[1]+1] = 2;
			printpositionMap();
			b.completedMove = true;
		}else if(b.owned[b.location[0],b.location[1]+1] == 1){
			Debug.Log("Ahh hell naw.");
			//break;
		}else if (b.owned[b.location[0],b.location[1]+1] == 0){
			Debug.Log("Purchasing");
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
		if(b.owned[b.location[0]-1,b.location[1]] == 2){
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0]-1,b.location[1]] = 2;
			printpositionMap();
			b.completedMove = true;
		}else if(b.owned[b.location[0]-1,b.location[1]] == 1){
			Debug.Log("Ahh hell naw.");
			//break;
		}else if (b.owned[b.location[0]-1,b.location[1]] == 0){
			Debug.Log("Purchasing");
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
		if(b.owned[b.location[0]+1,b.location[1]] == 2){
			b.position[b.location[0],b.location[1]] = 0;
			b.position[b.location[0]+1,b.location[1]] = 2;
			printpositionMap();
			b.completedMove = true;
		}else if(b.owned[b.location[0]+1,b.location[1]] == 1){
			Debug.Log("Ahh hell naw.");
			//break;
		}else if (b.owned[b.location[0]+1,b.location[1]] == 0){
			Debug.Log("Purchasing");				
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
		Debug.Log("count: " + count);
		return count;
	}
}
