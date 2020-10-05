using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10;
    [NaughtyAttributes.InputAxis] public string walkInput;

    private CharacterController cc;
    private Animator anim;
    private Vector3 dir = new Vector3(1, 0, 0);
    private Vector3 velocity = Vector3.zero;

    private Vector3 originalPos;
    private Quaternion originalRot;


    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponentInChildren<Animator>();

        originalPos = transform.position;
        originalRot = transform.rotation;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        float value = Input.GetAxis(walkInput);

        velocity = dir * speed * value;

        anim.SetFloat("Speed", Mathf.Abs(value));

        cc.Move(velocity * Time.deltaTime);

        transform.LookAt(transform.position + velocity, transform.up);
    }


    public void ResetPosition()
    {
        transform.position = originalPos;
        transform.rotation = originalRot;
    }
}
