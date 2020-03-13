using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test_rotate : MonoBehaviour
{
    public float xAngle, yAngle, zAngle;

    private GameObject cube1, cube2;

    void Awake()
    {
        cube1 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube1.transform.position = new Vector3(0.75f, 0.0f, 0.0f);
        cube1.transform.Rotate(90.0f, 0.0f, 0.0f, Space.Self);
        cube1.GetComponent<Renderer>().material.color = Color.red;
        cube1.name = "Self";

        cube2 = GameObject.CreatePrimitive(PrimitiveType.Cube);
        cube2.transform.position = new Vector3(-0.75f, 0.0f, 0.0f);
        cube2.transform.Rotate(90.0f, 0.0f, 0.0f, Space.World);
        cube2.GetComponent<Renderer>().material.color = Color.green;
        cube2.name = "World";
    }

    //void Update()
    //{
    //    cube1.transform.Rotate(xAngle, yAngle, zAngle, Space.Self);
    //    cube2.transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    //}

    private Vector3 target = new Vector3(5.0f, 0.0f, 0.0f);

    void Update()
    {
        // Spin the object around the world origin at 20 degrees/second.
        transform.RotateAround(target, Vector3.up, 30 * Time.deltaTime);
    }
}
