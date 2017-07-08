using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event {

    public EventType Type;
    public float AtSeconds;
    public bool Fired = false;
}
