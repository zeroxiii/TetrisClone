using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public Shape[] allShapes;

    Shape GetRandomShape()
    {
        int i = Random.Range(0, this.allShapes.Length);

        if (this.allShapes[i])
        {
            return this.allShapes[i];
        }
        else
        {
            Debug.Log("WARNING! Invalid shape!");
            return null;
        }
    }

    public Shape SpawnShape()
    {
        Shape shape = null;
        shape = Instantiate(GetRandomShape(), transform.position, Quaternion.identity) as Shape;
        if (shape)
        {
            return shape;
        }
        else
        {
            Debug.Log("WARNING! Invalid shape is in spawner!");
            return null;
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
