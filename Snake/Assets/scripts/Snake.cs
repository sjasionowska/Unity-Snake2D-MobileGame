using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour
{
    /// <summary>
    /// At the beginning we set up the default direction - up:
    /// Vector2 dir = Vector2.up; and we give her the number
    /// directionNumber = 0;
    /// We define, that the screen wasn't touched so the direction is not 
    /// changing now.
    /// </summary>
    Vector2 dir = Vector2.up;
    int directionNumber = 0;
    bool screenTouched = false;

    /// <summary>
    /// We create two lists for snake: 
    /// one - allTiles - for Game Objects that make up the snake
    /// second - occupiedPositionsBySnake - with their positions.
    /// In the Editor we add snake's head to allTiles list.
    /// </summary>
    [SerializeField]
    public List<GameObject> allTiles = new List<GameObject>();
    public List<Vector2> occupiedPositionsBySnake = new List<Vector2>();

    /// <summary>
    /// We define the tail prefab to enlarge our snake after he'll eat the food.
    /// CurrentTail is responsible for snake's tail - without head -
    /// for his move.
    /// At the beginning we have no tail, so tailNumber = 0;
    /// We define the speed with "SerializeField" attribute so
    /// we can redefine it in Unity Editor.
    /// </summary>
    [SerializeField]
    public GameObject tailPrefab;
    List<Transform> CurrentTail = new List<Transform>();
    int tailNumber = 0;
    [SerializeField]
    float speed = 5.0f;


    /// <summary>
    /// We add 4 tiles for the snake's tail.
    /// We use ChangeScreenTouchedVariable to ensure, that
    /// screen can be efficiently touched only once per one move, 
    /// so player can not change the direction too much. Also, 
    /// he can not change the direction if he made this once in this 
    /// move.
    /// We start snake's move.
    /// We start Food Generator so it will create our food just after
    /// the snake was created.
    /// </summary>
    void Start()
    {
        AddTiles();

        InvokeRepeating("ChangeScreenTouchedVariable", 0f, 1 / speed);
        InvokeRepeating("Move", 1 / speed, 1 / speed);

        FindObjectOfType<FoodGenerator>().FixedStart();
    }

    /// <summary>
    /// If screen wasn't touched yet in this one move,
    /// we check if player is touching the screen
    /// once per frame.
    /// </summary>
    void Update()
    {
        if (!screenTouched)
        {
            CheckTouch();
        }

    }

    /// <summary>
    /// Once per move we set up screenTouched variable to false.
    /// </summary>
    void ChangeScreenTouchedVariable()
    {
        screenTouched = false;
    }

    /// <summary>
    /// Method is checking if screen was touched.
    /// If yes, it's changing screenTouched variable for next move
    /// and checking, which direction is chosen by player.
    /// If player touched left side of the screen, 
    /// the snake will turn into left and analogously for right side.
    /// </summary>
    void CheckTouch()
    {
        foreach (Touch touch in Input.touches)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                screenTouched = true;
                if (touch.position.x < Screen.width / 2)
                {
                    directionNumber -= 1;
                    directionNumber %= 4;
                }
                else if (touch.position.x >= Screen.width / 2)
                {
                    directionNumber += 1;
                    directionNumber %= 4;
                }
            }
        }
    }

    /// <summary>
    /// At the beginning we add 4 tiles for tail,
    /// we save their transform in CurrentTail list for move,
    /// we give them names based on tile number so we can see 
    /// what is going on in the Editor,
    /// then we add them to allTiles list.
    /// </summary>
    void AddTiles()
    {
        for (int j = 0; j < 4; j++)
        {
            var v = new Vector2(transform.position.x, transform.position.y - j-1);
            GameObject tail = (GameObject)Instantiate(tailPrefab, v,
                                             Quaternion.identity);
            tail.name = "tail" + tailNumber;
            CurrentTail.Insert(j, tail.transform);
            allTiles.Add(tail);
            tailNumber++;
        }

    }

    /// <summary>
    /// Every time when snake will it food we will give him one tile 
    /// for his tail.
    /// Since we add this tile at the beginning of snake
    /// and the head has different colour than rest of the tiles,
    /// we set this tile Renderer as enabled for the beginning.
    /// Also the Collider is enabled so there will be no collision 
    /// between head and new tile when it's created.
    /// Later same as in AddTiles() method,
    /// we give the name, add this tile to CurrentTail and
    /// allTiles list, increase the number of tiles in tail.
    /// 
    /// </summary>
    public void AddTile()
    {
        Vector2 v = transform.position;
        GameObject tail = (GameObject)Instantiate(tailPrefab, v,
                                             Quaternion.identity);
        tail.GetComponent<BoxCollider2D>().enabled = false;
        tail.GetComponent<Renderer>().enabled = false;
        tail.name = "tail" + tailNumber;
        CurrentTail.Insert(0, tail.transform);
        allTiles.Add(tail);
        tailNumber++;
    }

    /// <summary>
    /// Player is moving like he's in snake's head - 
    /// if he goes left, head is moving left
    /// and analogously for the right side.
    /// We can see that in switch (directionNumber) action.
    /// 
    /// in every move we move the last tail element to where 
    /// first tile in tail was and we remove it from the back. 
    /// 
    /// In every move we check if we have any enabled tiles
    /// and we change their Renderer to true
    /// as well as the Collider so snake's head can 
    /// make a collision with it and lose the game.
    /// </summary>
    void Move()
    {

        switch (directionNumber)
        {
            case 0:
                dir = Vector2.up;
                break;

            case 1:
            case -3:
                dir = Vector2.right;
                break;

            case 2:
            case -2:
                dir = Vector2.down;
                break;

            case 3:
            case -1:
                dir = Vector2.left;
                break;
        }
        Vector2 v = transform.position;
        transform.Translate(dir);

        if (CurrentTail.Count > 0)
        {
            CurrentTail.Last().position = v;

            CurrentTail.Insert(0, CurrentTail.Last());
            CurrentTail.RemoveAt(CurrentTail.Count - 1);

            var enabledTails = CurrentTail.Where(tail => !tail.GetComponent<BoxCollider2D>().enabled);
            foreach (var tail in enabledTails)
            {
                tail.GetComponent<Renderer>().enabled = true;
                tail.GetComponent<BoxCollider2D>().enabled = true;
            }

        }

    }

    /// <summary>
    /// In this method we check if position is not occupied by snake's head or 
    /// tail. As an argument we use list with expected positions for food spawn.
    /// As default the available variable as true, then we clear whole 
    /// occupiedPositionsBySnake list and create it once again. 
    /// For snake's head and tail we check if expected position is the same. 
    /// If yes, we can't put there our food, so available is false;
    /// If there is no collision between these positions, we just leave it as 
    /// true. We use this method later in FoodGenerator class.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool CheckIfPositionAvailableSnakeTail(List<Vector2> position)
    {
        bool available = true;
        occupiedPositionsBySnake.Clear();

        var coordinatesX = allTiles.Select(tail => tail.transform.position.x).ToList();
        var coordinatesY = allTiles.Select(tail => tail.transform.position.y).ToList();

        for (int a = 0; a < allTiles.Count; a++)
        { 
            occupiedPositionsBySnake.Add(new Vector2(coordinatesX[a], coordinatesY[a]));
            if (occupiedPositionsBySnake.Last() == position.Last())
            {
                available = false;
                break;
            }
        }
        return available;
    }

}



