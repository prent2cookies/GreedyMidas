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
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void ChangeAnimationState(int value)
    {
        animator.SetInteger("AnimState", value);
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection (ray, Mathf.Infinity);
			if (hit.collider != null && hit.collider.name == name) {
				if(hit.collider.gameObject.tag == "Finish"){
					TurnDefs.Player currentTurn = b.turns.GetCurrentTurn();
					if(currentTurn == TurnDefs.Player.ONE){
						
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
						}else if(b.owned[index[0], index[1]] == 1 && b.completedMove == false){
							if(index[1] < 4 && b.position[index[0],index[1]+1] == 1){
								//m.purchase(1, index[0], index[1]);
								m.moveLeft();
							}else if(index[1] > 0 && b.position[index[0],index[1]-1] == 1){
								//m.purchase(1, index[0], index[1]);
								m.moveRight();
							}else if(index[0] > 0 && b.position[index[0]-1,index[1]] == 1){
								//m.purchase(1, index[0], index[1]);
								m.moveDown();
							}else if(index[0] < 4 && b.position[index[0]+1,index[1]] == 1){
								//m.purchase(1, index[0], index[1]);
								m.moveUp();
							}
							
						}

					}else if(currentTurn == TurnDefs.Player.TWO){	
					
						if(b.owned[index[0], index[1]] == 0 && b.completedMove == false){					
							if(index[1] < 4 && b.position[index[0],index[1]+1] == 2){
								a.purchase(1, index[0], index[1]);
							}else if(index[1] > 0 && b.position[index[0],index[1]-1] == 2){
								a.purchase(1, index[0], index[1]);
							}else if(index[1] > 0 && b.position[index[0]-1,index[1]] == 2){
								a.purchase(1, index[0], index[1]);
							}else if(index[0] < 4 && b.position[index[0]+1,index[1]] == 2){
								a.purchase(1, index[0], index[1]);
							}
						}else if(b.owned[index[0], index[1]] == 2 && b.completedMove == false){
							if(index[1] < 4 && b.position[index[0],index[1]+1] == 2){
								//m.purchase(1, index[0], index[1]);
								a.moveLeft();
							}else if(index[1] > 0 && b.position[index[0],index[1]-1] == 2){
								//m.purchase(1, index[0], index[1]);
								a.moveRight();
							}else if(index[0] > 0 && b.position[index[0]-1,index[1]] == 2){
								//m.purchase(1, index[0], index[1]);
								a.moveDown();
							}else if(index[0] < 4 && b.position[index[0]+1,index[1]] == 2){
								//m.purchase(1, index[0], index[1]);
								a.moveUp();
							}
							
						}
					}
				}
			}
		}
		
		
		
		
	
        if (b.owned[index[0], index[1]] == 0)
        {
            switch (b.cards[index[0], index[1]])
            {
                case 1://set up room 1
                    ChangeAnimationState(1);
                    break;
                case 2://set up room 2
                    ChangeAnimationState(2);
                    break;
                case 3://set up room 3
                    ChangeAnimationState(3);
                    break;
                case 4://set up room 4
                    ChangeAnimationState(4);
                    break;
                default:
                    ChangeAnimationState(0);
                    break;

            }
        }
        else if(b.owned[index[0], index[1]] == 1)
        {
            if(b.position[index[0], index[1]] == 1)
            {
                ChangeAnimationState(5);
            }
            else
            {
                ChangeAnimationState(6);
            }
        }
        else
        {
            if (b.position[index[0], index[1]] == 2)
            {
                ChangeAnimationState(7);
            }
            else
            {
                ChangeAnimationState(8);
            }
        }
    }
	
	bool CastRay() {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, Mathf.Infinity);
		if (hit) {
			return true;
            //Debug.Log (hit.collider.gameObject.name);
        }
		return false;
	}
}
