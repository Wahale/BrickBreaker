using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Platform : MonoBehaviour
{
    public int health;
    public GameObject bonus;

    private void Update()
    {
        if (health == 0)
        {
            if (bonus != null)
            {
                var obj = Instantiate(bonus, transform.position, Quaternion.identity);
                var b = (Bonus.BonusEnum) Random.Range(0, 5);
                obj.GetComponent<Bonus>().brickBonus = b;
            }

            Destroy(this.gameObject);
        }
    }

       
}