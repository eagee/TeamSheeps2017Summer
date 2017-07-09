using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backdrop : MonoBehaviour {

    public GameObject prev = null, next = null;
    public int bdNumber = -1;
    public bool hasCamera = false;
    public bool destroyMyself = false;

	// Use this for initialization
	void Start () {
    }

    /// <summary>
    /// Locates "Respawn" tag in children and returns position of that transform.
    /// </summary>
    public Vector3 GetSpawnPoint()
    {
        // Find child object with tag, "Respawn"
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Transform child = this.transform.GetChild(i);
            if (child.tag == "Respawn")
            {
                return child.transform.position;
            }
        }
        Debug.Log("Failed to find child object with Respawn tag :(");
        return Vector3.zero;
    }

    // Update is called once per frame
    void Update () {
    }
}
