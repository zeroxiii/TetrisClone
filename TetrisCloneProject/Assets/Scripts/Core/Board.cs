using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour {

    // SpriteRender that will be instantiated in a grid to create our board
    public Transform emptySprite;

    // The height of the board
    public int height = 30;

    // The width of the board
    public int width = 10;

    // Number of rows where we won't have grid lines at the top
    public int header = 10;

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

    // Check if the position is within the board
    bool IsWithinBoard(int x, int y) {
        return (x >= 0 && x < this.width && y >= 0);
    }

    // Checks if the Shape is in a valid position in the board
    public bool IsValidPosition(Shape shape) {
        foreach (Transform child in shape.transform) {
            Vector2 pos = Vectorf.Round(child.position);

            if (!IsWithinBoard((int) pos.x, (int) pos.y)) {
                return false;
            }
        }

        return true;
    }

    // Draw our empty board with our empty sprite object(s)
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
