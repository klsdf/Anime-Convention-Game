using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ScaleObj
{
    public Transform objTransform;
    public Vector3 originScale;
}



//只需要给objTransform赋值即可，originScale会自动读取
public class ScenePerspective : MonoBehaviour
{




    public Transform firstChildObject;
    public Transform secondChildObject;
    public Transform thirdChildObject;
    public ScaleObj[] objs;

    private Vector3 intersect;//玩家和直线的垂线的交点


    private float dist1;
    private float dist2;
    private float dist3;

    private float scaleSize;



    //控制玩家移动速度
    private float playerSpeed;

    private DemoCharacterController playerController;


    private ScalePointSize pointSize1;
    private ScalePointSize pointSize2;
    private ScalePointSize pointSize3;

    // Start is called before the first frame update
    void Start()
    {
        pointSize1 = firstChildObject.gameObject.GetComponent<ScalePointSize>();
        pointSize2 = secondChildObject.gameObject.GetComponent<ScalePointSize>();

        if (thirdChildObject != null)
        {
            pointSize3 = thirdChildObject.gameObject.GetComponent<ScalePointSize>();
        }


        foreach (ScaleObj scaleObj in objs)
        {
            scaleObj.originScale = scaleObj.objTransform.localScale;

            if (scaleObj.objTransform.gameObject.GetComponent<DemoCharacterController>() != null)
            {
                playerController = scaleObj.objTransform.gameObject.GetComponent<DemoCharacterController>();
                playerSpeed = playerController.speed;
            }

        }
    }



    // Update is called once per frame
    void Update()
    {

        //如果只有两个点，进入直线的模式
        if (thirdChildObject == null)
        {

            foreach (ScaleObj t in objs)
            {
                两点缩放(t);
            }
        }
        else
        {
            foreach (ScaleObj t in objs)
            {
                //如果有三个点，进入曲线的模式
                三点缩放(t);

            }


        }


    }



    void 两点缩放(ScaleObj scaleObj)
    {

        Transform playerObj = scaleObj.objTransform;

        //当有两条线的时候
        Vector3 start = firstChildObject.position;
        Vector3 end = secondChildObject.position;
        //计算玩家到直线的垂线
        Vector3 dir = (end - start).normalized;

        Vector3 vec = playerObj.position - start;
        float t = Vector3.Dot(vec, dir);
        intersect = start + t * dir;



        //判断点是否在在线段内
        dist1 = Vector3.Distance(intersect, firstChildObject.position);
        dist2 = Vector3.Distance(intersect, secondChildObject.position);

        dist3 = Vector3.Distance(firstChildObject.position, secondChildObject.position);

        if (Mathf.Abs(dist1 + dist2 - dist3) < 0.1f)
        {
            //Debug.Log("点在线内");

            float proportion = dist1 / dist3;


            scaleSize = Mathf.Lerp(pointSize1.scaleSize, pointSize2.scaleSize, proportion);
            // print(playerObj.localScale.x);
            playerObj.localScale = new Vector3(scaleObj.originScale.x * scaleSize, scaleObj.originScale.y * scaleSize, scaleObj.originScale.z * scaleSize);

        }
        else
        {
            //Debug.Log("点在线外");
        }

    }


    void 三点缩放(ScaleObj playerObj)
    {


        float playerT = TFromPointToBezier(playerObj.objTransform.position);
        // float point1T = 0;
        float point2T = TFromPointToBezier(secondChildObject.position);
        // float point3T = 1;
        // print(point2T);
        if (playerT < point2T)
        {
            float proportion = playerT / point2T;
            scaleSize = Mathf.Lerp(pointSize1.scaleSize, pointSize2.scaleSize, proportion);
        }
        else
        {

            float proportion = (playerT - point2T) / (1 - point2T);
            scaleSize = Mathf.Lerp(pointSize2.scaleSize, pointSize3.scaleSize, proportion);
        }

        playerObj.objTransform.localScale = new Vector3(playerObj.originScale.x * scaleSize, playerObj.originScale.y * scaleSize, playerObj.originScale.z * scaleSize);

        playerController.speed = playerSpeed *scaleSize;
        


    }

    void OnDrawGizmos()
    {
        //绘制两点之间的线段
        Gizmos.color = Color.red;

        Gizmos.DrawLine(firstChildObject.position, secondChildObject.position);

        // Gizmos.DrawLine(playerObj.position, intersect);
        if (thirdChildObject == null)
        {
            return;
        }


        //绘制贝塞尔曲线
        // Transform point0, point1, point2;
        Gizmos.color = Color.white;
        for (float t = 0; t <= 1; t += 0.05f)
        {
            Vector3 p = CalculateBezierPoint(t, firstChildObject.position, secondChildObject.position, thirdChildObject.position);
            Gizmos.DrawSphere(p, 0.1f);
        }
        // float closestT = TFromPointToBezier(playerObj.position);

        // Gizmos.DrawLine(playerObj.position, CalculateBezierPoint(closestT, firstChildObject.position, secondChildObject.position, thirdChildObject.position));



    }

    //计算一个点到贝塞尔的最近点的T
    float TFromPointToBezier(Vector3 point)
    {
        float closestT = 0;
        float closestDistance = float.MaxValue;

        // Sample points along the bezier curve and find the closest to the player
        for (float t = 0; t <= 1; t += 0.01f)
        {
            Vector3 p = CalculateBezierPoint(t, firstChildObject.position, secondChildObject.position, thirdChildObject.position);
            float distance = Vector3.Distance(point, p);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestT = t;
            }
        }
        return closestT;
    }



    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; //term 1
        p += 2 * u * t * p1; //term 2
        p += tt * p2; //term 3

        return p;
    }
}
