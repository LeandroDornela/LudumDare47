using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public Transform rightLimitTransform;
    public Transform leftLimitTransform;

    public Vector3 rightLimit;
    public Vector3 leftLimit;


    private void Awake()
    {
        rightLimit = rightLimitTransform.position;
        leftLimit = leftLimitTransform.position;
    }


    // Update is called once per frame
    void Update()
    {
        if(transform.position.x >= rightLimit.x && target.position.x > rightLimit.x)
        {
            transform.position = rightLimit;
        }
        else if(transform.position.x <= leftLimit.x && target.position.x < leftLimit.x)
        {
            transform.position = leftLimit;
        }
        else
        {
            float x = Mathf.Lerp(transform.position.x, target.position.x, 10 * Time.deltaTime);
            transform.position = new Vector3(x, transform.position.y, transform.position.z);
        }
    }
}
