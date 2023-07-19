using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddDensity : MonoBehaviour
{
    public DensityField densityField;
    public Vector3 intPos, tempVector3;
    public Transform targetFinger, indexFinger, targetThumb;
    public float scale = 10.0f, sqrtTwoTimesTwo = 2.82842712475f;
    private float distance, distance2;
    private void Awake()
    {
        StartCoroutine("WaitForFinger");
    }

    private IEnumerator WaitForFinger()
    {
        yield return new WaitForSeconds(1.0f);
        targetFinger = transform.FindChildRecursive("Hand_IndexTip");
        indexFinger = transform.FindChildRecursive("Hand_MiddleTip");
        targetThumb = transform.FindChildRecursive("Hand_ThumbTip");
    }

    private void FixedUpdate()
    {
        if (densityField != null && targetFinger != null)
        {
            for(int xi = 0; xi < densityField.dimension; xi++)
            {
                for(int yi = 0; yi < densityField.dimension; yi++)
                {
                    for(int zi = 0; zi < densityField.dimension; zi++)
                    {
                        tempVector3.x = xi;
                        tempVector3.y = yi;
                        tempVector3.z = zi;
                        distance = Mathf.Abs(Vector3.Distance(targetFinger.position, tempVector3 / 10.0f));
                        if(Vector3.Distance(targetFinger.position, targetThumb.position) < 0.01f && distance < 0.1f)
                                densityField.AddToDensity((0.3f - distance) * Time.deltaTime, xi, yi, zi);
                        distance2 = Mathf.Abs(Vector3.Distance(indexFinger.position, tempVector3 / 10.0f));

                        if (Vector3.Distance(indexFinger.position, targetThumb.position) < 0.02f && distance2 < 0.1f)
                            densityField.AddToDensity((distance2 - 0.4f) * Time.deltaTime, xi, yi, zi);
                    }
                }
            }
            /*
            if(intPos.x < 0 || intPos.x >= densityField.dimension
                || intPos.y < 0 || intPos.y >= densityField.dimension
                || intPos.z < 0 || intPos.z >= densityField.dimension)
            {
                //Debug.Log("Out of bounds");
            }
            else
            {
                for(int xi = -1; xi < 3; xi++)
                {
                    for(int yi = -1; yi < 3; yi++)
                    {
                        for(int zi = -1; zi < 3; zi++)
                        {
                            if((int)intPos.x + xi >= 0 && (int)intPos.x + xi < densityField.dimension
                                && (int)intPos.y + yi >= 0 && (int)intPos.y + yi < densityField.dimension
                                && (int)intPos.z + zi >= 0 && (int)intPos.z + zi < densityField.dimension)
                            {
                                tempVector3.x = xi; tempVector3.y = yi; tempVector3.z = zi;
                                densityField.SetDensity(0.1f * (sqrtTwoTimesTwo - Mathf.Abs(Vector3.Distance(target.position, tempVector3))), (int)intPos.x + xi, (int)intPos.y + yi, (int)intPos.z + zi);
                                Debug.Log(Mathf.Abs(Vector3.Distance(target.position, tempVector3)));
                            }
                        }
                    }
                }
            }
            */
        }
    }
}
