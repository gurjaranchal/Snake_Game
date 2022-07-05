using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Snake : MonoBehaviour
{


    [SerializeField] public Slider SpeedSlider;

    [SerializeField] public Text SpeedValue;
    //Tail prefab
    public GameObject tailPrefab;
    public int score = 0;
    public Text ScoreText;
    public AudioSource eat, over;
    public GameOverScreen GameOverScreen;
    //public Text points;

    //int maxPlatform = 0;


    //Tail List
    //List<Transform>tail = newList
    List<Transform> tail = new List<Transform>();

    //snake movement 
    Vector2 dir;
    Vector2 a;
    bool ate = false;
    public float SnakeMovementSpeed = 0.06f;
    // Start is called before the first frame update
    void Start()
    {
        //Initial snake movement
        Vector2 dir = Vector2.right;
        SpeedSlider.onValueChanged.AddListener((SnakeMovementSpeed) =>
        {
            CancelInvoke();
            InvokeRepeating("Move", 0.06f, SnakeMovementSpeed);

            SpeedValue.text = (10 - (SnakeMovementSpeed * 40)).ToString("0.00");
        });
        InvokeRepeating("Move", 0.06f, SnakeMovementSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
            dir = Vector2.right;
        else if (Input.GetKey(KeyCode.LeftArrow))
            dir = Vector2.left;
        else if (Input.GetKey(KeyCode.UpArrow))
            dir = Vector2.up;
        else if (Input.GetKey(KeyCode.DownArrow))
            dir = Vector2.down;
    }

    void Move()
    {
        //movement of snake 
        this.transform.Translate(dir);

        //tail position
        Vector2 v = this.transform.position;

        if (ate) //if snake eat something
        {
            //load preferab
            GameObject g = Instantiate(tailPrefab, a, Quaternion.identity);

            //Add  the list
            tail.Insert(0, g.transform);
            Debug.Log("snake ate the food");


            //ate false
            ate = false;

        }
        else if (tail.Count > 0)
        {
            tail.Last().position = a;
            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
        a = v;
    }
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        print("some collision");
        //food
        if (collision.name.StartsWith("food"))
        {
            ate = true;
            score += 1;
            eat.Play();
            ScoreText.text = score.ToString();
            //Destroy when other game object hits snake
            Destroy(collision.gameObject);
        }
        else if (collision.name.StartsWith("border"))
        {
            //score = maxPlatform;
            //reload current
            over.Play();
            GameOverScreen.Setup(score);
            Destroy(gameObject);
        }
        else if (collision.name.StartsWith("Tail"))
        {
            //reload current

            // score = maxPlatform;

            over.Play();
            GameOverScreen.Setup(score);
            Destroy(gameObject);


        }

    }

    public void AdjustSpeed(float newSpeed)

    {
        SnakeMovementSpeed = newSpeed;
    }
}