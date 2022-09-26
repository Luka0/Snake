using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D leftWall;
    public Collider2D rightWall;
    public Collider2D topWall;
    public Collider2D bottomWall;

    private void Start()
    {
        RandomizePosition();
    }

    private void RandomizePosition()
    {
        float x = Random.Range(leftWall.transform.position.x + 1, rightWall.transform.position.x - 1);
        float y = Random.Range(bottomWall.transform.position.y + 1, topWall.transform.position.y - 1);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Snake")
        {
            RandomizePosition();
        }
    }
}
