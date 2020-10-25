using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Turret
{
    Basic = 0,
    Advanced = 1,
    Rocket = 2,
    Laser = 3,
    Factory = 10
}

public class Builder : MonoBehaviour
{
    private bool clicked;
    private int cost = 0;
    private GameObject current;
    public GameObject basic;
    public GameObject advanced;
    public GameObject rocket;
    public GameObject laser;
    public GameObject factory;

    void Start() { current = basic; }

    // Update is called once per frame
    void Update()
    {
        //check for touches
        if (Input.touchCount > 0)
        {
            // check we are in build mode
            if (TouchState.state == State.Build)
            {

                Touch tap = Input.GetTouch(0);
                //check if touch just began
                if (tap.phase == TouchPhase.Began)
                {
                    Vector3 position = Camera.main.ScreenToWorldPoint(tap.position);
                    //Check we are clicking on open space
                    RaycastHit2D hit = Physics2D.GetRayIntersection(new Ray(position, Vector3.forward), 20.0f, LayerMask.NameToLayer("UI"));
                    Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider != null)
                    {
                        buildTurret(position);
                    }
                    ////make sure we are clicking not on a menu
                    //if (position.z < 0)
                    //{
                    //    position.z = 0;
                    //    //Build Turret at tap point
                    //    buildTurret(position);
                    //}
                }
            }
        }
        //mouse touches
        else if (Input.GetMouseButtonDown(0) && !clicked)
        {
            clicked = true;
            // check we are in build mode
            if (TouchState.state == State.Build)
            {
                Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                //Check we are clicking on open space
                RaycastHit2D hit = Physics2D.GetRayIntersection(new Ray(position, Vector3.forward), 20.0f, LayerMask.NameToLayer("Background"));
                Debug.Log(hit.collider.gameObject.name);
                if (hit.collider != null)
                {
                    buildTurret(position);
                }
                ////make sure we are clicking not on a menu
                //if (position.z < 0)
                //{
                //    position.z = 0;
                //    //Build Turret at tap point
                //    buildTurret(position);
                //}
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
        }
    }

    //changes the current turret build
    public void SetTurret(int turret)
    {
        switch ((Turret)turret)
        {
            case Turret.Basic:
                current = basic;
                cost = 5;
                break;
            case Turret.Advanced:
                current = advanced;
                cost = 10;
                break;
            case Turret.Rocket:
                current = rocket;
                cost = 10;
                break;
            case Turret.Laser:
                current = laser;
                cost = 15;
                break;
            case Turret.Factory:
                current = factory;
                cost = 5;
                break;

            default:
                current = basic;
                cost = 5;
                break;
        }
    }

    private void buildTurret(Vector3 position)
    { 
        if (ResourcePool.SpendResources(cost))
        {
            Instantiate(current, position, transform.rotation);
        }
    }
}
