using UnityEngine;

public class Destroyer : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // if the object go out of the screen it will be destroy
        if (transform.position.x < -2) Destroy(gameObject);

    }
}
