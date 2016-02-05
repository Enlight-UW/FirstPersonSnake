using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour {

    public GameObject leader;
    private Vector3 offset;


    void Start()
    {

        offset = transform.position - leader.transform.position;
    }

    void LateUpdate()
    {

        transform.position = leader.transform.position + offset;
    }
}
