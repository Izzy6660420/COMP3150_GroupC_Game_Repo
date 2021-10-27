using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Torch : MonoBehaviour
{
    private Player player;
    public Transform arm;
    public Light2D torchLight;
    public Light2D torchLightBG;
    public PolygonCollider2D polyCol;
    public GameObject gameObj;

    public float power = 20.0f;
    private float maxPower = 20.0f;
    private float minPower = 0.0f;
    public float powerDrain = 1.0f;
    public AudioClip buttonClickSfx;
    float defaultIntensity;

    private TorchUI torchBar;
    private bool usable = true;

    List<EnemyAI> enemiesInLight = new List<EnemyAI>();

    void Start()
    {
        torchBar = TorchUI.instance;
        torchBar.SetBatteryCeiling(maxPower, minPower);
        player = Player.instance;
        defaultIntensity = torchLight.intensity;
    }

    void Update()
    {
        Vector2 mPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 dir = (mPos - (Vector2)arm.position).normalized;

        if (player.facingRight)
        {
            arm.right = dir;
        }
        else
        {
            arm.right = -dir;
        }

        if (torchLight.enabled)
        {
            power -= Time.deltaTime * powerDrain;
            torchBar.SetBattery(power);
        }

        if (power > maxPower)
        {
            power = maxPower;
        }

        if (Input.GetMouseButtonDown(2) && torchLight.enabled)
        {
            
            StartCoroutine(Fade());
            foreach (var enemy in enemiesInLight)
                StartCoroutine(enemy.Stun(2f));

        }

        if (power < minPower)
        {
            power = minPower;
            SetActive(false);
            usable = false;
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
        torchLight.enabled = b;
        torchLightBG.enabled = b;
        polyCol.enabled = b;
        gameObj.SetActive(b);
        AudioManager.instance.PlaySound(buttonClickSfx, transform.position);
    }

    public void AddPower(float percent)
    {
        power += maxPower * percent;
    }

    public bool usableBool()
    {
        return usable;
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
