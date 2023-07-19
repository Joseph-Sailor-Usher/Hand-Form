using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliderController : MonoBehaviour
{
    public float minPosition = 0, maxPosition = 0.77f;
    public BoxCollider boxCollider;
    public GameObject sliderPrefab;
    private Vector3 tempVector = Vector3.zero;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand"))
        {
            if(other.transform.position.x >= minPosition && other.transform.position.x < maxPosition)
            {
                tempVector.x = minPosition;
                transform.position = tempVector;
            }
        }
    }
}
