using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class backend : MonoBehaviour {

	public Text board;
	//Could be 1 struct?
	public int[,] cards = new int[5,5];
	public int[,] owned = new int[5,5];
	public int[,] position = new int[5,5];
	
	public int[] apollo = new int[20];
	public int[] midas = new int[20];
    public bool completedAction = false;
    public bool completedMove = false;
    public bool canPurchase = false;
    public Turns turns;

	public int purchaseX = -1;
	public int purchaseY = -1;
	public int spot = -1;
	public int[] location;
	public bool said = false;
	
    // Use this for initialization
    void Start () {
        turns = GetComponent<Turns>();
        owned[0,2] = 1; //(midas start)
		owned[4,2] = 2; //(Apollo start)

		position[0,2] = 1; //(midas start)
		position[4,2] = 2; //(Apollo start)
		
		board.text = "";
		
		for(int i=0;i<5;i++)
		{
			for(int j=0;j<5;j++)
			{
				cards[i, j]= Random.Range(1, 6);
				board.text += cards[i,j];
				if(j == 4){
						board.text += "\n";
				}
				//Debug.Log("At " + i + "," + j + " val = " + cards[i,j]);
			}
		}
		
		for (int i = 0; i < 5; i++)
		{
			Debug.Log(position[i,0] + "\t" + position[i,1] + "\t" + position[i,2] + "\t" + position[i,3] + "\t" + position[i,4]);
		}
	}

	
}