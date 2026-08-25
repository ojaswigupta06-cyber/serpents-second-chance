using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class miniPlayer : MonoBehaviour
{
    public float speed = 10;
    private float horizontal;
    public float boundary = 12.15f;
    private miniGameManager gameManagerScript;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = GameObject.Find("miniGameManager").GetComponent<miniGameManager>();
        gameManagerScript.gameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");

        transform.Translate(Vector3.right * Time.deltaTime * speed * horizontal);

        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        viewportPosition.x = Mathf.Clamp(viewportPosition.x, 0f, 1f);

        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        Destroy(gameObject);
        gameManagerScript.LoseGame();
    }
}