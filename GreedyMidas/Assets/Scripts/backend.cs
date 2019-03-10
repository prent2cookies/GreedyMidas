using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backend : MonoBehaviour {

	//Could be 1 struct?
	int[,] cards = new int[5,5];
	int[,] owned = new int[5,5];
	int[,] position = new int[5,5];
	
	int[] apollo = new int[20];
	int[] midas = new int[20];
    bool completedAction = false;
    bool completedMove = false;
    bool canPurchase = false;
    Turns turns;

    // Use this for initialization
    void Start () {
        turns = GetComponent<Turns>();
        owned[0,2] = 1; //(midas start)
		owned[4,2] = 2; //(Apollo start)

		position[0,2] = 1; //(midas start)
		position[4,2] = 2; //(Apollo start)
		
		for(int i=0;i<5;i++)
		{
			for(int j=0;j<5;j++)
			{
				cards[i, j]= Random.Range(1, 6);
				//Debug.Log("At " + i + "," + j + " val = " + cards[i,j]);
			}
		}
		
		for (int i = 0; i < 5; i++)
		{
			Debug.Log(position[i,0] + "\t" + position[i,1] + "\t" + position[i,2] + "\t" + position[i,3] + "\t" + position[i,4]);
		}
	}

	void Update () {
        TurnDefs.Player currentTurn = turns.GetCurrentTurn();
        if (Input.GetKeyDown("d") && completedAction == false)
        { //draw card
            DrawCard();
		}
	}	
		
	//Midas Turn
		//-Draw card
		/**for(int i = 0; i < 10; i++){
			//Radomnize a # 1-5 and put in midas Array
			int spot = System.Array.IndexOf(midas, 0);
			midas[spot]= Random.Range(1, 6);
			Debug.Log("At " + spot + " val = " + midas[spot]);
		}*/
		
		//-Move
			//Player inputs a direction to move via arrow keys.
			//If owned at that location is 1, move. If 2, prevent
			//If 0:
				//Optional: Open Door?
				//check if midas array has the correct # of keys needed.
				//If so, remove keys from inventory
				//update owned at that location to a 1
	
	// Update is called once per frame
	public void Turn () {
	
		//Midas Turn
		//-Draw card
        else if((Input.GetKeyDown("up")|| Input.GetKeyDown("down") || Input.GetKeyDown("left") || Input.GetKeyDown("right")) && completedMove == false)
        { //movement
            Move();
        }
        else if(completedMove == true && completedAction == true)
        {
            Debug.Log("Your move is already complete! Next Player's turn");
        }

        if(canPurchase == true && Input.GetKeyDown("y"))
        {
            Purchase();
        }
        else if(canPurchase == true && Input.GetKeyDown("n"))
        {
            Debug.Log("Rejected");
        }

    }

    // Update is called once per frame
    void DrawCard() {
        //Midas Turn
        //-Draw card
        int spot = System.Array.IndexOf(midas, 0);
        midas[spot] = Random.Range(1, 6);
        Debug.Log("At " + spot + " val = " + midas[spot]);
        completedAction = true;
    }

    void Move() {
		//-Move
			//Player inputs a direction to move via arrow keys.
			if (Input.GetKeyDown("up"))
			{
				moveUp();
			}
			else if (Input.GetKeyDown("down"))
			{
				moveDown();
			}	
			else if (Input.GetKeyDown("left"))
			{
				moveLeft();
			}
			
			else if (Input.GetKeyDown("right"))
			{
				moveRight();
			}		
	}	
	
	public int[] findLocation(int player){
		int found_i = -1;
		int[] location = new int[2];
		for(int i = 0; i < 5 && found_i < 0; ++i)
		{
			for(int j = 0; j < 5; ++j)
			{
				if( position[i,j] == player) // (or maybe 'object.ReferenceEqual')
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
	
		
	public void printPositionMap(){
		Debug.Log("Position Map");
		for (int i = 0; i < 5; i++)
			{
				Debug.Log(position[i,0] + "\t" + position[i,1] + "\t" + position[i,2] + "\t" + position[i,3] + "\t" + position[i,4]);
			}
	}
	
	
	public void printOwnedMap(){
		Debug.Log("Owned Map");
		for (int i = 0; i < 5; i++)
			{
				Debug.Log(owned[i,0] + "\t" + owned[i,1] + "\t" + owned[i,2] + "\t" + owned[i,3] + "\t" + owned[i,4]);
			}
	}
	
	public bool purchase(int player, int x, int y){
		//return true if succesfull, false otherwise
		//Open Door?
		int spot = System.Array.IndexOf(midas, cards[x,y]);
		if(spot == -1){
			Debug.Log("Can't Purchase");
		}else{
			Debug.Log("Want to Purchase? y or n.");
            canPurchase = true;
		}
		
		return false;
		
	}
	
	//IEnumerator YourCoroutineName2(){
	//		Turn();
	//		yield return true;
		
	//}
	
	void Purchase()
    {
        Debug.Log("Purchased");
        owned[x, y] = 1;
        midas[spot] = 0;
        canPurchase = false;
        position[location[0], location[1]] = 0;
        position[location[0] + 1, location[1]] = 1;
        completedMove = true;
        completedAction = true;
        printOwnedMap();
    }
	/*
	IEnumerator YourCoroutineName(int player, int x, int y, int spot){
		// Your code that you want here 
		// In this case is the for function
		while ( !Input.GetKeyDown("y") && !Input.GetKeyDown("n"))
                 yield return null;
			 
		if (Input.GetKeyDown("y")){
			Debug.Log("Purchased");
			owned[x, y] = 1;
			midas[spot] = 0;
			yield return true;
		}else if(Input.GetKeyDown("n")){
			Debug.Log("Rejected");
			yield return false;
		}
	}
	*/
	
	public void moveLeft(){
		int[] location = findLocation(1);
		if(owned[location[0],location[1]-1] == 1){
			position[location[0],location[1]] = 0;
			position[location[0],location[1]-1] = 1;
			printPositionMap();
		}else if(owned[location[0],location[1]-1] == 2){
			Debug.Log("Ahh hell naw.");
			//break;
		}else if (owned[location[0],location[1]-1] == 0){
			Debug.Log("Purchasing");					
			bool passed = purchase(1, location[0],location[1]-1);
			if(passed){
				//printPositionMap();
				position[location[0],location[1]] = 0;
				position[location[0],location[1]-1] = 1;
				printOwnedMap();
			}
		}
	}
	
	public void moveRight(){
		int[] location = findLocation(1);
		if(owned[location[0],location[1]+1] == 1){
			position[location[0],location[1]] = 0;
			position[location[0],location[1]+1] = 1;
			printPositionMap();
		}else if(owned[location[0],location[1]+1] == 2){
			Debug.Log("Ahh hell naw.");
			//break;
		}else if (owned[location[0],location[1]+1] == 0){
			Debug.Log("Purchasing");
			bool passed = purchase(1, location[0],location[1]+1);
			Debug.Log(passed);
			if(passed){
				//printPositionMap();
				position[location[0], location[1]] = 0;
				position[location[0], location[1]+1] = 1;
				printOwnedMap();
			}
		}
	}
	
	public void moveUp(){
		int[] location = findLocation(1);
		if(owned[location[0]-1,location[1]] == 1){
			position[location[0],location[1]] = 0;
			position[location[0]-1,location[1]] = 1;
			printPositionMap();
		}else if(owned[location[0]-1,location[1]] == 2){
			Debug.Log("Ahh hell naw.");
			//break;
		}else if (owned[location[0]-1,location[1]] == 0){
			Debug.Log("Purchasing");
			bool passed = purchase(1, location[0]-1,location[1]);
			if(passed){
				//printPositionMap();
				position[location[0],location[1]] = 0;
				position[location[0]-1,location[1]] = 1;
				printOwnedMap();
			}
		}
	}
	
	public void moveDown(){
		int[] location = findLocation(1);
		if(owned[location[0]+1,location[1]] == 1){
			position[location[0],location[1]] = 0;
			position[location[0]+1,location[1]] = 1;
			printPositionMap();
		}else if(owned[location[0]+1,location[1]] == 2){
			Debug.Log("Ahh hell naw.");
			//break;
		}else if (owned[location[0]+1,location[1]] == 0){
			Debug.Log("Purchasing");				
			bool passed = purchase(1, location[0]+1,location[1]);
			if(passed){
				//printPositionMap();
				position[location[0],location[1]] = 0;
				position[location[0]+1,location[1]] = 1;
				printOwnedMap();
			}
		}
		
	}
	}
/**		
Apollo Turn
-Draw card
	Radomnize a # 1-5 and put in apollo Array
-Move
	Player inputs a direction to move via arrow keys.
	If owned at that location is 2, move. If 1, prevent
	If 0:
		Optional: Open Door?
		check if midas array has the correct # of keys needed.
		If so, remove keys from inventory
		update owned at that location to a 1
*/

}
