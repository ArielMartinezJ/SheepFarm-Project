using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfBehaviour : MonoBehaviour {
   
    public ResourceManager ResourceManagerScript;
    public GameObject limit1;
    public GameObject limit2;
    public int patrolPointsNumber;
    Vector3[] patrolPoints;
    int currentPatrol = 0;
    public UnityEngine.AI.NavMeshAgent wolf;
    public ResourceManager manager;
    public float eatTime;
    public float elapsedTime;
    GameObject target;
    public bool chasing = false;
    public enum State {WANDERING, HUNTING, REACHING}
    public State currentState = State.WANDERING;
    public GameObject[] Meshes;
    // Use this for initialization
    void Start()
    {
       patrolPoints = new Vector3[patrolPointsNumber];
        CreatePatrolRoute();
        wolf.destination = patrolPoints[currentPatrol];
        manager = GameObject.FindGameObjectWithTag("Player").GetComponent<ResourceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {

            case State.WANDERING:

                if ((wolf.transform.position - patrolPoints[currentPatrol]).magnitude < 1)
                {
                    if (currentPatrol + 1 >= patrolPoints.Length)
                    {
                        currentPatrol = 0;
                    }
                    else
                    {
                        currentPatrol++;
                    }
                    wolf.destination = patrolPoints[currentPatrol];
                }


                break;

            case State.HUNTING:

                if (elapsedTime >= eatTime)
                {
                    target = FindInstanceWithinRadius(gameObject, "sheep", 5.0f);
                    if (target != null)
                    {   
                        ChangeState(State.REACHING);
                    }
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                    if ((wolf.transform.position - patrolPoints[currentPatrol]).magnitude < 1)
                    {
                        if (currentPatrol + 1 >= patrolPoints.Length)
                        {
                            currentPatrol = 0;
                        }
                        else
                        {
                            currentPatrol++;
                        }
                        wolf.destination = patrolPoints[currentPatrol];
                    }
                }
                

                break;

            case State.REACHING:
                if (target != null)
                {
                    if ((wolf.transform.position - target.transform.position).magnitude < 2.5f)
                    {
                        ChangeState(State.HUNTING);
                    }
                }
                else {
                    ChangeState(State.HUNTING);
                }
                break;

        }
    }

    private void ChangeState(State newState)
    {
        switch (currentState) {
            case State.WANDERING:
                Meshes[0].SetActive(false);
                break;
            case State.HUNTING:
                wolf.destination = target.transform.position;
                elapsedTime = 0.0f;
                break;
            case State.REACHING:
                target.SetActive(false);
                wolf.destination = patrolPoints[currentPatrol];
                break;
        }

        switch (newState) {
            case State.WANDERING:

                break;
            case State.HUNTING:
                wolf.speed = 4.0f;
                wolf.angularSpeed = 400.0f;
                Meshes[1].SetActive(true);
                break;
            case State.REACHING:

                break;

        }

        currentState = newState;
    }

    private void OnMouseDown()
    {

        if (!manager.canCut)
        {
            this.gameObject.SetActive(false);
        }
        else {
            ChangeState(State.HUNTING);
        }
    }

    void CreatePatrolRoute()
    {
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            patrolPoints[i] = new Vector3(Random.RandomRange(limit1.transform.position.x, limit2.transform.position.x), limit2.transform.position.y, Random.RandomRange(limit1.transform.position.z, limit2.transform.position.z));
        }
    }
    public  GameObject FindInstanceWithinRadius(GameObject me, string tag, float radius)
    {

        GameObject[] targets = GameObject.FindGameObjectsWithTag(tag);
        if (targets.Length == 0) return null;

        float dist = 0;
        GameObject closest = targets[0];
        float minDistance = (closest.transform.position - me.transform.position).magnitude;

        for (int i = 1; i < targets.Length; i++)
        {
            dist = (targets[i].transform.position - me.transform.position).magnitude;
            if (dist < minDistance)
            {
                minDistance = dist;
                closest = targets[i];
            }
        }
        if (minDistance < radius) return closest;
        else return null;
    }
}
