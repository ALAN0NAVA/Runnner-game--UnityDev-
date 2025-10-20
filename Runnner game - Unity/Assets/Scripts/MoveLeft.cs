using UnityEngine;


public class MoveLeft : MonoBehaviour
{
    private bool passed = false;
    private PlayerController playerControllerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControllerScript.gameActive)
        {
            //move to the left
            transform.Translate(Vector3.left * Time.deltaTime * playerControllerScript.increaseSpeed());
        }   
        if (transform.position.x < 12 && passed == false)
        {
            playerControllerScript.IncreaseScore(100);
            passed = true;
        }
    }
}
