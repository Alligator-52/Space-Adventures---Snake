using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SnakeMovement : MonoBehaviour
{
    [Header("Player Pos Info")]
    private Vector2Int gridPos;
    private Vector2Int moveDirection;

    [Header("Player Timer")]
    private float moveTimer;
    public float moveTimerMax = 1f;

    [Header("Body Segments")]
    public GameObject bodySegment;
    private readonly List<Transform> segments = new List<Transform>();
    private Vector2 previousPos;

    [Header("Snake Size")]
    [SerializeField] private int initialSize = 3;
    [SerializeField] private int growSize = 3;
    [SerializeField] private int growSizeBig = 5;

    [Header("Sprites")]
    [SerializeField] private SpriteRenderer snakeHead;
    [SerializeField] private Sprite headDead;

    [Header("CameraShake")]
    public CameraShake cameraShake;

    [Header("Audio")]
    [SerializeField] private AudioSource foodSound;
    [SerializeField] private AudioSource bigFoodSound;
    [SerializeField] private AudioSource deathSound;

    #region joysticks
    [Header("Controls")]
    public FloatingJoystick joystick;

    #endregion


    #region Mobile Control Variables
    /* private Vector2 startTouchPos;
     private Vector2 endTouchPos;
     private Vector2 currentTouchPos;
     [SerializeField] private float swipeRange;
     private bool isTouching = false; */
    #endregion

    void Start()
    {
        GameObject player = GameObject.Find("SnakeHead");
        gridPos = new Vector2Int(0,0);
        moveTimer = moveTimerMax;
        moveDirection = new Vector2Int(0, 1);
        segments.Add(player.transform); // adding the head to the list
        //previousPos = segments[^1].position;
        InitSnake();
        GameObject temp = GameObject.Find("ShakeMAnager");
        cameraShake = temp.GetComponent<CameraShake>(); 

    }

    
    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void GetInput()
    {
    
        #region floating joystick
        

        if (Mathf.Round(joystick.Vertical) >= 0.1f)
        {

            if (moveDirection.x != 0)
            {
                if (moveDirection.y != -1)
                {
                    moveDirection.y = 1;
                    moveDirection.x = 0;
                }
            }   
        }
        else if (Mathf.Round(joystick.Vertical) <= -0.1f)
        {

            if (moveDirection.x != 0)
            {
                if (moveDirection.y != 1)
                {
                    moveDirection.y = -1;
                    moveDirection.x = 0;
                }
            }
           
        }
        else if (Mathf.Round(joystick.Horizontal) >= 0.1f)
        {
            if (moveDirection.y != 0)
            {
                if (moveDirection.x != -1)
                {
                    moveDirection.y = 0;
                    moveDirection.x = 1;
                }
            }
            
        }
        else if (Mathf.Round(joystick.Horizontal) <= -0.1f)
        {
            if (moveDirection.y != 0)
            {
                if (moveDirection.x != 1)
                {
                    moveDirection.y = 0;
                    moveDirection.x = -1;
                }
            }
            
        }

        #endregion

        #region Mobile Inputs
        /*
       if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
       {
           startTouchPos = Input.GetTouch(0).position;
       }
       if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
       {
           currentTouchPos = Input.GetTouch(0).position;
           Vector2 touchDistance = currentTouchPos - startTouchPos;
           if(!isTouching)
           {
               if (touchDistance.y > swipeRange)
               {
                   if (moveDirection.y != -1)
                   {
                       moveDirection.y = 1;
                       moveDirection.x = 0;
                   }
                   isTouching = true;  
               }
               else if (touchDistance.y < -swipeRange)
               {
                   if (moveDirection.y != 1)
                   {
                       moveDirection.y = -1;
                       moveDirection.x = 0;
                   }
                   isTouching = true;
               }
               else if (touchDistance.x > swipeRange)
               {
                   if (moveDirection.x != -1)
                   {
                       moveDirection.y = 0;
                       moveDirection.x = 1;
                   }
                   isTouching = true;
               }
               else if (touchDistance.x < -swipeRange)
               {
                   if (moveDirection.x != 1)
                   {
                       moveDirection.y = 0;
                       moveDirection.x = -1;
                   }
                   isTouching = true;
               }
           }
       }
       if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
       {
           isTouching = false;
       }
        
        If anyone is having any trouble with the diagonal swiping favouring the x-axis more than the y axis, try adding this to your left and right detection:

               if (Distance.x < - swipeRange && Mathf.Abs(Distance.x) > Mathf.Abs(Distance.y) + 150) {
                   Debug.Log("left");
                   stopTouch = true;
               }
               else if (Distance.x > swipeRange && Mathf.Abs(Distance.x) > Mathf.Abs(Distance.y) + 150) {
                   Debug.Log("right");
                   stopTouch = true;
               }
        */
        #endregion

        #region PC Inputs       

        if (Input.GetKeyDown(KeyCode.W))
        {

            if (moveDirection.x != 0)
            {
                if (moveDirection.y != -1)
                {
                    moveDirection.y = 1;
                    moveDirection.x = 0;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {

            if (moveDirection.x != 0)
            {
                if (moveDirection.y != 1)
                {
                    moveDirection.y = -1;
                    moveDirection.x = 0;
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            if (moveDirection.y != 0)
            {
                if (moveDirection.x != -1)
                {
                    moveDirection.y = 0;
                    moveDirection.x = 1;
                }
            }

        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            if (moveDirection.y != 0)
            {
                if (moveDirection.x != 1)
                {
                    moveDirection.y = 0;
                    moveDirection.x = -1;
                }
            }

        }
        
        #endregion
    }

    private void MovePlayer()
    {
        moveTimer += Time.deltaTime; //moveTimer keeps increasing 
        if (moveTimer > moveTimerMax)
        {
            previousPos = segments[^1].position;
            moveTimer -= moveTimerMax;
            gridPos += moveDirection;
            
            for (int i = segments.Count - 1; i > 0; i--)
            {
                segments[i].transform.position = segments[i - 1].transform.position;
                //giving the last segment the postion of the next segment.
            }

            transform.position = new Vector3(gridPos.x, gridPos.y);
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(moveDirection));
        }

    }

    private float GetAngleFromVector(Vector2Int dir)
    {
        float n = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
        if(n<0)
        {
            n += 360;
        }
        return n;
    }

    private void InitSnake()
    {
        
        for (int i = 1; i <= initialSize; i++)
        {
            GameObject segmentIncrease = Instantiate(this.bodySegment);
            //segmentIncrease.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            segments.Add(segmentIncrease.transform);
        }
    }

    void GrowSnake()
    {
        
        for (int i = 1; i<= growSize; i++)
        {
            GameObject segmentIncrease = Instantiate(this.bodySegment);
            //segmentIncrease.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);  
            segmentIncrease.transform.position = previousPos;
            segments.Add(segmentIncrease.transform);
        }

    }

    void GrowSnakeBig()
    {
        
        for (int i = 1; i <= growSizeBig; i++)
        {
            GameObject segmentIncrease = Instantiate(this.bodySegment);
            //segmentIncrease.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
            segmentIncrease.transform.position = previousPos;
            segments.Add(segmentIncrease.transform);
        }

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Food"))
        {
            foodSound.Play();   
            GrowSnake();
            FindObjectOfType<GameManager>().ScoreCounter();
            cameraShake.ShakeCameraOnFood();
        }
        else if(col.CompareTag("BigFood"))
        {
            bigFoodSound.Play();
            GrowSnakeBig();
            FindObjectOfType<GameManager>().ScoreCounterBig();
            cameraShake.ShakeCameraOnBFood();
        }
        else if(col.CompareTag("Boundary") || col.CompareTag("BodySegment"))
        {
            deathSound.Play();
            FindObjectOfType<UIManager>().GameOver();
            snakeHead.sprite = headDead;
            cameraShake.ShakeCamera();
        }

        #region for spawing on the other side of the wall
        /*else if (col.CompareTag("Boundary"))
        {
            Vector3 position = transform.position;

            if (moveDirection.x != 0f)
            {
                position.x = -col.transform.position.x + moveDirection.x;
            }
            else if (moveDirection.y != 0f)
            {
                position.y = -col.transform.position.y + moveDirection.y;
            }

            transform.position = position;
        }*/
        #endregion
    }

    public bool OccupiesPos(float x, float y)
    {
        foreach(Transform segment in segments)
        {
            if(segment.position.x == x && segment.position.y == y)
            {
                return true;
            }
        }
        return false;

    }


}
