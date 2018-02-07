using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : MonoBehaviour {

    float rotationSpeed;
    public float movementSpeed;
     float rotationTime;
   
    void Start()
    {
        rotationTime = Random.Range(2, 5);
        rotationSpeed = Random.Range(0, 360);
        ChangeRotation();
        Invoke("ChangeRotation", rotationTime);
      
     
    }

    void ChangeRotation()
    {

        if (Random.value > 0.5f)
        {
            rotationSpeed = -rotationSpeed;

        }
        transform.Rotate(new Vector3(0, rotationSpeed, 0));
        Invoke("ChangeRotation", rotationTime);
    }

    void Update()
    {

        
        transform.position += transform.forward * movementSpeed * Time.deltaTime;
        

    }
}
