using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTiles : MonoBehaviour
{
    Animator animator;
    public backend b;
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
            if(b.owned[index[0], index[1]] == 1)
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
            if (b.owned[index[0], index[1]] == 2)
            {
                ChangeAnimationState(7);
            }
            else
            {
                ChangeAnimationState(8);
            }
        }
    }
}
