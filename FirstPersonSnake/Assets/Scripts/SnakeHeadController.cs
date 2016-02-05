using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SnakeHeadController : MonoBehaviour {
    private Rigidbody rb;
    public float speed;
    private Vector3 movement;
    public GameObject snakePiece;
    public GameObject snakeHead;
    public Text loseText;
    private bool shouldMove = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        loseText.text = "";

    }

	void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movement = new Vector3(moveHorizontal, 0, moveVertical);

        if (shouldMove)
        {
            rb.transform.Translate(movement * speed);
        }


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            shouldMove = false;
            loseText.text = "You Died.... Great job bro";
            snakeHead.SetActive(false);

        }
    }
}
