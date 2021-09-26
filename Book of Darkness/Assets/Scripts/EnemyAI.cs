using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    Transform target;

    public float speed = 2000f;
    public float nextDist = 3f;
    public float startTimer = 2f;

    public Transform enemyGFX;
    public FieldOfView fov;

    Path path;
    int currentWaypoint = 0;
    //bool reachedEnd = false;
    bool stunned = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        target = Player.instance.transform;

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void FixedUpdate()
    {
        if (path == null || stunned)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            //reachedEnd = true;
            return;
        }
        else
        {
            //reachedEnd = false;
        }

        if (Player.instance.CompareScene(transform.parent.name) && fov.visibleTargets.Count > 0)
        {
            // Delay upon seeing player
            startTimer -= Time.fixedDeltaTime;
            if (startTimer <= 0)
            {
                Vector2 dir = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
                Vector2 force = dir * speed * Time.deltaTime;

                rb.AddForce(force);
                if (force.x >= 0.01f)
                {
                    enemyGFX.localScale = new Vector3(-1f, 1f, 1f);
                    fov.FlipFOV("Right");
                }
                else if (force.x <= -0.01f)
                {
                    enemyGFX.localScale = new Vector3(1f, 1f, 1f);
                    fov.FlipFOV("Left");
                }
            }
        }
        else
        {
            // Return to patrol path
        }

        float dist = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (dist < nextDist)
        {
            currentWaypoint++;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Torch"))
        {
            stunned = true;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Torch"))
        {
            stunned = false;
        }
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
}
