using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainScript : MonoBehaviour
{
    public static GameObject Ball,Platform;
    [HideInInspector] public List<GameObject> bricks;
    public static int SpeedX = 4;
    private float d = 0.8f;
    bool _collisionX = false,_collisionY = false;

    private void Start()
    {
        Platform = GameObject.FindWithTag("Platform");
        bricks = new List<GameObject>(GameObject.FindGameObjectsWithTag("Brick"));
        Ball = this.gameObject;
    }

    private void FixedUpdate()
    {
        MoveBall();
        CheckPosition();
    }

    void LateUpdate()
    {
        CheckForTouchPlatform();
        CheckForTouchBricks();
    }

    private void CheckForTouchBricks()
    {
        for (int i = 0; i < bricks.Count; i++)
        {
            if (CheckCollision(this.gameObject.transform, bricks[i]))
            {
                Direction();
                var platform = bricks[i].GetComponent<Platform>();
                platform.health -= 1;
                if (platform.health == 0)
                    bricks.Remove(bricks[i]);
            }
        }
    }

    private bool CheckCollision(Transform one, GameObject two)
    {

        _collisionX = one.position.x + d >= two.transform.position.x &&
                     two.transform.position.x + d >= one.position.x;
        _collisionY = one.position.y + d >= two.transform.position.y &&
                     two.transform.position.y + d >= one.position.y;

        return _collisionX && _collisionY;
    }

    private void CheckForTouchPlatform()
    {
        if (CheckCollision(this.gameObject.transform, Platform))
        {
            Direction();
        }
    }

    
    private void MoveBall()
    {
        gameObject.transform.Translate(SpeedX * Time.fixedDeltaTime, 0, 0);
    }

    private void Direction()
    {
        if (transform.eulerAngles.z < 60 && 30 < transform.eulerAngles.z)
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(300, 330));
        else if (transform.eulerAngles.z < 330 && 300 < transform.eulerAngles.z)
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(210, 240));
        else if (transform.eulerAngles.z < 240 && 210 < transform.eulerAngles.z)
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(120, 150));
        else if (transform.eulerAngles.z < 150 && 120 < transform.eulerAngles.z)
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(30, 60));
    }

    private void CheckPosition()
    {
        if (transform.position.x > 10f || transform.position.x < -10f)
            Direction();
        if (transform.position.y > 4.7f)
            Direction();
        else if (transform.position.y < -4.7f)
            Destroy(this.gameObject);
    }
}