using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    // Reference to our game board
    Board gameBoard;

    // Reference to our spawner
    Spawner spawner;

    // Currently active shape
    Shape activeShape;

    public float dropInterval = 0.5f;
    float timeToDrop;

    float timeToNextKeyLeftRight;

    [Range(0.02f, 1f)]
    public float keyRepeatRateLeftRight = 0.15f;

    float timeToNextKeyDown;

    [Range(0.01f, 1f)]
    public float keyRepeatRateDown = 0.01f;

    float timeToNextKeyRotate;

    [Range(0.02f, 1f)]
    public float keyRepeatRateRotate = 0.20f;

    bool gameOver = false;

    public GameObject gameOverPanel;

    // Use this for initialization
    void Start()
    {

        // Locate the spawner and boards gameobjects
        this.gameBoard = GameObject.FindWithTag("Board").GetComponent<Board>();
        this.spawner = GameObject.FindWithTag("Spawner").GetComponent<Spawner>();

        this.timeToNextKeyLeftRight = Time.time;
        this.timeToNextKeyRotate = Time.time;
        this.timeToNextKeyDown = Time.time;
        this.timeToDrop = Time.time + this.dropInterval;

        if (!this.gameBoard)
        {
            Debug.LogWarning("WARNING! There is no game board defined!");
        }

        if (!this.spawner)
        {
            Debug.LogWarning("WARNING! There is no spawner defined!");
        }
        else
        {
            // Spawn a new shape if we currently don't have one
            if (this.activeShape == null)
            {
                this.activeShape = this.spawner.SpawnShape();
            }

            this.spawner.transform.position = Vectorf.Round(this.spawner.transform.position);
        }

        if (this.gameOverPanel)
        {
            this.gameOverPanel.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.gameBoard || !this.spawner || !this.activeShape || this.gameOver)
        {
            return;
        }
        else
        {
            PlayerInput();
        }
    }

    void PlayerInput()
    {
        // Listen for events and move the piece to the correct direction
        if (Input.GetButton("MoveRight") && Time.time > this.timeToNextKeyLeftRight || Input.GetButtonDown("MoveRight"))
        {
            this.activeShape.MoveRight();
            this.timeToNextKeyLeftRight = Time.time + this.keyRepeatRateLeftRight;

            if (!this.gameBoard.IsValidPosition(this.activeShape))
            {
                this.activeShape.MoveLeft();
            }
        }
        else if (Input.GetButton("MoveLeft") && Time.time > this.timeToNextKeyLeftRight || Input.GetButtonDown("MoveLeft"))
        {
            this.activeShape.MoveLeft();
            this.timeToNextKeyLeftRight = Time.time + this.keyRepeatRateLeftRight;

            if (!this.gameBoard.IsValidPosition(this.activeShape))
            {
                this.activeShape.MoveRight();
            }
        }
        else if (Input.GetButtonDown("Rotate") && Time.time > this.timeToNextKeyRotate)
        {
            this.activeShape.RotateRight();
            this.timeToNextKeyRotate = Time.time + this.keyRepeatRateRotate;

            if (!this.gameBoard.IsValidPosition(this.activeShape))
            {
                this.activeShape.RotateLeft();
            }
        }
        else if (Input.GetButton("MoveDown") && (Time.time > this.timeToNextKeyDown) || (Time.time > this.timeToDrop))
        {
            this.timeToDrop = Time.time + this.dropInterval;
            this.timeToNextKeyDown = Time.time + this.keyRepeatRateDown;
            this.activeShape.MoveDown();

            // Verify that the new position is valid, if not, revert back to previous position
            if (!this.gameBoard.IsValidPosition(this.activeShape))
            {
                if (this.gameBoard.IsOverLimit(this.activeShape))
                {
                    GameOver();
                }
                else
                {
                    LandShape();

                }
            }

        }
    }

    void LandShape()
    {
        // Land the shape
        this.activeShape.MoveUp();
        this.gameBoard.StoreShapeInGrid(this.activeShape);
        this.activeShape = this.spawner.SpawnShape();

        this.timeToNextKeyLeftRight = Time.time;
        this.timeToNextKeyRotate = Time.time;
        this.timeToNextKeyDown = Time.time;

        this.gameBoard.ClearAllRows();
    }

    void GameOver()
    {
        this.activeShape.MoveUp();
        this.gameOver = true;
        Debug.LogWarning(this.activeShape.name + " is over the limit");

        if (this.gameOverPanel)
        {
            this.gameOverPanel.SetActive(true);
        }
    }

    public void Restart()
    {
        Debug.Log("Restarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
