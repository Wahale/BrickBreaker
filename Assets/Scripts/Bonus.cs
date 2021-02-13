using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bonus : MonoBehaviour
{
    private float d = 0.5f;
    private bool _isAdding = true;
        
    private int[] speed = new[] {-2, 2};
    public enum BonusEnum
    {
        Speed,
        Ball,
        Platform,
        None
    }

    public BonusEnum brickBonus;
    

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
        if (CheckCollision(this.gameObject.transform,MainScript.Ball.transform) && _isAdding)
        {
            BonusIs();
            Debug.Log("Ok");
        }
    }
        
    private void BonusIs()
    {
        _isAdding = false;
        switch (brickBonus)
        {
            case BonusEnum.Ball:
                AddBall();
                break;
            case BonusEnum.Platform :
                PlatformChangeScale();
                break;
            case BonusEnum.Speed :
                AddSpeed();
                break;
            case BonusEnum.None :
                Destroy(this.gameObject);
                break;
        }
    }
    
    private bool CheckCollision(Transform one, Transform two)
    {
        bool collisionX = one.position.x +d >= two.position.x &&
                          two.position.x +d >= one.position.x;
        bool collisionY= one.position.y +d >= two.position.y &&
                         two.position.y +d >= one.position.y;
        return collisionX && collisionY;
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
        Destroy(this.gameObject);
    }
        
    IEnumerator PlatformChangeScale()
    {
        MainScript.Platform.transform.localScale = new Vector3(30,5.683837f,0);
        yield return new WaitForSeconds(4f);
        MainScript.Platform.transform.localScale = new Vector3(23.69707f,5.683837f,0);
        Destroy(this.gameObject);
    }
        
    IEnumerator AddBall()
    {
        var b = Instantiate(MainScript.Ball, transform.position, new Quaternion(0, 0, 45,0));
        yield return new WaitForSeconds(4f);
        b.AddComponent<MainScript>();
        Destroy(b.gameObject);
        Destroy(this.gameObject);
    }
}