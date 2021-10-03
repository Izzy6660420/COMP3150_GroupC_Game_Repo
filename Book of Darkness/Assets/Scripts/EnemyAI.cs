using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 70000f;
    public float startTimer = 2f;

    public Transform enemyGFX;
    public FieldOfView fov;
    bool stunned = false;

    public List<Transform> patrolPoints = new List<Transform>();
    Transform currentPoint;
    Transform target;
    Rigidbody2D rb;
    float nextDst = 2f;
    bool onPath = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = Player.instance.transform;
    }

    void FixedUpdate()
    {
        if (stunned)
            return;

        float dst = Vector2.Distance((Vector2)target.position, rb.position);
        if (dst < nextDst)
        {
            onPath = false;
        }
            

        if (Player.instance.CompareScene(transform.parent.name) && fov.visibleTargets.Count > 0 && !Player.instance.IsHiding())
        {
            // Delay upon seeing player
            target = Player.instance.transform;

            startTimer -= Time.fixedDeltaTime;
            if (startTimer <= 0)
            {
                
            }
        }
        else if (!onPath)
        {
            target = GetClosestPatrolPoint(target);
            onPath = true;
        }

        Vector2 dir = ((Vector2)target.position - rb.position).normalized;
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

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Torch"))
            stunned = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Torch"))
            stunned = false;
    }

    Transform GetClosestPatrolPoint(Transform currentPoint = null)
    {
        Transform closestPoint = null;
        float minDist = Mathf.Infinity;

        foreach (Transform point in patrolPoints)
        {
            float dist = Vector3.Distance(point.position, transform.position);
            if (dist < minDist && point != currentPoint)
            {
                closestPoint = point;
                minDist = dist;
            }
        }
        return closestPoint;
    }
}
