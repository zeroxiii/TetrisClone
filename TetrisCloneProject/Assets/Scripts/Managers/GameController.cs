using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    // Reference to our game board
    Board gameBoard;

    // Reference to our spawner
    Spawner spawner;

	// Use this for initialization
	void Start () {
        gameBoard = GameObject.FindWithTag("Board").GetComponent<Board>();
        spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();

        if (!this.gameBoard) {
            Debug.LogWarning("WARNING! There is no game board defined!");
        }

        if (!this.spawner) {
            Debug.LogWarning("WARNING! There is no spawner defined!");
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
