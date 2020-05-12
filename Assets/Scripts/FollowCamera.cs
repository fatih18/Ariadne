using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform target;


    public float distance = 10f;
    public float height = 5f;
    public float heightDumping = 2.0f;
    public float rotationDamping = 3.0f;
    public Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!target) return;

        float wantedRotationAngle = target.eulerAngles.y;
        float wantedHeight = target.position.y + height;
        //
        float currentRotationAngele = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        currentRotationAngele = Mathf.LerpAngle(currentRotationAngele, wantedRotationAngle, rotationDamping * Time.deltaTime);
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, heightDumping * Time.deltaTime); //distance from the cannon ball

        var currentRotation = Quaternion.Euler(0, currentRotationAngele, 0);

        transform.position = target.position;
        transform.position -= Vector3.forward * distance;

        transform.position = new Vector3(transform.position.x, currentHeight, transform.position.z);

        transform.LookAt(target); //pers

    }

}
