using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public Color hoverColor = Color.gray;

    public Vector3 turretOffset;

    private Renderer rend;
    private Color startingColor;
    private GameObject turret;

    // Mouse Events
    void OnMouseEnter()
    {
        rend.material.color = hoverColor;
    }

    void OnMouseExit()
    {
        rend.material.color = startingColor;
    }

    void OnMouseDown()
    {
        if (turret != null)
        {
            Debug.Log("Can't build there! - TODO: Add to UI");
            return;
        }
    }

    // Fun Stuff
    void BuildTurret()
    {
        GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position + turretOffset, transform.rotation);
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        startingColor = rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
