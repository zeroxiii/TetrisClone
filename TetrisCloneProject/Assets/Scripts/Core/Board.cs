using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{

    // SpriteRender that will be instantiated in a grid to create our board
    public Transform emptySprite;

    // The height of the board
    public int height = 30;

    // The width of the board
    public int width = 10;

    // Number of rows where we won't have grid lines at the top
    public int header = 10;

    // Store inactive shapes that have landed in board
    Transform[,] grid;

    void Awake()
    {
        this.grid = new Transform[this.width, this.height];
    }

    void Start()
    {
        DrawEmptyCells();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Check if the position is within the board
    bool IsWithinBoard(int x, int y)
    {
        return (x >= 0 && x < this.width && y >= 0);
    }

    // Check if the position is occuped by a stored shape
    bool IsOccupied(int x, int y, Shape shape)
    {
        return (this.grid[x, y] != null && this.grid[x, y].parent != shape.transform);
    }

    // Checks if the Shape is in a valid position in the board
    public bool IsValidPosition(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);

            if (!IsWithinBoard((int)pos.x, (int)pos.y))
            {
                return false;
            }

            if (IsOccupied((int)pos.x, (int)pos.y, shape))
            {
                return false;
            }
        }

        return true;
    }

    // Draw our empty board with our empty sprite object(s)
    void DrawEmptyCells()
    {
        if (this.emptySprite != null)
        {
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
        }
        else
        {
            Debug.Log("WARNING! Please assing the emptySprite object!");
        }
    }

    // Stores a shape in the grid array
    public void StoreShapeInGrid(Shape shape)
    {
        if (shape == null)
        {
            return;
        }

        foreach (Transform child in shape.transform)
        {
            Vector2 pos = Vectorf.Round(child.position);
            this.grid[(int)pos.x, (int)pos.y] = child;
        }
    }

    bool IsComplete(int y)
    {
        for (int x = 0; x < this.width; ++x)
        {
            // Exit early if a space is empty
            if (this.grid[x, y] == null)
            {
                return false;
            }
        }

        // All space in row complete, return true
        return true;
    }

    void ClearRow(int y)
    {
        for (int x = 0; x < this.width; ++x)
        {
            if (this.grid[x, y] != null)
            {
                Destroy(this.grid[x, y].gameObject);
            }
            this.grid[x, y] = null;
        }
    }

    void ShiftOneRowDown(int y)
    {
        for (int x = 0; x < this.width; ++x)
        {
            if (this.grid[x, y] != null)
            {
                this.grid[x, y - 1] = this.grid[x, y];
                this.grid[x, y] = null;
                this.grid[x, y - 1].position += new Vector3(0, -1, 0);
            }
        }
    }

    void ShiftRowsDown(int startY)
    {
        for (int i = startY; i < this.height; ++i)
        {
            ShiftOneRowDown(i);
        }
    }

    public void ClearAllRows()
    {
        for (int y = 0; y < this.height; ++y)
        {
            if (IsComplete(y))
            {
                ClearRow(y);
                ShiftRowsDown(y + 1);
                y--;
            }
        }
    }

    public bool IsOverLimit(Shape shape)
    {
        foreach (Transform child in shape.transform)
        {
            if (child.transform.position.y >= (this.height - this.header - 1))
            {
                return true;
            }
        }

        return false;
    }
}
