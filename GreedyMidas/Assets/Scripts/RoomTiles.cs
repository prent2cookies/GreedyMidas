using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RoomTiles : MonoBehaviour
{
    Animator animator;
    public backend b;
	public MidasTurn m;
	public ApolloTurn a;
    public Turns turns;
	
    public int[] index = new int[2];


    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //initializes animator component
    //controls what tile displays at what location
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //updates animator when variables change
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void ChangeAnimationState(int value)
    {
        animator.SetInteger("AnimState", value);
    }

    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    //Main logic of clicking room tiles
    //Determines what is being clicked on and if the click is viable
    //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
    void Update()
    {
        //Determines if click is viable
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
			if (hit.collider != null && hit.collider.name == name) {
				if(hit.collider.gameObject.tag == "Finish"){
					TurnDefs.Player currentTurn = b.turns.GetCurrentTurn();

                    //Midas Turn
                    if (currentTurn == TurnDefs.Player.ONE){

						//calls purchase function to determine if unowned room can be purchased
						if(b.owned[index[0], index[1]] == 0 && b.completedMove == false){
							if(index[1] < 4 && b.position[index[0],index[1]+1] == 1){
								m.purchase(1, index[0], index[1]);
							}else if(index[1] > 0 && b.position[index[0],index[1]-1] == 1){
								m.purchase(1, index[0], index[1]);
							}else if(index[0] > 0 && b.position[index[0]-1,index[1]] == 1){
								m.purchase(1, index[0], index[1]);
							}else if(index[0] < 4 && b.position[index[0]+1,index[1]] == 1){
								m.purchase(1, index[0], index[1]);
							}

                        //moves in Midas-owned room if turn is not completed
						}else if(b.owned[index[0], index[1]] == 1 && b.completedMove == false){
							if(index[1] < 4 && b.position[index[0],index[1]+1] == 1){
								m.moveLeft();
							}else if(index[1] > 0 && b.position[index[0],index[1]-1] == 1){
								m.moveRight();
							}else if(index[0] > 0 && b.position[index[0]-1,index[1]] == 1){
								m.moveDown();
							}else if(index[0] < 4 && b.position[index[0]+1,index[1]] == 1){
								m.moveUp();
							}
							
						}

                    //Apollo Turn
                    }else if(currentTurn == TurnDefs.Player.TWO){

                        //calls purchase function to determine if unowned room can be purchased
                        if (b.owned[index[0], index[1]] == 0 && b.completedMove == false){					
							if(index[1] < 4 && b.position[index[0],index[1]+1] == 2){
								a.purchase(1, index[0], index[1]);
							}else if(index[1] > 0 && b.position[index[0],index[1]-1] == 2){
								a.purchase(1, index[0], index[1]);
							}else if(index[1] > 0 && b.position[index[0]-1,index[1]] == 2){
								a.purchase(1, index[0], index[1]);
							}else if(index[0] < 4 && b.position[index[0]+1,index[1]] == 2){
								a.purchase(1, index[0], index[1]);
							}

                        //moves in Apollo-owned room if turn is not completed
                        }else if(b.owned[index[0], index[1]] == 2 && b.completedMove == false){
							if(index[1] < 4 && b.position[index[0],index[1]+1] == 2){
								a.moveLeft();
							}else if(index[1] > 0 && b.position[index[0],index[1]-1] == 2){
								a.moveRight();
							}else if(index[0] > 0 && b.position[index[0]-1,index[1]] == 2){
								a.moveDown();
							}else if(index[0] < 4 && b.position[index[0]+1,index[1]] == 2){
								a.moveUp();
							}

                        //moves in Midas-owned room if turn is not completed
                        }else if(b.owned[index[0], index[1]] == 1 && b.completedMove == false){
							if(index[1] < 4 && b.position[index[0],index[1]+1] == 2){
								a.moveLeft();
							}else if(index[1] > 0 && b.position[index[0],index[1]-1] == 2){
								a.moveRight();
							}else if(index[0] > 0 && b.position[index[0]-1,index[1]] == 2){
								a.moveDown();
							}else if(index[0] < 4 && b.position[index[0]+1,index[1]] == 2){
								a.moveUp();
							}
							
						}
					}
				}
			}
		}
		
		
		
		
	    //sets up the correct rooms in the correct locations using the animator
        if (b.owned[index[0], index[1]] == 0)
        {
            switch (b.cards[index[0], index[1]])
            {
                case 1://set up bronze key unowned room
                    ChangeAnimationState(1);
                    break;
                case 2://set up silver key unowned room
                    ChangeAnimationState(2);
                    break;
                case 3://set up lead key unowned room
                    ChangeAnimationState(3);
                    break;
                case 4://set up wrought iron key unowned room
                    ChangeAnimationState(4);
                    break;
                default: //error room (something broke in the game if this state is called)
                    ChangeAnimationState(0);
                    break;

            }
        }
        else if(b.owned[index[0], index[1]] == 1)
        {
            if (b.position[index[0], index[1]] == 1)
            {
                ChangeAnimationState(5); //room that Midas is currently in
            }
            else if (b.position[index[0], index[1]] == 2) {
                ChangeAnimationState(9); //room that Midas owns but Apollo is currently in
            }
            else
            {
                ChangeAnimationState(6); //room that Midas owns but is not currently in
            }
        }
        else
        {
            if (b.position[index[0], index[1]] == 2)
            {
                ChangeAnimationState(7); //room that Apollo owns and is currently in
            }
            else
            {
                ChangeAnimationState(8); //room that Apollo owns but is not currently in
            }
        }
    }
	
	bool CastRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit) {
			return true;
        }
		return false;
	}
}
