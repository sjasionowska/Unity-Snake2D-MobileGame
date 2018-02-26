using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FoodGenerator : MonoBehaviour {

    /// <summary>
    /// We set up prefabs for normal and special food.
    /// </summary>
    [SerializeField]
    GameObject normalFoodPrefab;
    [SerializeField]
    GameObject specialFoodPrefab;

    /// <summary>
    /// We create 2D vectors for position occupied by food, no matter 
    /// if special or normal - occupiedPosition,
    /// position for normal and special food expected 
    /// by the FoodGenerator script responsible for choosing 
    /// the position randomly.
    /// </summary>
    Vector2 occupiedPosition = new Vector2();
    Vector2 expectedPositionNormal = new Vector2();
    Vector2 expectedPositionSpecial = new Vector2();

    /// <summary>
    /// We create lists for expected positions for normal and 
    /// special food.
    /// </summary>
    List<Vector2> expectedPositionNormalList = new List<Vector2>();
    List<Vector2> expectedPositionSpecialList = new List<Vector2>();

    /// <summary>
    /// As default we expect that there is no normal nor special
    /// food on the scene.
    /// </summary>
    public bool currentNormalFood = false;
    public bool currentSpecialFood = false;

    /// <summary>
    /// We spawn normal food and start spawning special food
    /// after 8 seconds from game start, later it's repeating 
    /// every 15 seconds.
    /// </summary>
    public void FixedStart()
    {
        Spawn();
        InvokeRepeating("SpawnSpecial", 8, 15);
    }

    /// <summary>
    /// In every frame we check if there is normal food on the scene.
    /// If not, we spawn it.
    /// </summary>
    void Update()
    {
        if (currentNormalFood == false)
            Spawn();
    }

    /// <summary>
    ///  We use width and height of game board.
    ///  
    /// We search for position that is not occupied by food or snake
    /// and instantiate food there.
    /// 
    /// When food becomes instantiated we set currentNormalFood
    /// variable as true.
    /// We define normal food position as new occupied position. 
    /// 
    /// </summary>
    void Spawn()
    {
        float width = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().width;
        float height = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().height;
        do  
        {
           
            float x = (int)Random.Range((-width / 2 + 0.5f), (width / 2 - 0.5f));
            int randomizeNumber = Random.Range(0, 2);
            if (randomizeNumber == 0)
                x += 0.5f;
            if (randomizeNumber == 1)
                x -= 0.5f;
            int y = (int)Random.Range(-height / 2, height / 2);
            expectedPositionNormal = new Vector2(x, y);
            expectedPositionNormalList.Add(expectedPositionNormal);
        } while (!CheckIfPositionAvailable(expectedPositionNormal) || !FindObjectOfType<Snake>().CheckIfPositionAvailableSnakeTail(expectedPositionNormalList));
        Instantiate(normalFoodPrefab,
                    expectedPositionNormal,
                    Quaternion.identity);

        currentNormalFood = true;
        occupiedPosition = expectedPositionNormal;
    }

    /// <summary>
    /// Same as in normal food, but for special this time.
    /// </summary>
    void SpawnSpecial()
    {
        if (currentSpecialFood == false)
        {
            float width = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().width;
            float height = GameObject.Find("LevelGenerator").GetComponent<LevelGenerator>().height;

            do  
            {
                float x = (int)Random.Range((-width / 2 + 0.5f), (width / 2 - 0.5f));
                int randomizeNumber = Random.Range(0, 1);
                if (randomizeNumber == 0)
                    x += 0.5f;
                if (randomizeNumber == 1)
                    x -= 0.5f;
                int y = (int)Random.Range(-height / 2, height / 2);
                expectedPositionSpecial = new Vector2(x, y);
                expectedPositionSpecialList.Add(expectedPositionSpecial);
            } while (!CheckIfPositionAvailable(expectedPositionSpecial) || !FindObjectOfType<Snake>().CheckIfPositionAvailableSnakeTail(expectedPositionSpecialList)) ;

            // Instantiate the food at (x, y)
                Instantiate(specialFoodPrefab,
                        expectedPositionSpecial,
                        Quaternion.identity); // default rotation
            currentSpecialFood = true;
            occupiedPosition = expectedPositionSpecial;
        }
    }

    /// <summary>
    /// We check if expected position is available
    /// to spawn food there.
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    bool CheckIfPositionAvailable(Vector2 position)
    {
        return position != occupiedPosition;
    }

}
