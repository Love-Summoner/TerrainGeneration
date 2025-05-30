//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/scripts/controls/player_controls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Player_controls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Player_controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""player_controls"",
    ""maps"": [
        {
            ""name"": ""in_game_actions"",
            ""id"": ""a16c3c71-2c77-46ac-ad59-b0b6439e25b5"",
            ""actions"": [
                {
                    ""name"": ""movement"",
                    ""type"": ""Value"",
                    ""id"": ""78377bde-e76d-4213-a50b-6eee17eddcdb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""cast_spell"",
                    ""type"": ""Button"",
                    ""id"": ""e9332f80-e5f5-4011-98d8-16a066cdf774"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""1b20166e-2412-4b0a-8f5f-fcd72e16aa22"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""hotbar"",
                    ""type"": ""Button"",
                    ""id"": ""d83e62ab-4162-42ee-b274-e892d7545047"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""pickup/interact"",
                    ""type"": ""Button"",
                    ""id"": ""9abd236b-5815-4529-98b1-974f6762e181"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""keyboard_movement"",
                    ""id"": ""59e83f3a-44ae-45bf-b499-02fb92b9a5d9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""f1f9f158-7157-4f6f-9f5d-18da6b81e1c1"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""7c335ea7-38bf-454c-8705-e6cb21428bb9"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""d71d8709-33e9-4c22-9ae5-1aa572ca31fa"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""500b7acc-f62b-4fc5-b0a4-26db4eb40b75"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""61ae69df-16e5-4029-8f79-5ed8cceb933b"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""cast_spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c87b678-adf8-4e44-8247-f93031c84a16"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""452d49df-576f-4401-b611-e0039b1900ba"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""hotbar"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""61be02fd-266f-4bc1-823c-aff1fbcee662"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""pickup/interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // in_game_actions
        m_in_game_actions = asset.FindActionMap("in_game_actions", throwIfNotFound: true);
        m_in_game_actions_movement = m_in_game_actions.FindAction("movement", throwIfNotFound: true);
        m_in_game_actions_cast_spell = m_in_game_actions.FindAction("cast_spell", throwIfNotFound: true);
        m_in_game_actions_Jump = m_in_game_actions.FindAction("Jump", throwIfNotFound: true);
        m_in_game_actions_hotbar = m_in_game_actions.FindAction("hotbar", throwIfNotFound: true);
        m_in_game_actions_pickupinteract = m_in_game_actions.FindAction("pickup/interact", throwIfNotFound: true);
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

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // in_game_actions
    private readonly InputActionMap m_in_game_actions;
    private List<IIn_game_actionsActions> m_In_game_actionsActionsCallbackInterfaces = new List<IIn_game_actionsActions>();
    private readonly InputAction m_in_game_actions_movement;
    private readonly InputAction m_in_game_actions_cast_spell;
    private readonly InputAction m_in_game_actions_Jump;
    private readonly InputAction m_in_game_actions_hotbar;
    private readonly InputAction m_in_game_actions_pickupinteract;
    public struct In_game_actionsActions
    {
        private @Player_controls m_Wrapper;
        public In_game_actionsActions(@Player_controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @movement => m_Wrapper.m_in_game_actions_movement;
        public InputAction @cast_spell => m_Wrapper.m_in_game_actions_cast_spell;
        public InputAction @Jump => m_Wrapper.m_in_game_actions_Jump;
        public InputAction @hotbar => m_Wrapper.m_in_game_actions_hotbar;
        public InputAction @pickupinteract => m_Wrapper.m_in_game_actions_pickupinteract;
        public InputActionMap Get() { return m_Wrapper.m_in_game_actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(In_game_actionsActions set) { return set.Get(); }
        public void AddCallbacks(IIn_game_actionsActions instance)
        {
            if (instance == null || m_Wrapper.m_In_game_actionsActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_In_game_actionsActionsCallbackInterfaces.Add(instance);
            @movement.started += instance.OnMovement;
            @movement.performed += instance.OnMovement;
            @movement.canceled += instance.OnMovement;
            @cast_spell.started += instance.OnCast_spell;
            @cast_spell.performed += instance.OnCast_spell;
            @cast_spell.canceled += instance.OnCast_spell;
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @hotbar.started += instance.OnHotbar;
            @hotbar.performed += instance.OnHotbar;
            @hotbar.canceled += instance.OnHotbar;
            @pickupinteract.started += instance.OnPickupinteract;
            @pickupinteract.performed += instance.OnPickupinteract;
            @pickupinteract.canceled += instance.OnPickupinteract;
        }

        private void UnregisterCallbacks(IIn_game_actionsActions instance)
        {
            @movement.started -= instance.OnMovement;
            @movement.performed -= instance.OnMovement;
            @movement.canceled -= instance.OnMovement;
            @cast_spell.started -= instance.OnCast_spell;
            @cast_spell.performed -= instance.OnCast_spell;
            @cast_spell.canceled -= instance.OnCast_spell;
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @hotbar.started -= instance.OnHotbar;
            @hotbar.performed -= instance.OnHotbar;
            @hotbar.canceled -= instance.OnHotbar;
            @pickupinteract.started -= instance.OnPickupinteract;
            @pickupinteract.performed -= instance.OnPickupinteract;
            @pickupinteract.canceled -= instance.OnPickupinteract;
        }

        public void RemoveCallbacks(IIn_game_actionsActions instance)
        {
            if (m_Wrapper.m_In_game_actionsActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IIn_game_actionsActions instance)
        {
            foreach (var item in m_Wrapper.m_In_game_actionsActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_In_game_actionsActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public In_game_actionsActions @in_game_actions => new In_game_actionsActions(this);
    public interface IIn_game_actionsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnCast_spell(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnHotbar(InputAction.CallbackContext context);
        void OnPickupinteract(InputAction.CallbackContext context);
    }
}
