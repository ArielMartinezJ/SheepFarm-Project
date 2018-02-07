using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour {

    public Vector3 spawnPosition;
    private Vector3 newPosition;
    public Vector3 forward;
    public bool checkpointPassed = false;
    private bool alreadyPassed = false;

    [SerializeField]
    private AudioSource checkpointSound;

    void Awake ()
    {
        spawnPosition = new Vector3(16.9f, 1.08f, 1.19f);
    }

	void Start ()
    {	
	}
	
	void Update ()
    {
        spawnPosition = newPosition;

        /*if (HealthScript.iAmrestarting)
        {
            checkpointPassed = false;
        }*/
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            Debug.Log("Checkpoint passed");
            checkpointPassed = true;
            newPosition = this.gameObject.transform.position;
            forward = this.gameObject.transform.forward;
            if (!alreadyPassed)
            {
                checkpointSound.Play();
            }
            alreadyPassed = true;
        }
    }
}


