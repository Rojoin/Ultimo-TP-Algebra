using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float radius;
    [Tooltip("0 means no drag. 1 means instant stop")]
    [SerializeField] private float drag;
    [SerializeField] private Vector2 initVel;

    private Vector3 accel;
    private Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        accel = Vector3.zero;
        vel = new Vector3(initVel.x, initVel.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSize();

        Move();
        Drag();
    }

    public void AddImpulse(Vector2 impulse)
    {
        vel += new Vector3(impulse.x, impulse.y, 0);
    }

    public void AddImpulse(Vector2 dir, float force)
    {
        vel += new Vector3(dir.x, dir.y, 0) * force;
    }

    void Drag()
    {
        vel.x -= vel.x * drag;
        vel.y -= vel.y * drag;
    }

    void UpdateSize()
    {
        Vector2 newScale = new Vector2(radius, radius);
        transform.localScale = newScale;
    }

    void Move()
    {
        transform.position += vel * Time.deltaTime;
    }
}