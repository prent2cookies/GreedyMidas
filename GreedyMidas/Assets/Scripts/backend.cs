using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backend : MonoBehaviour {
    
    //Declaration of main game variables used in future scripts

    //text information for player
	public Text prompt;
	public Text MidasText;
	public Text ApolloText;
	public Text TurnPrompt;

    //Card Inventory Texts
    public Text MidasCard1; //bronze
    public Text MidasCard2; //silver
    public Text MidasCard3; //lead
    public Text MidasCard4; //wrought iron
    public Text MidasCard5; //skeleton

    public Text ApolloCard1; //bronze
    public Text ApolloCard2; //silver
    public Text ApolloCard3; //lead
    public Text ApolloCard4; //wrought iron
    public Text ApolloCard5; //skeleton
   
    //Tile location and ownership
    public int[,] cards = new int[5,5];
	public int[,] owned = new int[5,5];
	public int[,] position = new int[5,5];
	
    //player card inventories
	public int[] apollo = new int[20];
	public int[] midas = new int[20];

    //game logic variables
    public bool completedAction = false;
    public bool completedMove = false;
    public bool canPurchase = false;
    public Turns turns;
	public int purchaseX = -1;
	public int purchaseY = -1;
	public int spot = -1;
	public int[] location;
	public bool said = false;
	int randomNumber;
	public string[] colorText = new string[5] {"Bronze", "Silver", "Lead", "Wrought Iron", "Skeleton"};

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Initialized setup of game variables
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Start () {
        turns = GetComponent<Turns>();
        owned[0,2] = 1; //(midas start)
		owned[4,2] = 2; //(Apollo start)

		position[0,2] = 1; //(midas start)
		position[4,2] = 2; //(Apollo start)
		
		prompt.text = "";		
		MidasText.text = "Midas has no cards.";
		ApolloText.text = "Apollo has no cards.";
		TurnPrompt.text = "Midas Turn";
		
		for(int i=0;i<5;i++)
		{
			for(int j=0;j<5;j++)
			{
				randomNumber = Random.Range(1, 101);
				//40 iron, 30 lead, 20 bronze, 10 silver
				if(randomNumber <= 40){
					cards[i, j] = 4;
				}else if(randomNumber <= 70){
					cards[i, j] = 3;
				}else if(randomNumber <= 90){
					cards[i, j] = 1;
				}else{
					cards[i, j] = 2;
				}
			}
		}
	}

	
}