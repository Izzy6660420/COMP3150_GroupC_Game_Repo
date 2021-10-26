using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    float forgetTimer = 3f;
    public GameObject shadow;
    public SpriteRenderer sprite;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = Player.instance.transform;
    }

    void OnAwake()
    {
        //StartCoroutine(Fade(Color.white));
    }

    void Update()
    {
        shadow.SetActive(true);
        if (DimensionController.instance.dimensionInf() == "nightmare")
            shadow.SetActive(false);
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

        if (Player.instance.CompareScene(transform.parent.name) && fov.FindVisibleTargetsInLayer("Player") && !Player.instance.IsHiding())
        {
            target = Player.instance.transform;
            forgetTimer = 3f;

            startTimer -= Time.fixedDeltaTime;
            if (startTimer <= 0)
            {

            }
        }
        else if (!onPath)
        {
            forgetTimer -= Time.fixedDeltaTime;
            if (forgetTimer <= 0)
            {
                StartCoroutine(Fade(Color.clear, true));
            }
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

    Transform GetClosestPatrolPoint(bool chasePlayer, Transform currentPoint = null)
    {
        if (chasePlayer)
            return Player.instance.transform;

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

    public IEnumerator Stun(float t)
    {
        stunned = true;
        yield return new WaitForSeconds(t);
        stunned = false;
    }

    IEnumerator Fade(Color c, bool kill = false)
    {
        var percent = 0f;
        var initColor = sprite.color;

        while (percent < 1)
        {
            percent += Time.deltaTime;
            sprite.color = Color.Lerp(initColor, c, percent);
            yield return null;
        }
        if (kill) Destroy(gameObject);
    }
}
