using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Transform menu;
    public Transform credits;
    public Camera cam;
    public float camSpeed = 2f;
    public Transition transition;

    private Transform target;

    private void Start()
    {
        target = menu;
        cam.transform.LookAt(target);
    }

    private void Update()
    {
        Vector3 dir = target.transform.position - cam.transform.position;
        Quaternion rotationQ = Quaternion.LookRotation(dir);
        Vector3 rotationE = Quaternion.Lerp(cam.transform.rotation, rotationQ, Time.deltaTime * camSpeed).eulerAngles;
        cam.transform.rotation = Quaternion.Euler(rotationE);
    }

    public void Play()
    {
        transition.FadeTo("SampleLevel");
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void Credits()
    {
        target = credits;
        // TODO: Make certain words in the credits be clickable linkts to things
    }

    public void BackFromCredits()
    {
        target = menu;
    }
}
