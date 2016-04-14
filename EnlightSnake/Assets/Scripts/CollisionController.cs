using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CollisionController : MonoBehaviour {
    private int score;
    public Text scoreText;
    public GameObject food;
    public Text loseText;
    public GameObject snakePiece;
    public GameObject snakeHead;
    private Vector3[] leaderPositions = new Vector3[300];
    public ArrayList snakePieces = new ArrayList();

    // Use this for initialization
    void Start () {
        snakePieces.Add(snakePiece);
        score = 0;
    }
	
	// Update is called once per frame
	void Update () {
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
    }

    void OnTriggerEnter(Collider other)
    {

        string type = other.ToString();
        if (other.gameObject.CompareTag("Food"))
        {

            other.gameObject.SetActive(false);
            snakePieces.Add((GameObject)Instantiate(snakePiece, new Vector3(0, 0.5f, -2.0f), new Quaternion(0, 0, 0, 0)));

            score++;
            scoreText.text = "Score: " + score;
            Instantiate(food, new Vector3(Random.value * 98, 1.1f, Random.value * 98), new Quaternion(0, 0, 0, 0));
        }
        if (other.gameObject.CompareTag("Wall"))
        {
            loseText.text = "You Died.... Great job bro";
            SceneManager.LoadScene(0);
            snakeHead.SetActive(false);

        }
        if (other.gameObject.CompareTag("SnakePiece"))
        {
            SceneManager.LoadScene(0);
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
