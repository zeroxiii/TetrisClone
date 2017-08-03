using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // Reference to our game board
    Board gameBoard;

    // Reference to our spawner
    Spawner spawner;

    // Currently active shape
    Shape activeShape;

    float dropInterval = 1f;
    float timeToDrop;

	// Use this for initialization
	void Start () {
        
        // Locate the spawner and boards gameobjects
        gameBoard = GameObject.FindWithTag("Board").GetComponent<Board>();
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();

        if (this.spawner) {
            // Spawn a new shape if we currently don't have one
            if (this.activeShape == null) {
                this.activeShape = this.spawner.SpawnShape();
            }

            this.spawner.transform.position = Vectorf.Round(this.spawner.transform.position);
        }

        if (!this.gameBoard) {
            Debug.LogWarning("WARNING! There is no game board defined!");
        }

        if (!this.spawner) {
            Debug.LogWarning("WARNING! There is no spawner defined!");
        }
	}
	
	// Update is called once per frame
	void Update () {

        // If we don't have a spawner or gameboard, don't run the game
        if (!this.gameBoard || !this.spawner) {
            return;
        }

        if (Time.time > this.timeToDrop) {
            this.timeToDrop = Time.time + this.dropInterval;

			if (this.activeShape)
			{
				this.activeShape.MoveDown();

                // Verify that the new position is valid, if not, revert back to previous position
                if (!this.gameBoard.IsValidPosition(this.activeShape))
                {
                    this.activeShape.MoveUp();
                    this.gameBoard.StoreShapeInGrid(this.activeShape);

                    if (this.spawner) {
                        this.activeShape = this.spawner.SpawnShape();
                    }
                }
			}
		}

	}
}
