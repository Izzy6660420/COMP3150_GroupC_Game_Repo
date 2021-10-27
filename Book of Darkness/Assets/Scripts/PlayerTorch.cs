using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using UnityEngine.EventSystems;

public class PlayerTorch : MonoBehaviour
{
    public static PlayerTorch instance;
    void Awake() { if (instance == null) instance = this; }

    private Player player;
    public Transform arm;
    public Light2D torchLight;
    public Light2D torchLightBG;
    public PolygonCollider2D polyCol;
    public GameObject gameObj;

    public float power;
    public float maxPower = 20.0f;
    public float powerDrain = 1.0f;

    public AudioClip buttonClickSfx;
    float defaultIntensity;

    List<EnemyAI> enemiesInLight = new List<EnemyAI>();

    void Start()
    {
        power = maxPower;
        player = Player.instance;
        defaultIntensity = torchLight.intensity;
        SetActive(false);
    }

    void Update()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mPos - (Vector2)arm.position).normalized;
        arm.right = player.facingRight ? dir : -dir;

        if (Input.GetButtonDown(InputAxes.Torch) && power > 0)
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            SetActive(!torchLight.enabled);
        }

        if (torchLight.enabled) power -= Time.deltaTime * powerDrain;

        power = Mathf.Clamp(power, 0, maxPower);
        if (power > maxPower) { power = maxPower; }

        if (power <= 0) { SetActive(false); }

        if (Input.GetMouseButtonDown(2) && torchLight.enabled)
        {
            StartCoroutine(Fade());
            foreach (var enemy in enemiesInLight)
                StartCoroutine(enemy.Stun(2f));
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemies"))
            enemiesInLight.Add(col.gameObject.GetComponent<EnemyAI>());
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemies"))
            enemiesInLight.Remove(col.gameObject.GetComponent<EnemyAI>());
    }

    public void SetActive(bool b)
    {
        if (torchLight.enabled == b) return;

        torchLight.enabled = b;
        torchLightBG.enabled = b;
        polyCol.enabled = b;
        gameObj.SetActive(b);
        AudioManager.instance.PlaySound(buttonClickSfx, transform.position);
    }

    public float AddPower(float n)
    {
        power += n;
        return power;
    }

    public float GetPercent()
    {
        return power / maxPower;
    }

    IEnumerator Fade()
    {
        AudioManager.instance.PlaySound("Flash", transform.position, 0.3f);

        yield return new WaitForSeconds(0.5f);

        power /= 2;
        torchLight.intensity = 5f;

        while (torchLight.intensity > defaultIntensity)
        {
            torchLight.intensity -= Time.deltaTime * 20;
            yield return null;
        }
    }
}
