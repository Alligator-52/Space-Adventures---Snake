using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour
{
    public BoxCollider2D boundary;
    void Start()
    {
        RandomisePosition();    
    }

    private void RandomisePosition()
    {
        float x = Random.Range(boundary.bounds.min.x, boundary.bounds.max.x);
        float y = Random.Range(boundary.bounds.min.y, boundary.bounds.max.y);
        x = Mathf.Round(x);
        y = Mathf.Round(y);
        while(FindObjectOfType<SnakeMovement>().OccupiesPos(x,y))
        {
            //We will advance the food position to the next row/column until the position is not occupied by the snake.
            //Stops the objects from spawning on the snake
            x++;
            if (x > boundary.bounds.max.x)
            {
                x = boundary.bounds.min.x;
                y++;
                if(y > boundary.bounds.max.y)
                {
                    y = boundary.bounds.min.y;
                }
            }
        }
        transform.position = new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        RandomisePosition();
        /*if(col.CompareTag("Player"))
        {
            RandomisePosition();
        }
        if(col.CompareTag("BodySegment"))
        {
            RandomisePosition();
            Debug.Log("Spawned on snake");
        }*/
    }
}
