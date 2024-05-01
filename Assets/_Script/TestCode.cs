using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCode : MonoBehaviour
{
    [SerializeField] private GameObject testObject;
    [SerializeField] private GameObject plainGrid;

    private void Start()
    {
        testObject.transform.position = new Vector3(0.86f,0, 0f);
    }

    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Grid grid = plainGrid.GetComponent<Grid>(); 
            testObject.transform.position += new Vector3(0.86f, 0, 1.5f);
            testObject.transform.position = grid.WorldToCell(grid.WorldToCell(testObject.transform.position));
        }
    }
}
