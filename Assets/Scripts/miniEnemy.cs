using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniEnemy : MonoBehaviour
{
    public float force = 1;
    public float torque = 1;
    private Rigidbody enemyRB;
    private float destroyBoundary = -5.6f;
    public miniGameManager GameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        enemyRB.AddTorque(torque, torque, torque); // spin enemy
        GameManagerScript = GameObject.Find("miniGameManager").GetComponent<miniGameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        enemyRB.AddForce(Vector3.down * force); // push enemy down the screen
        // destroy if off screen
        if (transform.position.y < destroyBoundary)
        {
            Destroy(gameObject);
            GameManagerScript.UpdateScore(1); // send a point to GameManager
        }
    }
}

