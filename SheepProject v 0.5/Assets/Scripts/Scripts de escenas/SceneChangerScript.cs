using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChangerScript : MonoBehaviour {

    /*private AudioSource myAudioSource;
    public AudioClip myAudioClip;*/

	// Use this for initialization
	void Start () {
        //myAudioSource = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GoToLevel1Scene()
    {
        //SceneManager.LoadScene(2);
        StartCoroutine(DelaySceneLoad(2));
    }

    public void GoToLevel2Scene()
    {
        //SceneManager.LoadScene(3);
        StartCoroutine(DelaySceneLoad(3));
    }

    public void GoToLevel3Scene()
    {
        //SceneManager.LoadScene(4);
        StartCoroutine(DelaySceneLoad(4));
    }

    public void GoToInstructionsScene()
    {
        //SceneManager.LoadScene(1);
        StartCoroutine(DelaySceneLoad(1));
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //si le damos al botón de Quit en Unity, parará de jugar
#else
        Application.Quit(); //si le damos Quit fuera de Unity, cerrará el programa
#endif
    }

    public void GoBackToMenu()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        //SceneManager.LoadScene(0);
        StartCoroutine(DelaySceneLoad(0));
    }

    public void GoToNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GoToPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    /*public void PlaySoundOnClick()
    {
        myAudioSource.PlayOneShot(myAudioClip);
    }*/

    IEnumerator DelaySceneLoad(int sceneNum)
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(sceneNum);
    }
}
