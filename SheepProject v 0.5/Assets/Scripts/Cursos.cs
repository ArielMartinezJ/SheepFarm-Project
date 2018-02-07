using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursos : MonoBehaviour {
    public GameObject shoot;
    public GameObject scissors;
    public ResourceManager manager;
    public GameObject handOpen;
    public GameObject handClose;

	// Use this for initialization
	void Start () {
        manager = GameObject.FindGameObjectWithTag("Player").GetComponent<ResourceManager>();
    }
	
	// Update is called once per frame
	void Update () {

        transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);  
    }

    public void ShootGun()
    {
        manager.ChangeState(ResourceManager.State.SHOOT);
        manager.canCut = false;
        shoot.SetActive(true);
        scissors.SetActive(false);
    }

    public void Scissors()
    {
        manager.ChangeState(ResourceManager.State.CUT);
        manager.canCut = true;
        shoot.SetActive(false);
        scissors.SetActive(true);
    }
    public void HandOpen()
    {
        manager.ChangeState(ResourceManager.State.DRAG);
        manager.canCut = false;
        shoot.SetActive(false);
        scissors.SetActive(false);
        handClose.SetActive(false);
        handOpen.SetActive(true);
    }
    public void HandClosed()
    {
        manager.canCut = false;
        shoot.SetActive(false);
        scissors.SetActive(false);
        handClose.SetActive(true);
        handOpen.SetActive(false);
    }
}
