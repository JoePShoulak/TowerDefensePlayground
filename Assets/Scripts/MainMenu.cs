using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public Transform menu;
    public Transform credits;
    public Camera camera;
    public float camSpeed = 2f;

    private Transform target;

    private void Start()
    {
        target = menu;
        camera.transform.LookAt(target);
    }

    private void Update()
    {
        Vector3 dir = target.transform.position - camera.transform.position;
        Quaternion rotationQ = Quaternion.LookRotation(dir);
        Vector3 rotationE = Quaternion.Lerp(camera.transform.rotation, rotationQ, Time.deltaTime * camSpeed).eulerAngles;
        camera.transform.rotation = Quaternion.Euler(rotationE);
    }

    public void Play()
    {
        SceneController.LoadSample();
    }

    public void Quit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }

    public void Credits()
    {
        target = credits;

    }

    public void BackFromCredits()
    {
        target = menu;
    }
}
