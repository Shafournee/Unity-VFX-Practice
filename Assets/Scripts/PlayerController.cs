using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using EZCameraShake;

public class PlayerController : MonoBehaviour
{
    const float minForce = 2f;
    const float maxForce = 30f;
    const float maxDragBackDistance = 2f;
    const float minVelocity = 1f;
    const int shootingModifier = -5;

    float force;

    Rigidbody2D rigidBody;
    Vector2 forceDir;

    [SerializeField] GameObject dragBarCanvas = default;
    [SerializeField] GameObject fillBar = default;

    Vector2 startingDragPos;
    Vector2 dragPos;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        dragBarCanvas.SetActive(false);
    }

    private void Update()
    {
        ControlCharacter();

        if(Input.GetMouseButtonDown(1))
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        StopVelocity();
    }

    private void StopVelocity()
    {
        if(rigidBody.velocity.magnitude < minVelocity)
        {
            rigidBody.velocity = Vector2.zero;
        }
    }

    void ControlCharacter()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startingDragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragBarCanvas.SetActive(true);
        }

        if (Input.GetMouseButton(0))
        {
            ShowDragBarEachFrame();
        }

        if (Input.GetMouseButtonUp(0))
        {
            GameManager.instance.ModifyScore(shootingModifier);
            CalculateForceDir();
            dragBarCanvas.SetActive(false);
            StartCoroutine(AddForceToPlayer());
        }
    }

    void ShowDragBarEachFrame()
    {
        dragBarCanvas.transform.position = transform.position;
        dragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        forceDir = startingDragPos - dragPos;
        float angle = Mathf.Atan2(forceDir.y, forceDir.x) * Mathf.Rad2Deg;
        dragBarCanvas.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        float fillAmount = forceDir.magnitude / maxDragBackDistance;
        fillAmount = Mathf.Min(fillAmount, maxDragBackDistance);
        fillBar.GetComponent<Image>().fillAmount = fillAmount;
    }

    void CalculateForceDir()
    {
        force = (maxForce / maxDragBackDistance) * forceDir.magnitude + minForce;
        force = Mathf.Min(force, maxForce);
        forceDir = forceDir.normalized * maxForce;
    }

    IEnumerator AddForceToPlayer()
    {
        yield return new WaitForFixedUpdate();
        rigidBody.AddForce(forceDir * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float magnitude = rigidBody.velocity.magnitude / 10f;
        CameraShaker.Instance.ShakeOnce(magnitude, 10f, 0f, .5f);
    }
}
