using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bonus : MonoBehaviour
{
    private Collider _platformCollider;
        
    private int[] speed = new[] {-2, 2};
    public enum BonusEnum
    {
        speed,
        ball,
        platform,
        none
    }

    public BonusEnum brickBonus;

    private void Start()
    {
        _platformCollider = GameObject.FindWithTag("Platform").GetComponent<Collider>();
    }

    private void FixedUpdate()
    {
        MoveBonus();
    }

    private void LateUpdate()
    {
        CheckForTouchBall();
    }

    private void CheckForTouchBall()
    {
        if (transform.GetComponent<Collider>().bounds.Intersects(_platformCollider.bounds))
        {
            BonusIs();
        }
    }
        
    private void BonusIs()
    {
        switch (brickBonus)
        {
            case BonusEnum.ball:
                AddBall();
                break;
            case BonusEnum.platform :
                PlatformChangeScale();
                break;
            case BonusEnum.speed :
                AddSpeed();
                break;
            case BonusEnum.none :
                break;
        }
    }

    private void MoveBonus()
    {
        transform.Translate(0,-1 * Time.fixedDeltaTime,0);
    }
        
    IEnumerator AddSpeed()
    {
        MainScript.SpeedX += speed[Random.Range(0, speed.Length)];
        yield return new WaitForSeconds(4f);
        MainScript.SpeedX = 4;
    }
        
    IEnumerator PlatformChangeScale()
    {
        MainScript.Platform.transform.localScale = new Vector3(30,5.683837f,0);
        yield return new WaitForSeconds(4f);
        MainScript.Platform.transform.localScale = new Vector3(23.69707f,5.683837f,0);
    }
        
    IEnumerator AddBall()
    {
        var b = Instantiate(MainScript.Ball, transform.position, new Quaternion(0, 0, 45,0));
        yield return new WaitForSeconds(4f);
        b.AddComponent<MainScript>();
        Destroy(b.gameObject);
    }
}