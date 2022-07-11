using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public Vector3 targetPosition;
    public float speed; 

    // Update is called once per frame
    void Update () {
 
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0;
        }
 
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
        Debug.Log(transform.position);
    }
}
