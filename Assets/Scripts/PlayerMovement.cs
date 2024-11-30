using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xClampRange = 25f;
    [SerializeField] float yClampRange = 15f;
    [SerializeField] float controlRollFactor = 20f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float controlPitchFactor = 20f;
    Vector2 movementInput;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    void ProcessTranslation()
    {
        float xOffset = movementInput.x * controlSpeed * Time.deltaTime;
        float rawXPosition = transform.localPosition.x + xOffset;
        float clampedXPosition = Mathf.Clamp(rawXPosition, -xClampRange, xClampRange);

        float yOffset = movementInput.y * controlSpeed * Time.deltaTime;
        float rawYPosition = transform.localPosition.y + yOffset;
        float clampedYPosition = Mathf.Clamp(rawYPosition, -yClampRange, yClampRange);

        transform.localPosition = new Vector3(clampedXPosition, clampedYPosition, 0f);
    }

    void ProcessRotation()
    {
        float pitch = -controlPitchFactor * movementInput.y;
        float roll = -controlRollFactor * movementInput.x;
        Quaternion targetRotation = Quaternion.Euler(pitch, 0f, roll);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void OnMove(InputValue value)
    {
        movementInput = value.Get<Vector2>();
    }
}
