using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheepBehaviour : MonoBehaviour {

    int sheerState = 5;
    public GameObject[] meshes;
    int actualMesh = 0;
    public ResourceManager ResourceManagerScript;
    public GameObject limit1;
    public GameObject limit2;
    public int patrolPointsNumber;
    Vector3[] patrolPoints;
    int currentPatrol = 0;
    public NavMeshAgent sheep;
    public ResourceManager manager;

    public AudioSource sheepSound;
    public AudioSource[] sheepBleatSounds;
    public bool dragable;

    void Start () {
        patrolPoints = new Vector3[patrolPointsNumber];
        CreatePatrolRoute();
        sheep.destination = patrolPoints[currentPatrol];
        manager = GameObject.FindGameObjectWithTag("Player").GetComponent<ResourceManager>();
    }
	

	void Update () {

        if ((sheep.transform.position - patrolPoints[currentPatrol]).magnitude < 1)
        {
            if (currentPatrol + 1 >= patrolPoints.Length)
            {
                currentPatrol = 0;
            }
            else {
                currentPatrol++;
            }
            sheep.destination = patrolPoints[currentPatrol];
        }
       
	}

    private void OnMouseDown()
    {
        if (manager.currentState == ResourceManager.State.CUT)
        {
            if (sheerState > 1)
            {
                ResourceManagerScript.SumLlana(10);
                sheerState -= 1;
                meshes[actualMesh].SetActive(false);
                meshes[actualMesh + 1].SetActive(true);
                actualMesh += 1;
                //sheepSound.PlayOneShot(sheepBleatSounds[Random.Range(0, 3)]);
            }
            else {
                dragable = true;
                
            }
        }
        else {
            ResourceManagerScript.SumLlana(-50);
        }
        
    }

    private void OnMouseDrag()
    {
        if (dragable && manager.currentState == ResourceManager.State.DRAG)
        {
            
            //manager.cursor.GetComponent<Cursos>().HandClosed();
            sheep.enabled = false;
            this.transform.parent = null;
            this.transform.parent = manager.cursor.transform;
            this.transform.localPosition = Vector3.zero;
        }
    }
    private void OnMouseUp()
    {
        if (manager.currentState == ResourceManager.State.DRAG)
        {

        
        this.gameObject.GetComponent<SphereCollider>().enabled = false;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.tag == "TRUCK")
                {
                    ResourceManagerScript.SumLlana(1000);
                    this.gameObject.SetActive(false);
                    // manager.cursor.GetComponent<Cursos>().HandOpen();
                }
                else
                {
                    Debug.Log(hit.collider.gameObject.name);
                    ResourceManagerScript.SumLlana(-1000);
                    sheep.enabled = true;
                    this.transform.parent = null;
                    //manager.cursor.GetComponent<Cursos>().HandOpen();
                }

            }   
        }
    
  
    }

    void CreatePatrolRoute()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = new Vector3(Random.RandomRange(limit1.transform.position.x, limit2.transform.position.x), limit2.transform.position.y, Random.RandomRange(limit1.transform.position.z, limit2.transform.position.z));
        }
    }


}
