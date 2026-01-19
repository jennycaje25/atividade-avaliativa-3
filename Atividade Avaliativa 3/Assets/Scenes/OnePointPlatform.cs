using UnityEngine;

public class OnePointPlatform : MonoBehaviour
{
    public Transform targetPoint;
    public float speed = 3f;
    public float arriveThreshold = 0.05f;

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool goingToTarget = true;

    void Start()
    {
        startPos = transform.position;
        targetPos = targetPoint.position;
    }

    void Update()
    {
        Vector3 desired = goingToTarget ? targetPos : startPos;

        transform.position = Vector3.MoveTowards(
            transform.position,
            desired,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, desired) < arriveThreshold)
        {
            goingToTarget = !goingToTarget;
        }
    }

    // --- AQUI O PLAYER GRUDA NA PLATAFORMA ---
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(transform);
        }
    }

    // --- AQUI ELE DESGRUDA AO SAIR ---
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            collision.collider.transform.SetParent(null);
        }
    }
}