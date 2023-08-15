using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Look Locations")]
    public Transform menu;
    public Transform credits;
    public Transform levelSelect;

    [Header("Camera Settings")]
    public Camera cam;
    public float camSpeed = 2f;

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
        target = levelSelect;
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

    public void Back()
    {
        target = menu;
    }
}
