using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MainScript : MonoBehaviour
{
    public static GameObject Ball;
    public static GameObject Platform;
    private Collider _ballCollider, _colliderPlatform;
    private GameObject[] _bricks;
    public List<Collider> _collidersBricks ;
    public static int SpeedX = 4;
    bool _canChange = true;

    private void Start()
    {
        _colliderPlatform = GameObject.FindWithTag("Platform").GetComponent<Collider>();
        _bricks = GameObject.FindGameObjectsWithTag("Brick");
        Ball = transform.gameObject;
        Platform = _colliderPlatform.gameObject;
        _ballCollider = GetComponent<Collider>();

        for (int i = 0; i < _bricks.Length; i++)
        {
            _collidersBricks.Add(_bricks[i].GetComponent<Collider>());
        }
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
        if (!_canChange) return;
        for (int i = 0; i < _collidersBricks.Count; i++)
        {
            if (_collidersBricks[i].bounds.Intersects(_ballCollider.bounds))
            {
                Direction();
                var platform = _collidersBricks[i].GetComponent<Platform>();
                platform.health -= 1;
                if (platform.health == 0)
                    _collidersBricks.Remove(_collidersBricks[i]);
                _canChange = false;
            }
            else
                Invoke(nameof(Invoke), 0.1f);
        }
    }

    private void CheckForTouchPlatform()
    {
        if (!_canChange) return;
        if (_colliderPlatform.bounds.Intersects(_ballCollider.bounds))
        {
            Direction();
            _canChange = false;
        }
        else
            Invoke(nameof(Invoke), 0.1f);
    }



    private void MoveBall()
    {
        _ballCollider.transform.Translate(SpeedX * Time.fixedDeltaTime, 0, 0);
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

    private void Invoke()
    {
        _canChange = true;
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