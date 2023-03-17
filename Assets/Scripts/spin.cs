using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spin : MonoBehaviour
{
    enum Direction
    {
        Up,
        Down
    }

    [SerializeField] Direction direction;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    private void Update()
    {
        if (direction.Equals(Direction.Up))
        {
            transform.Rotate(Vector3.up * 2f);
            direction = Direction.Down;
        }
        else
        {
            transform.Rotate(Vector3.down * 2f);
            direction = Direction.Up;
        }
        
    }
}
