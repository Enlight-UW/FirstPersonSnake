using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SnakeHeadController : MonoBehaviour {
    public float speed;
    private Vector3 movement;
    
    public GameObject snakePiece;
    public GameObject snakeHead;
    public Text loseText;
    private GameObject lastMadePiece;
    private int score;
    private CharacterController characterController;
    public int turnSpeed;
    private Rigidbody rb;
    private Vector3[] leaderPositions = new Vector3[30];

    private Vector3 offset = new Vector3(0, 0, -2);
    public static ArrayList snakePieces = new ArrayList();


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        snakeHead = GetComponent<GameObject>();

        snakePieces.Add(Instantiate(snakePiece, new Vector3(0, 0.75f, -1.0f), new Quaternion(0, 0, 0, 0)));

        loseText.text = "";
        score = 0;
        for (int i = 0; i < snakePieces.Count; i++)
        {
            if (i == 0)
            {
                GameObject piece = (GameObject)snakePieces[i];
                piece.transform.position = snakeHead.transform.position + offset;

            }
            else
            {
                GameObject piece = (GameObject)snakePieces[i];
                GameObject previousPiece = (GameObject)snakePieces[i - 1];
                piece.transform.position = previousPiece.transform.position + offset;
            }
        }


    }

	void FixedUpdate()
    {
        movement = new Vector3(0, 0, 1);
        transform.Translate(movement * speed);
        


    }
    void Update()
    {

        for (int i = leaderPositions.Length - 1; i > 0; i--)
        {
            leaderPositions[i] = leaderPositions[i - 1];
        }
        leaderPositions[0] = snakeHead.transform.position;
    }

    void OnTriggerEnter(Collider other)
    {
        string type = other.ToString();
        System.Console.WriteLine("Collided wiyh {0}", type);
        if (other.gameObject.CompareTag("Food"))
        {
            
            other.gameObject.SetActive(false);
            snakePieces.Add(Instantiate(snakePiece,  new Vector3(0,0.75f,-2.0f), new Quaternion(0,0,0,0)));
            score++;
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            loseText.text = "You Died.... Great job bro";
            
            snakeHead.SetActive(false);

        }
        
    }
    void LateUpdate()
    {

        for (int i = 0; i < snakePieces.Count; i++)
        {
            if (i == 0)
            {
                GameObject piece = (GameObject)snakePieces[i];
                piece.transform.position = leaderPositions[29];

            }
            else
            {
                GameObject piece = (GameObject)snakePieces[i];
                GameObject previousPiece = (GameObject)snakePieces[i - 1];

            }
        }
    }
}
