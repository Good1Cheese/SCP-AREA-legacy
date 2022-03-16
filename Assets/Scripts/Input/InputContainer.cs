using UnityEngine;
using UnityEngine.InputSystem;

public class InputContainer : MonoBehaviour
{
    public MainControls Input { get; set; }
    public InputAction Movement => Input.Movement.Main;
    public InputAction Run => Input.Movement.Run;
    public InputAction SlowWalk => Input.Movement.SlowWalk;
    public InputAction SlowWalkRun => Input.Movement.SlowWalkRun;

    private void Awake()
    {
        Input = new MainControls();
        Input.Enable();
    }
}