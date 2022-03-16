// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Input/MainControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @MainControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainControls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""37c43c7e-98cb-4932-818a-696a76c5c153"",
            ""actions"": [
                {
                    ""name"": ""Main"",
                    ""type"": ""Value"",
                    ""id"": ""be1a00f3-65c5-41f2-a00d-73fbd7c515ca"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""135ab6d8-43ea-4f26-8dd9-10fcdf04e1c6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SlowWalk"",
                    ""type"": ""Button"",
                    ""id"": ""99adc7c9-3eff-4a57-a392-30a3fef7f69b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SlowWalkRun"",
                    ""type"": ""Button"",
                    ""id"": ""340d0337-36c2-4abe-8aab-a4de5310aa51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""dd95f25e-fca4-4730-8769-f6cd8d12c532"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Main"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""ca13c2b9-2c54-4459-9c76-d34f3b72eccc"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Main"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""c8653732-0521-439c-9077-97ef53422e4e"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Main"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""29dbc8ba-ef66-4efb-aa98-ed1cd6c9b830"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Main"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b2330435-d2b8-4b92-ab8d-47627aeff269"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Main"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cb5dc32a-a7a7-4e57-b3de-dba86bff1387"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""684e5c76-9d2f-4f04-9fdb-5add4a9443de"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlowWalk"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With One Modifier"",
                    ""id"": ""e0b9fd4f-e747-4368-a3e8-1949ae5e9ef8"",
                    ""path"": ""ButtonWithOneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlowWalkRun"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""cd3f205d-e5af-4514-8b4a-20be7199844e"",
                    ""path"": ""<Keyboard>/c"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlowWalkRun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""054c2487-2fa3-4c7f-924c-cc84b7856ecd"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SlowWalkRun"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Main = m_Movement.FindAction("Main", throwIfNotFound: true);
        m_Movement_Run = m_Movement.FindAction("Run", throwIfNotFound: true);
        m_Movement_SlowWalk = m_Movement.FindAction("SlowWalk", throwIfNotFound: true);
        m_Movement_SlowWalkRun = m_Movement.FindAction("SlowWalkRun", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Main;
    private readonly InputAction m_Movement_Run;
    private readonly InputAction m_Movement_SlowWalk;
    private readonly InputAction m_Movement_SlowWalkRun;
    public struct MovementActions
    {
        private @MainControls m_Wrapper;
        public MovementActions(@MainControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Main => m_Wrapper.m_Movement_Main;
        public InputAction @Run => m_Wrapper.m_Movement_Run;
        public InputAction @SlowWalk => m_Wrapper.m_Movement_SlowWalk;
        public InputAction @SlowWalkRun => m_Wrapper.m_Movement_SlowWalkRun;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Main.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMain;
                @Main.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMain;
                @Main.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMain;
                @Run.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnRun;
                @SlowWalk.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlowWalk;
                @SlowWalk.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlowWalk;
                @SlowWalk.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlowWalk;
                @SlowWalkRun.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlowWalkRun;
                @SlowWalkRun.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlowWalkRun;
                @SlowWalkRun.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnSlowWalkRun;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Main.started += instance.OnMain;
                @Main.performed += instance.OnMain;
                @Main.canceled += instance.OnMain;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @SlowWalk.started += instance.OnSlowWalk;
                @SlowWalk.performed += instance.OnSlowWalk;
                @SlowWalk.canceled += instance.OnSlowWalk;
                @SlowWalkRun.started += instance.OnSlowWalkRun;
                @SlowWalkRun.performed += instance.OnSlowWalkRun;
                @SlowWalkRun.canceled += instance.OnSlowWalkRun;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);
    public interface IMovementActions
    {
        void OnMain(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnSlowWalk(InputAction.CallbackContext context);
        void OnSlowWalkRun(InputAction.CallbackContext context);
    }
}
