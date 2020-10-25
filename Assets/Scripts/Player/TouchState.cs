using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    None = 0,
    Fire = 1,
    Build = 2
}

//State machine for handling different touch types
public class TouchState
{
    static public State state;

    static public void SetFire() { state = State.Fire; }
    static public void SetBuild() { state = State.Build; }
    static public void SetNone() { state = State.None; }

}
