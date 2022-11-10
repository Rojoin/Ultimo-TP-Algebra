using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNShoot : MonoBehaviour
{
   public float power = 10f;
   public Rigidbody2D rb;
   public Vector2 minPower;
   public Vector2 maxPower;
   private LineTrajectory lt;

   private Camera cam;
   private Vector2 force;
   private Vector3 startPoint;
   private Vector3 endPoint;

   private void Start()
   {
       cam = Camera.main;
       lt = GetComponent<LineTrajectory>();
   }

   private void Update()
   {
       if (Input.GetMouseButtonDown(0))
       {
           startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
           startPoint.z = 15;
           Debug.Log(startPoint);
       }

       if (Input.GetMouseButton(0))
       {
           Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
           currentPoint.z = 15f;
           lt.RenderLine(startPoint,currentPoint);
       }
       if (Input.GetMouseButtonUp(0))
       {
           endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
           endPoint.z = 15;

           force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x),
               Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

           //Cambiar por metodo sin RB
           rb.AddForce(force * power, ForceMode2D.Impulse);

           lt.EndLine();
       }
   }
   bool circleCircle(float c1x, float c1y, float c1r, float c2x, float c2y, float c2r)
   {

       // get distance between the circle's centers
       // use the Pythagorean Theorem to compute the distance
       float distX = c1x - c2x;
       float distY = c1y - c2y;
       float distance = Mathf.Sqrt((distX * distX) + (distY * distY));

       // if the distance is less than the sum of the circle's
       // radii, the circles are touching!
       if (distance <= c1r + c2r)
       {
           return true;
       }
       return false;
   }
}
