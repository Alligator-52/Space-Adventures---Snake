using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.UI;

public class RandomFoodObstacle : MonoBehaviour
{
    public BoxCollider2D boundary;

    [Header("Food")]
    [SerializeField] private GameObject bigFood; //Prefab of the food
    [SerializeField] private int destroyTimeFood = 3;
    [SerializeField] private float mintimeFood;
    [SerializeField] private float maxtimeFood;
    [SerializeField] private float decreaseValBy = 12f;
    [HideInInspector] public float initBigFoodValue = 100;

    [Header("Collectible")]
    [SerializeField] private GameObject collectible; //Prefab of the food
    [SerializeField] private int destroyTimeCollectible = 3;
    [SerializeField] private float mintimecollectible;
    [SerializeField] private float maxtimecollectible;

   /* [Header("Big food healthbar")]
    [SerializeField] private int timer = 5;
    [SerializeField] private Image healthBar;
    [SerializeField] private Transform healtBarParent;
    private int timerInt;
    private float timerFloat; */

    private void Start()
    {
        initBigFoodValue = 100;
        StartCoroutine(BigFoodSpawner());
        StartCoroutine(CollectibleSpawnner());
        //timerInt = timer;
        //timerFloat = (float)timer;
    }

    private void Update()
    {
        ScoreDecreaser();
    }
    IEnumerator BigFoodSpawner()
    {
        
        while(Time.timeScale == 1)
        {
            float x = Mathf.Round(Random.Range(boundary.bounds.min.x, boundary.bounds.max.x));
            float y = Mathf.Round(Random.Range(boundary.bounds.min.y, boundary.bounds.max.y));
            float timeFood = Random.Range(mintimeFood, maxtimeFood);
            yield return new WaitForSeconds(timeFood);
            while (FindObjectOfType<SnakeMovement>().OccupiesPos(x, y))
            {
                //We will advance the food position to the next row/column until the position is not occupied by the snake.
                //Stops the objects from spawning on the snake
                x++;
                if (x > boundary.bounds.max.x)
                {
                    x = boundary.bounds.min.x;
                    y++;
                    if (y > boundary.bounds.max.y)
                    {
                        y = boundary.bounds.min.y;
                    }
                }
            }
            GameObject destroyThisFood =  Instantiate(bigFood, new Vector2(x,y), Quaternion.identity);
            yield return new WaitForSeconds(destroyTimeFood);
            Destroy(destroyThisFood);
            yield return null;
        }
    }

    IEnumerator CollectibleSpawnner()
    {
        while (Time.timeScale == 1)
        {
            float x = Mathf.Round(Random.Range(boundary.bounds.min.x, boundary.bounds.max.x));
            float y = Mathf.Round(Random.Range(boundary.bounds.min.y, boundary.bounds.max.y));
            float timeCollectible = Random.Range(mintimecollectible, maxtimecollectible);
            yield return new WaitForSeconds(timeCollectible);
            while (FindObjectOfType<SnakeMovement>().OccupiesPos(x, y))
            {
                //We will advance the food position to the next row/column until the position is not occupied by the snake.
                //Stops the objects from spawning on the snake
                x++;
                if (x > boundary.bounds.max.x)
                {
                    x = boundary.bounds.min.x;
                    y++;
                    if (y > boundary.bounds.max.y)
                    {
                        y = boundary.bounds.min.y;
                    }
                }
            }
            GameObject destroyThisCollectible = Instantiate(collectible, new Vector2(x, y), Quaternion.identity);
            yield return new WaitForSeconds(destroyTimeCollectible);
            Destroy(destroyThisCollectible);
            yield return null;
        }
    }

    private void ScoreDecreaser()
    {
        if (GameObject.Find("BigFood(Clone)") != null)  //checks if the object is present in the scene or not
        {
            initBigFoodValue -= decreaseValBy * Time.deltaTime;
        }
        else
        {
            initBigFoodValue = 100;
        }
        if (initBigFoodValue < 0)
        {
            initBigFoodValue = 100;
        }
    }

    /*IEnumerator HealthBarStatus()
    {
        Image destroyThisBar = Instantiate(healthBar, healtBarParent.transform.position, Quaternion.identity, healtBarParent);
        while (timerFloat > 0)
        {
            timerFloat -= Time.deltaTime;
            //destroyThisBar.fillAmount = timerFloat / timerInt;
            healthBar.fillAmount = timerFloat / timerInt;
        }
        Destroy(destroyThisBar);
        healthBar.fillAmount = 1;
        yield return null;
        
    }*/
}
