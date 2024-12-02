using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacle : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            transform.position = new Vector3(-41, 41, 1204);
        }
    }
}
