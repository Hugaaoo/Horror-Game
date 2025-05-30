using UnityEngine;

public class HeadBob : MonoBehaviour
{
    public CharacterController controller; // Ton Player ici
    public float bobSpeed = 8f;
    public float bobAmount = 0.05f;
    public float smooth = 6f;

    private Vector3 startPosition;
    private float timer;

    void Start()
    {
        startPosition = transform.localPosition; // position de départ du camera holder
    }

    void Update()
    {
        if (controller == null) return;

        if (controller.velocity.magnitude > 0.1f && controller.isGrounded)
        {
            timer += Time.deltaTime * bobSpeed;
            float bobOffsetY = Mathf.Sin(timer) * bobAmount;
            float bobOffsetX = Mathf.Cos(timer * 2) * bobAmount * 0.5f; // un peu de mouvement latéral

            Vector3 targetPosition = startPosition + new Vector3(bobOffsetX, bobOffsetY, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, Time.deltaTime * smooth);
        }
        else
        {
            timer = 0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPosition, Time.deltaTime * smooth);
        }
    }
}
