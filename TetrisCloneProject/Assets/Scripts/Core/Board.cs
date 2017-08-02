using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    public Transform emptySprite;
    public int height = 30;
    public int width = 10;
    public int header = 8;

    // Use this for initialization
    Transform[,] grid;

    void Awake() {
        this.grid = new Transform[this.width, this.height];
    }

	void Start () {
        DrawEmptyCells();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void DrawEmptyCells() {
        if (this.emptySprite != null) {
            for (int y = 0; y < this.height - this.header; y++)
			{
				for (int x = 0; x < this.width; x++)
				{
					Transform clone;
					clone = Instantiate(this.emptySprite, new Vector3(x, y, 0), Quaternion.identity) as Transform;
					clone.name = "Board Space ( x = " + x.ToString() + " , y =" + y.ToString() + " )";
					clone.transform.parent = transform;
				}
			}
        } else {
            Debug.Log("WARNING! Please assing the emptySprite object!");
        }

    }
}
