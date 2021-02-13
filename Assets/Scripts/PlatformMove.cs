using System;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    private void Update()
    {
        if(Input.GetKey(KeyCode.A))
            Move(-1);
        if(Input.GetKey(KeyCode.D))
            Move(1);
        Check();
    }

    private void Move(int direction)
    {
        transform.Translate(Vector3.right * direction* 10 * Time.fixedDeltaTime);
    }
        
        
    private void Check()
    {
        var position = transform.position;
        position = new Vector3(Mathf.Clamp(position.x,-8.42f,8.51f),position.y,position.z);
        transform.position = position;
    }
}