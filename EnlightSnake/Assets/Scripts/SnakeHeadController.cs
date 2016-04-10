using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SnakeHeadController : MonoBehaviour {
    public float speed;
    private Vector3 forward = new Vector3(0,0,0.1f);
	private Vector3 backward = new Vector3(0,0,-1);
	private Vector3 left = new Vector3(-1,0,0);
	private Vector3 right = new Vector3(1,0,0);
	private Vector3 movement;
    private Vector3 movement2;
    public CardboardHead fancyCamera;
    public GameObject snakePiece;
    public GameObject food;
    public GameObject snakeHead;
    public Text loseText;
    public Text movementDirection;
    public Text scoreText;
    private GameObject lastMadePiece;
    private int score;
    private CharacterController characterController;
    private Rigidbody rb;
    private Vector3[] leaderPositions = new Vector3[300];
    private Transform target;
    private Vector3 direction;

    private Vector3 offset = new Vector3(0, 0, -2);
    public static ArrayList snakePieces = new ArrayList();

    //Cardboard Variables
    private Vector3 pos = Cardboard.SDK.HeadPose.Position;


    void Start()
    {
		
        movement = forward;
        //movementDirection.text = "Score: 0";
        snakePieces.Add(snakePiece);
        loseText.text = "";
        scoreText.text = "Score: 0";
        score = 0;

        //target = fancyCamera.target;
    }

	
    void Update()
    {
        //Populates the last leader positions for the past 30 frames.
		Vector3[] newVector = new Vector3[300];
        for (int i = leaderPositions.Length - 1; i > 0; i--)
        {
            newVector[i] = leaderPositions[i - 1];
        }
        newVector[0] = snakeHead.transform.position;
        //This part is the part that is throwing errors
		/*for(int i = 0; i < leaderPositions.Length; i++) {
			newVector[i].y = 0.75f;
		}*/
		leaderPositions = newVector;

        //So this is the previous movement thing.
        /*
        if (Input.GetKeyDown("right"))
        {
            if (movement.Equals(forward))
            {
                movement = right;
                movementDirection.text = "Right";
            }
            else if (movement.Equals(backward))
            {
                movement = left;
                movementDirection.text = "Left";
            }
            else if (movement.Equals(left))
            {
                movement = forward;
                movementDirection.text = "Forward";
            }
            else if (movement.Equals(right))
            {
                movement = backward;
                movementDirection.text = "Backward";
            }
        }
        else if (Input.GetKeyDown("left"))
        {
            if (movement.Equals(forward))
            {
                movement = left;
                movementDirection.text = "Left";
            }
            else if (movement.Equals(backward))
            {
                movement = right;
                movementDirection.text = "Right";
            }
            else if (movement.Equals(left))
            {
                movement = backward;
                movementDirection.text = "Backward";
            }
            else if (movement.Equals(right))
            {
                movement = forward;
                movementDirection.text = "Forward";
            }
        }
        transform.Translate(movement * speed);
        */
        //direction = target.transform.position;
        // movement = Vector3.Lerp(snakeHead.transform.position, direction, 0.5f);

        //change the x and z direction based on head poses
        movement.x = Cardboard.SDK.HeadPose.RightHandedMatrix.MultiplyVector(forward).x * 10;
        movement.z = Cardboard.SDK.HeadPose.RightHandedMatrix.MultiplyVector(forward).z * -10;
        print(movement.z.ToString());
       // movement = pos;


        transform.Translate(movement);
        loseText.text = "Target is: " + direction;
        
    }

    void OnTriggerEnter(Collider other)
    {

        string type = other.ToString();
        System.Console.WriteLine("Collided wiyh {0}", type);
        if (other.gameObject.CompareTag("Food"))
        {
            
            other.gameObject.SetActive(false);
            snakePieces.Add((GameObject)Instantiate(snakePiece,  new Vector3(0,0.5f,-2.0f), new Quaternion(0,0,0,0)));
        
            score++;
            scoreText.text = "Score: " + score;
            Instantiate(food, new Vector3(Random.value * 99, 1.1f, Random.value * 99), new Quaternion(0, 0, 0, 0));
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            //loseText.text = "You Died.... Great job bro";
            
            snakeHead.SetActive(false);

        }
        if (other.gameObject.CompareTag("SnakePiece"))
        {
            //loseText.text = "Unintended Collision!!!!!";
        }
		 
        
    }
    void LateUpdate()
    {
        //Follower 
        for (int i = 0; i < snakePieces.Count; i++)
        {
            if (Time.time >= 1)
            {
                if (i == 0)
                {
                    GameObject piece = (GameObject)snakePieces[i];
                    piece.transform.position = leaderPositions[3];

                }
                else
                {
                    GameObject piece = (GameObject)snakePieces[i];
                    piece.transform.position = leaderPositions[3 + 3 * i];

                }
            }
        }
        
    }
}
