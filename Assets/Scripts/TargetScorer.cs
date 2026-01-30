using UnityEngine;

public class TrackingScorer : MonoBehaviour
{
    public Transform target;
    public float targetY = 0.5f;

    public float hitRadius = 0.55f;
    public float scorePerSecond = 100f;

    public float score;
    public bool isHovering;

    void Update()
    {
        Vector3 mouseWorld;
        if (!TryGetMouseWorldOnPlane(targetY, out mouseWorld))
        {
            isHovering = false;
            return;
        }

        Vector3 tp = target.position;
        // XZ 평면 거리만 비교
        float dist = Vector2.Distance(new Vector2(mouseWorld.x, mouseWorld.z), new Vector2(tp.x, tp.z));

        isHovering = dist <= hitRadius;

        if (isHovering)
        {
            score += scorePerSecond * Time.deltaTime;
        }
    }

    bool TryGetMouseWorldOnPlane(float yPlane, out Vector3 hitPoint)
    {
        hitPoint = Vector3.zero;

        Camera cam = Camera.main;
        if (cam == null) return false;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0f, yPlane, 0f));

        if (!plane.Raycast(ray, out float enter)) return false;

        hitPoint = ray.GetPoint(enter);
        return true;
    }
}
