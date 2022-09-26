using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments;

    public Transform food;
    public Transform segmentPrefab;
    public int initialSize = 3;

    private void Start()
    {
        _segments = new List<Transform>();
        ResetState();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.UpArrow) && _direction != Vector2.down && TurnIsAllowed())
        {
            _direction = Vector2.up;
        } else if (Input.GetKeyDown(KeyCode.LeftArrow) && _direction != Vector2.right && TurnIsAllowed())
        {
            _direction = Vector2.left;
        } else if (Input.GetKeyDown(KeyCode.DownArrow) && _direction != Vector2.up && TurnIsAllowed())
        {
            _direction = Vector2.down;
        } else if (Input.GetKeyDown(KeyCode.RightArrow) && _direction != Vector2.left && TurnIsAllowed())
        {
            _direction = Vector2.right;
        }
    }

    private bool TurnIsAllowed()
    {
        if (_segments.Count <= 1) { return true; }
        else
        {
            float x = this.transform.position.x;
            float y = this.transform.position.y;
            Vector3 expectedNeckPosition = new Vector3(x - _direction.x, y - _direction.y);
            return _segments[1].position == expectedNeckPosition;
        }
    }

    private void FixedUpdate()
    {
        OffsetSegments();

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x, 
            Mathf.Round(this.transform.position.y) + _direction.y,
            0.0f
        );
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            Grow();
        } else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }

    private void ResetState()
    {
        //Destroy segments and empty the _segments list
        for(int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();

        //Add initialSize number of segments into _segments
        _segments.Add(this.transform);
        for(int i = 1; i < initialSize; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        //Reset snake position and direction
        this.transform.position = new Vector3(0f, 0f);
        this._direction = Vector2.right;
    }

    private void Grow()
    {
        Transform newSegment = Instantiate(this.segmentPrefab);
        newSegment.position = _segments[_segments.Count - 1].position;
        _segments.Add(newSegment);
    }

    private void OffsetSegments()
    {
        for(int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }
    }
}
