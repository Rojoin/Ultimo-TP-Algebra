using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BallCollision : MonoBehaviour
{
    [SerializeField] private Ball[] balls;

    [SerializeField] private float drag;
    [SerializeField] private float radius;
    [SerializeField] private float whiteRadius;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].GetComponent<Ball>().SetDrag(drag);
            if (i == 0)
            {
                balls[i].GetComponent<Ball>().SetRadius(whiteRadius);

            }
            balls[i].GetComponent<Ball>().SetRadius(radius);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < balls.Length - 1; i++)
        {
            for (int j = i; j < balls.Length; j++)
            {
                if (j == i)
                    continue;

                if (BallsColliding(balls[i], balls[j]))
                    CollideBalls(balls[i], balls[j]);
            }
        }
    }

    bool BallsColliding(Ball ball1, Ball ball2)
    {
        float distance =
            Mathf.Sqrt(
                Mathf.Pow(ball2.GetCenter().x - ball1.GetCenter().x, 2) +
                Mathf.Pow(ball2.GetCenter().y - ball1.GetCenter().y, 2));

        float ball1Radius = ball1.GetRadius();
        float ball2Radius = ball2.GetRadius();

        if (distance <= ball1Radius + ball2Radius)
        {
            ball2.SetPosition(ball1.GetCenter() + (ball1Radius + ball2Radius) *
                ((ball2.GetCenter() - ball1.GetCenter()).normalized));

            return true;
        }

        return false;
    }

    void CollideBalls(Ball ball1, Ball ball2)
    {
        Vector2 res = ball2.GetCenter() - ball1.GetCenter();

        Vector2 tan = new Vector2(-res.y, res.x);

        float tanDotProductBall1 = ball1.GetVelocity().x * tan.x + ball1.GetVelocity().y * tan.y;
        float tanDotProductBall2 = ball2.GetVelocity().x * tan.x + ball2.GetVelocity().y * tan.y;


        float normalDotProductBall1 = Vector2.Dot(ball1.GetVelocity(), res.normalized);
        float normalDotProductBall2 = Vector2.Dot(ball2.GetVelocity(), res.normalized);

        float momentum1 = (normalDotProductBall1 / (ball1.GetMass() * 2)) + normalDotProductBall2;
        float momentum2 = (normalDotProductBall2 / (ball2.GetMass() * 2)) + normalDotProductBall1;

        ball1.SetVelocity(tan * tanDotProductBall1 + res.normalized * momentum1);
        ball2.SetVelocity(tan * tanDotProductBall2 + res.normalized * momentum2);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(Vector3.back, Vector3.forward);
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].GetComponent<Ball>().SetDrag(drag);
            if (i == 0)
            {
                balls[i].GetComponent<Ball>().SetRadius(whiteRadius);

            }
            balls[i].GetComponent<Ball>().SetRadius(radius);
        }
    }
}