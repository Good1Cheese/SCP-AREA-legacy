using UnityEngine;
using UnityEngine.InputSystem;

public class InputContainer : MonoBehaviour
{
    public MainControls Input { get; set; }
    public InputAction Movement => Input.Movement.Main;

    private void Awake()
    {
        Input = new MainControls();
        Input.Enable();
    }
}