using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speed; 
    private RaycastHit2D hit;

    // Update is called once per frame
    void Update ()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
        }

        if ((targetPosition.x - transform.position.x)  > 0)
            transform.localScale = new Vector3(-1,1,1);
        else if ((targetPosition.x - transform.position.x) < 0)
            transform.localScale = new Vector3(1,1,1);
        
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

        Debug.Log(targetPosition - transform.position);
    }
}
