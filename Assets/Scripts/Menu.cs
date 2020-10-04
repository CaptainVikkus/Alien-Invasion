using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public void ToggleActive() { gameObject.SetActive(!gameObject.activeSelf); }
}
