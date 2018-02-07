using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour {

    public Text LlanaText;
    public Text TimeText;
    public GameObject cursor;
    private int llana = 0;
    public float time;
    private float timeLeft;
    public GameObject timeUp;
    public bool canCut = true;
    public enum State {CUT, SHOOT, DRAG }
    public State currentState = State.CUT;
	void Start () {
        timeLeft = time;
	}
	

	void Update () {
        if (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
        }
        else {
            timeLeft = 0;
            timeUp.SetActive(true);
            Time.timeScale = 0;
        }
        //CursosHandler();
        UpdateTexts();
    }
    void CursosHandler()
    {
        cursor.transform.position = Camera.main.WorldToScreenPoint(Input.mousePosition);
    }

    void UpdateTexts()
    {
        TimeText.text = Mathf.RoundToInt(timeLeft).ToString();
        LlanaText.text = llana.ToString();
    }
    public void SumLlana(int plusLlama)
    {
        llana += plusLlama;
    }
    public void RestLlana(int lessLlana)
    {
        llana -= lessLlana;
    }

    public void ChangeState(State newState)
    {
        switch (currentState)
        {
            case State.CUT:
                
                break;
            case State.SHOOT:

                break;
            case State.DRAG:

                break;

        }

        switch (newState)
        {
            case State.CUT:

                break;
            case State.SHOOT:

                break;
            case State.DRAG:

                break;

        }

        currentState = newState;
    }
}
