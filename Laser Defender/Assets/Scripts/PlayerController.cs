using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    Vector3 moveInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Vector2 size;
    RectTransform rt;
    private void Start()
    {
        InitBounds();
        //var p1 = transform.TransformPoint(0, 0, 0);
        //var p2 = transform.TransformPoint(1, 1, 0);
        //size = p2 - p1;
        size = GetComponent<SpriteRenderer>().bounds.size / 2;
    }
    private void Update()
    {
        Move();
    }

    void InitBounds()
    {
        Camera mainCamera = Camera.main;
        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1f, 1f));
    }

    void Move()
    {
        Vector3 delta = moveInput * moveSpeed * Time.deltaTime;
        Vector2 newPos = new Vector2();
        newPos.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + size.x, maxBounds.x - size.x);
        newPos.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + size.y, maxBounds.y - size.y);
        transform.position = newPos;
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }
}
