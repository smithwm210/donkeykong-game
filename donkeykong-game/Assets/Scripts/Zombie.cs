using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class Zombie : MonoBehaviour
{
    //[SerializeField] bool grounded = false;

    //[SerializeField] Transform groundCheckCollider;
    //[SerializeField] LayerMask groundLayer;

    //const float groundCheckRadius = 0.2f;


    //reference to waypoints
    public List<Transform> points;
    //int value for next point index
    public int nextID = 0;
    //value that applies to ID to change
    private int idChangeValue = 1;
    public float speed = 1;
    
    
    private void Reset()
    {
        Init();
    }

    void Init()
    {
        //make box collider trigger
        GetComponent<BoxCollider2D>().isTrigger = true;
        GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 1.5f);

        GameObject root = new GameObject(name + "-Root");
        //reset position of root to zombie
        root.transform.position = transform.position;
        //set enemy object as child of root
        transform.SetParent(root.transform);
        //create waypoints object
        GameObject waypoints = new GameObject("Waypoints");
        //reset waypoints position to root
        //make waypoints object child of root
        waypoints.transform.SetParent(root.transform);
        waypoints.transform.position = root.transform.position;
        //create 2 points and reset their position to waypoints objects
        //make the points children of waypoint object
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform); p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform); p2.transform.position = root.transform.position;

        //Init points list then add the points to it
        points = new List<Transform>();
        points.Add(p1.transform);
        points.Add(p2.transform);

    }

    private void Update()
    {
        MoveToNextPoint();
    }

    void FixedUpdate()
    {
        //GroundCheck();
    }

    private void MoveToNextPoint()
    {
        //get next points transform
        Transform goalPoint = points[nextID];
        //flip the zombie to look into the point's direction
        if (goalPoint.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
            transform.localScale = new Vector3(-1, 1, 1);
        //move zombie towards the goal point
        transform.position = Vector2.MoveTowards(transform.position, goalPoint.position, speed*Time.deltaTime);
        //check the distance between zombie and goal point to trigger next point
        if(Vector2.Distance(transform.position, goalPoint.position)< 0.1f)
        {
            //check if at the end of the line
            if (nextID == points.Count - 1)
                idChangeValue = -1;
            //check if at the start of the line
            if (nextID == 0)
                idChangeValue = 1;
            //apply change to nextID
            nextID += idChangeValue;
        }

    }

    //private void GroundCheck()
    //{
    //grounded = false;

    // see if groundcheck object is colliding with other
    // 2D colliders in the "Ground" layer
    //Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
    //if (colliders.Length > 0)
    //grounded = true;
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            FindObjectOfType<LifeCount>().LoseLife();
        }
    }
}
