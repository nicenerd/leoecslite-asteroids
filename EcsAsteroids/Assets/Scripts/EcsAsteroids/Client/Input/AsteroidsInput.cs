//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/EcsAsteroids/Client/Input/AsteroidsInput.inputactions
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

namespace EcsAsteroids.Client
{
    public partial class @AsteroidsInput : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @AsteroidsInput()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""AsteroidsInput"",
    ""maps"": [
        {
            ""name"": ""Game"",
            ""id"": ""fc890493-0141-4be8-bbf1-aec7eb6ec468"",
            ""actions"": [
                {
                    ""name"": ""MoveFwd"",
                    ""type"": ""Button"",
                    ""id"": ""6af10eab-6285-498d-b9aa-bc8a231f242e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""PrimaryWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""ca5c11f2-3f75-4bc5-91c7-968f7e6fff7e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SecondaryWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""2b560564-34c6-4a95-ba67-e118e0b7eed4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""44fefa20-593c-4bab-8490-8a5362ba065e"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""8421602b-6d24-4278-ba73-bc1be037eff2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b848259f-62aa-4907-9398-07fc1376d407"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e6456bd-b84b-451f-b668-85045dd7960d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dbcef7f0-e783-4409-b1d6-77204b5d5826"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4fc74c31-041e-4318-8538-4f8832bfb16f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""78dc5aa0-558d-4806-ab9b-4115b5e8cc59"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveFwd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""57448427-3f2b-43a6-a0b1-74ca115ca065"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveFwd"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""DirWASD"",
                    ""id"": ""3a1c8a10-b434-48e9-901f-b4d7b66cf6c6"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""9fbb1aa5-b381-479e-ae08-ba2a57bbf65a"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""65393872-30ee-4d4b-9c6b-b5477558c326"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""DirArrows"",
                    ""id"": ""c77eb70d-6b92-445c-b13d-db0b4681f6b9"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Positive"",
                    ""id"": ""728a7299-70d4-4122-ba96-ea820bf802a5"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Negative"",
                    ""id"": ""c79225a9-708b-4efa-bc02-da58e27a17f2"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b03a4ae1-19a8-4ada-9e7e-295d4ab06c1b"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""81641a92-a476-42ea-a608-5c0ce0b057c0"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Game
            m_Game = asset.FindActionMap("Game", throwIfNotFound: true);
            m_Game_MoveFwd = m_Game.FindAction("MoveFwd", throwIfNotFound: true);
            m_Game_PrimaryWeapon = m_Game.FindAction("PrimaryWeapon", throwIfNotFound: true);
            m_Game_SecondaryWeapon = m_Game.FindAction("SecondaryWeapon", throwIfNotFound: true);
            m_Game_Rotation = m_Game.FindAction("Rotation", throwIfNotFound: true);
            m_Game_Pause = m_Game.FindAction("Pause", throwIfNotFound: true);
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

        // Game
        private readonly InputActionMap m_Game;
        private IGameActions m_GameActionsCallbackInterface;
        private readonly InputAction m_Game_MoveFwd;
        private readonly InputAction m_Game_PrimaryWeapon;
        private readonly InputAction m_Game_SecondaryWeapon;
        private readonly InputAction m_Game_Rotation;
        private readonly InputAction m_Game_Pause;
        public struct GameActions
        {
            private @AsteroidsInput m_Wrapper;
            public GameActions(@AsteroidsInput wrapper) { m_Wrapper = wrapper; }
            public InputAction @MoveFwd => m_Wrapper.m_Game_MoveFwd;
            public InputAction @PrimaryWeapon => m_Wrapper.m_Game_PrimaryWeapon;
            public InputAction @SecondaryWeapon => m_Wrapper.m_Game_SecondaryWeapon;
            public InputAction @Rotation => m_Wrapper.m_Game_Rotation;
            public InputAction @Pause => m_Wrapper.m_Game_Pause;
            public InputActionMap Get() { return m_Wrapper.m_Game; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameActions set) { return set.Get(); }
            public void SetCallbacks(IGameActions instance)
            {
                if (m_Wrapper.m_GameActionsCallbackInterface != null)
                {
                    @MoveFwd.started -= m_Wrapper.m_GameActionsCallbackInterface.OnMoveFwd;
                    @MoveFwd.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnMoveFwd;
                    @MoveFwd.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnMoveFwd;
                    @PrimaryWeapon.started -= m_Wrapper.m_GameActionsCallbackInterface.OnPrimaryWeapon;
                    @PrimaryWeapon.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnPrimaryWeapon;
                    @PrimaryWeapon.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnPrimaryWeapon;
                    @SecondaryWeapon.started -= m_Wrapper.m_GameActionsCallbackInterface.OnSecondaryWeapon;
                    @SecondaryWeapon.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnSecondaryWeapon;
                    @SecondaryWeapon.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnSecondaryWeapon;
                    @Rotation.started -= m_Wrapper.m_GameActionsCallbackInterface.OnRotation;
                    @Rotation.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnRotation;
                    @Rotation.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnRotation;
                    @Pause.started -= m_Wrapper.m_GameActionsCallbackInterface.OnPause;
                    @Pause.performed -= m_Wrapper.m_GameActionsCallbackInterface.OnPause;
                    @Pause.canceled -= m_Wrapper.m_GameActionsCallbackInterface.OnPause;
                }
                m_Wrapper.m_GameActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @MoveFwd.started += instance.OnMoveFwd;
                    @MoveFwd.performed += instance.OnMoveFwd;
                    @MoveFwd.canceled += instance.OnMoveFwd;
                    @PrimaryWeapon.started += instance.OnPrimaryWeapon;
                    @PrimaryWeapon.performed += instance.OnPrimaryWeapon;
                    @PrimaryWeapon.canceled += instance.OnPrimaryWeapon;
                    @SecondaryWeapon.started += instance.OnSecondaryWeapon;
                    @SecondaryWeapon.performed += instance.OnSecondaryWeapon;
                    @SecondaryWeapon.canceled += instance.OnSecondaryWeapon;
                    @Rotation.started += instance.OnRotation;
                    @Rotation.performed += instance.OnRotation;
                    @Rotation.canceled += instance.OnRotation;
                    @Pause.started += instance.OnPause;
                    @Pause.performed += instance.OnPause;
                    @Pause.canceled += instance.OnPause;
                }
            }
        }
        public GameActions @Game => new GameActions(this);
        public interface IGameActions
        {
            void OnMoveFwd(InputAction.CallbackContext context);
            void OnPrimaryWeapon(InputAction.CallbackContext context);
            void OnSecondaryWeapon(InputAction.CallbackContext context);
            void OnRotation(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
        }
    }
}