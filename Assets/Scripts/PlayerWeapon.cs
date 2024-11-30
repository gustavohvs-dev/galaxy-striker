using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] GameObject[] lasers;
    [SerializeField] RectTransform crosshair;
    [SerializeField] Transform targetPoint;
    [SerializeField] float targetDistance = 100f;
    [SerializeField] float joystickSpeed = 1000f; // Sensibilidade do joystick
    bool isFiring = false;
    Vector3 cursorPosition;
    Vector2 aimInput;



    void Start()
    {
        Cursor.visible = false;
        cursorPosition = Input.mousePosition;
    }

    void Update()
    {
        ProcessFiring();
        MoveCrosshair();
        MoveTargetPoint();
        AimLasers();
    }

    public void OnFire(InputValue value)
    {
        isFiring = value.isPressed;
    }

    public void OnAim(InputValue value)
    {
        aimInput = value.Get<Vector2>();
    }

    private void ProcessFiring()
    {
        foreach (GameObject laser in lasers)
        {
            var emmissionModule = laser.GetComponent<ParticleSystem>().emission;
            emmissionModule.enabled = isFiring;
        }
        
    }

    private void MoveCrosshair()
    {
        // Mouse
        crosshair.position = Input.mousePosition;

         // Joystick
        Vector3 joystickDelta = new Vector3(aimInput.x, aimInput.y, 0) * joystickSpeed * Time.deltaTime;
        cursorPosition += joystickDelta;

        cursorPosition.x = Mathf.Clamp(cursorPosition.x, 0, Screen.width);
        cursorPosition.y = Mathf.Clamp(cursorPosition.y, 0, Screen.height);

        crosshair.position = cursorPosition;
    }

    private void MoveTargetPoint()
    {
        Vector3 targetPointPosition = new Vector3(crosshair.position.x, crosshair.position.y, targetDistance);
        targetPoint.position = Camera.main.ScreenToWorldPoint(targetPointPosition);
    }

    private void AimLasers()
    {
        foreach(GameObject laser in lasers)
        {
            Vector3 fireDirection = targetPoint.position - this.transform.position;
            Quaternion rotationToTarget = Quaternion.LookRotation(fireDirection);
            laser.transform.rotation = rotationToTarget;
        }
    }

}
