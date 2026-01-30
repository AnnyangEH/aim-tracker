using UnityEngine;

public class TargetMover : MonoBehaviour
{
    public float speed = 4f;
    public float directionChangeInterval = 0.5f;
    public float moveRange = 4f;   // Plane 10x10 기준 4 추천
    public float yHeight = 0.5f;   // 타겟 높이 고정

    Vector3 direction;
    float timer;

    void Start()
    {
        PickNewDirection();
        FixHeight();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= directionChangeInterval)
        {
            PickNewDirection();
            timer = 0f;
        }

        transform.position += direction * speed * Time.deltaTime;
        FixHeight();

        // 사각형 경계 안으로 clamp + 튕김
        Vector3 p = transform.position;

        if (p.x <= -moveRange || p.x >= moveRange) direction.x *= -1;
        if (p.z <= -moveRange || p.z >= moveRange) direction.z *= -1;

        p.x = Mathf.Clamp(p.x, -moveRange, moveRange);
        p.z = Mathf.Clamp(p.z, -moveRange, moveRange);
        transform.position = p;
    }

    void PickNewDirection()
    {
        direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        if (direction.sqrMagnitude < 0.01f) direction = Vector3.right;
    }

    void FixHeight()
    {
        Vector3 p = transform.position;
        p.y = yHeight;
        transform.position = p;
    }
}
