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

public class PlayerController : MonoBehaviour
{
    private bool clicked;
    //--- Build Variables ---//
    private int cost = 0;
    private GameObject current;
    public GameObject basic;
    public GameObject advanced;
    public GameObject rocket;
    public GameObject laser;
    public GameObject factory;
    //--- Turret Control ---//
    public List<TurretController> turretList = new List<TurretController>();

    void Start() { current = basic; }

    // Update is called once per frame
    void Update()
    {
        //check for touches
        if (Input.touchCount > 0)
        {
            Touch tap = Input.GetTouch(0);
            //check if touch just began
            if (tap.phase == TouchPhase.Began)
            {
                Vector3 position = Camera.main.ScreenToWorldPoint(tap.position);

                switch (TouchState.state)
                {
                    //BuildMode
                    case State.Build:
                        buildTurret(position);
                        break;
                    //FireMode
                    case State.Fire:
                        foreach (TurretController turret in turretList)
                        {
                            turret.Fire(position);
                        }
                        break;
                    case State.None:
                        break;
                    default:
                        break;
                }
            }
        }
        //mouse touches
        else if (Input.GetMouseButtonDown(0) && !clicked)
        {
            clicked = true;
            Vector3 position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            switch (TouchState.state)
            {
                //BuildMode
                case State.Build:
                    buildTurret(position);
                    break;
                //FireMode
                case State.Fire:
                    foreach (TurretController turret in turretList)
                    {
                        turret.Fire(position);
                    }
                    break;
                case State.None:
                    break;
                default:
                    break;
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
        ////Check we are clicking on open space
        //RaycastHit2D hit = Physics2D.GetRayIntersection(new Ray(position, Vector3.forward), 20.0f, LayerMask.NameToLayer("Background"));
        //Debug.Log(hit.collider.gameObject.name);
        //if (hit.collider != null)
        //{
        //    buildTurret(position);
        //}
        //make sure we are clicking not on a menu
        if (position.z < 0)
        {
            position.z = 0;
            //Build Turret at tap point
            if (ResourcePool.SpendResources(cost))
            {
                Instantiate(current, position, transform.rotation);
            }
        }
        TouchState.SetNone();
    }

    //To access via buttons
    public void SetFire() { TouchState.state = State.Fire; }
    public void SetBuild() { TouchState.state = State.Build; }
    public void SetNone() { TouchState.state = State.None; }

}
