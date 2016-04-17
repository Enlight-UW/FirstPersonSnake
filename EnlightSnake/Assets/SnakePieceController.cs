using UnityEngine;
using System.Collections;

public class SnakePieceController : MonoBehaviour {
    // Reference to our child. this may initially be null because we might be the last piece in the chain
    public GameObject child;
    // true if this piece is the tail, false otherwise
    public bool isTail;
    public bool isHead = false;
    public static int ARRAY_LENGTH;
    // the number of children that we gain when we pick up a new pellet
    public int numChildrenOnPickup;
    // Keep track of our last 30 positions. the head keeps track of the current position, the tail is the least recent remembered position
    private Vector3[] mPrevLeaderPositions = new Vector3[ARRAY_LENGTH];
    // Keep another vector that we can give to a new child/children
    private Vector3[] mTailPositions;

	// Use this for initialization
	void Start () {
        mTailPositions = new Vector3[30 * numChildrenOnPickup];
    }
	
	// Update is called once per frame
	void Update () {
        // on every update we need to somehow shift all the positions down, update the transform of our object, and pass the most recent position to the tail of our child
        
        if (isHead)
        {
            this.addNewPosition(this.gameObject.transform.position);
            // add a new position to OUR list if we're the head, which will recursively add to the children
        }

    }

    // Add a child to this snake piece. Only allowed to have a single child
    public void addChild(GameObject newSnakePiece)
    {
        child = newSnakePiece;
    }

    // Called when we need to initialize the new children that were just added. We need to pass our pieces of the array that we're keeping for them
    public void initNewChildren()
    {
        GameObject currChild = this.gameObject;
        // Initialize the position vectors of the new children
        for (int i  = 0; i < numChildrenOnPickup; i++)
        {
            // get the next child
            currChild = currChild.GetComponent<SnakePieceController>().child;
            // allocate a vector for the new positions
            Vector3[] newPositions = new Vector3[ARRAY_LENGTH];
            System.Array.Copy(mTailPositions, i * ARRAY_LENGTH, newPositions, 0, ARRAY_LENGTH);
            currChild.GetComponent<SnakePieceController>().setPositions(newPositions);
        }
        this.isTail = false;
    }

    // Called when we want to set the initial positions upon gaining a child
    public void setPositions(Vector3[] positions)
    {
        mPrevLeaderPositions = positions;
    }
    

    private void addNewPosition(Vector3 position)
    {
        Vector3 childMessage = mPrevLeaderPositions[ARRAY_LENGTH - 1];
        // our new position should go to at position zero, and we should shift all the other positions
        for (int i = ARRAY_LENGTH - 1; i > 0; i--)
        {
            mPrevLeaderPositions[i] = mPrevLeaderPositions[i - 1];
        }
        mPrevLeaderPositions[0] = position;
        if (child != null)
        {
            child.GetComponent<SnakePieceController>().addNewPosition(childMessage);
        }
        // also update the other list if tail 
        if (isTail)
        {
            for (int i = (ARRAY_LENGTH * numChildrenOnPickup) - 1; i > 0; i--)
            {
                mTailPositions[i] = mTailPositions[i - 1];
            }
            mTailPositions[0] = position;
        }
        else
        {
            mTailPositions = null;
        }
    }
}
