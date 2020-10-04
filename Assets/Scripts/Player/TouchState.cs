using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    None = 0,
    Fire = 1,
    Build = 2
}

public class TouchState : MonoBehaviour
{
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Fire;
    }

    public void SetFire() { state = State.Fire; }
    public void SetBuild() { state = State.Build; }
    public void SetNone() { state = State.None; }

}
