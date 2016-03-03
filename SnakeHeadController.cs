using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SnakeHeadController : MonoBehaviour {
    public float speed;
    private Vector3 forward = new Vector3(0,0,1);
	private Vector3 backward = new Vector3(0,0,-1);
	private Vector3 left = new Vector3(-1,0,0);
	private Vector3 right = new Vector3(1,0,0);
	private Vector3 movement;
    
    public GameObject snakePiece;
    public GameObject snakeHead;
    public Text loseText;
    private GameObject lastMadePiece;
    private int score;
    private CharacterController characterController;
    private Rigidbody rb;
    private Vector3[] leaderPositions = new Vector3[30];

    private Vector3 offset = new Vector3(0, 0, -2);
    public static ArrayList snakePieces = new ArrayList();


    void Start()
    {
		
        movement = forward;
        snakePieces.Add(snakePiece);
        loseText.text = "";
        score = 0;
    }

	void FixedUpdate()
    {
		
        if (Input.GetKeyDown("right"))
        {
			if (movement == forward)
			{
				movement = right;
			}
			else if (movement == backward)
			{
				movement = left;
			}
			else if (movement == left)
			{
				movement = forward;
			}
			else if (movement == right)
			{
				movement = backward;
			}
        }
        else if (Input.GetKeyDown("left"))
        {
			if (movement == forward)
			{
				movement = left;
			}
			else if (movement == backward)
			{
				movement = right;
			}
			else if (movement == left)
			{
				movement = backward;
			}
			else if (movement == right)
			{
				movement = forward;
			}
        }
		else if(movement != forward && movement != backward && movement != right && movement != left) {
			movement = forward;
		}
        transform.Translate(movement * speed);
        


    }
    void Update()
    {
        //Populates the last leader positions for the past 30 frames.
		Vector3[] newVector = new Vector3[30];
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
        }
        if (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player"))
        {
            loseText.text = "You Died.... Great job bro";
            
            snakeHead.SetActive(false);

        }
		 
        
    }
    void LateUpdate()
    {
        //Follower 
        for (int i = 0; i < snakePieces.Count; i++)
        {
            if (i == 0)
            {
                GameObject piece = (GameObject)snakePieces[i];
                piece.transform.position = leaderPositions[3];

            }
            else
            {
                GameObject piece = (GameObject)snakePieces[i];
                piece.transform.position = leaderPositions[2 * i];

            }
        }
        
    }
}
