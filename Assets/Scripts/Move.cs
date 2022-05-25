using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    float xHorizontal, zVertical;
    playerControl playerCs;
    Animator anim;
    Vector3 moveDirection, moveDirection2;
    public bool isMove;
    public Joystick joystick;
    void Start()
    {
        isMove = false;
        playerCs = GameObject.Find("Player").GetComponent<playerControl>();
        anim = GameObject.Find("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        float xVertical = joystick.Vertical;
        float xHorizontal = joystick.Horizontal;
        float zVertical = Input.GetAxis("Vertical");
        float zHorizontal = Input.GetAxis("Horizontal");
        moveDirection2 = new Vector3(zHorizontal, 0f, zVertical);
        moveDirection = new Vector3(xHorizontal, 0f, xVertical);
        if (xHorizontal == 0 && xVertical == 0)
            isMove = false;
        else
            isMove = true;

        anim.SetBool("isMove", isMove);
    }

    void FixedUpdate()
    {
        
        MoveIt();
    }

   void MoveIt()
    {
        transform.Translate(moveDirection * playerCs.speed * Time.deltaTime);
        transform.Translate(moveDirection2 * playerCs.speed * Time.deltaTime);
    }

}
