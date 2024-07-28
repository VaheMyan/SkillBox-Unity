using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;
using Unity.Entities;

public class UserInputSystem : ComponentSystem
{
    public EntityQuery _inputQuery;
    public EntityQuery _inputRun;
    public EntityQuery _inputChangeMaterial;

    private bool _runBool;
    private bool _changeMaterialBool;
    //private bool isButtonPressed = Input.GetButton("<Keyboard>/M");
    //private float speedChange;

    public InputAction _moveAcion; // sa sharjvelu gortsoghutyuny
    public InputAction _shootAction; // sa krakelu gortsoghutyuny
    public InputAction _runAction;
    public InputAction _changeMaterialAction;

    private float2 _moveInput;
    private float _shootInput;
    public float _runInput;
    public float _changeMaterialInput;

    protected override void OnCreate()
    {
        _inputQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<ShootData>());
        _inputRun = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<ShootData>());
        _inputChangeMaterial = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<UserInputData>(), ComponentType.ReadOnly<ChangeMaterialData>());
    }

    protected override void OnStartRunning() // stex avelacnum es knopkeqy vory inch petq a ani
    {
        // OnStartRunning() - es ashxatum a erb hamakargy sksum a ashxatel

        // Joystik
        _moveAcion = new InputAction(name: "move", binding: "<Gamepad>/rightStick");
        _moveAcion.AddCompositeBinding("Dpad")
            .With(name: "Up", binding: "<Keyboard>/w")
            .With(name: "Down", binding: "<Keyboard>/s")
            .With(name: "Left", binding: "<Keyboard>/a")
            .With(name: "Right", binding: "<Keyboard>/d");

        _moveAcion.performed += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAcion.started += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAcion.canceled += context => { _moveInput = context.ReadValue<Vector2>(); };
        _moveAcion.Enable(); // miacvum a _moveAcion-i ashxatanqy

        _shootAction = new InputAction(name: "shoot", binding: "<Keyboard>/Space"); // stex asum es vor _shootAction = new InputAction
        // binding: "<Keyboard>/Space" krakelu knopkai vra script ka ytex yntrum es vor knpokeqy isk stex kanchum es dranq "<Keyboard>/Space"
        _shootAction.performed += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.started += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.canceled += context => { _shootInput = context.ReadValue<float>(); };
        _shootAction.Enable(); // miacvum a _shootAction-i ashxatanqy

        _runAction = new InputAction(name: "run", binding: "<Keyboard>/Enter");
        _runAction.performed += context => { _runInput = context.ReadValue<float>(); };
        _runAction.started += context => { _runInput = context.ReadValue<float>(); };
        _runAction.canceled += context => { _runInput = context.ReadValue<float>(); };
        _runAction.Enable();

        _changeMaterialAction = new InputAction(name: "changeMateerial", binding: "<Keyboard>/C");
        _changeMaterialAction.performed += context => { _changeMaterialInput = context.ReadValue<float>(); };
        _changeMaterialAction.started += context => { _changeMaterialInput = context.ReadValue<float>(); };
        _changeMaterialAction.canceled += context => { _changeMaterialInput = context.ReadValue<float>(); };
        _changeMaterialAction.Enable();

    }

    protected override void OnStopRunning() // OnStopRunning() - es ashxatum a erb hamakargy dadarum a ashxatel
    {
        _moveAcion.Disable(); // kangnecvum a _moveAcion-i ashxatanqy
        _shootAction.Disable(); // kangnecvum a _shootAction-i ashxatanqy
        _runAction.Disable();
        _changeMaterialAction.Disable();
    }

    bool startRuning = true;
    bool stopRuning = false;

    bool startChanging = true;
    bool stopChanging = false;

    protected override void OnUpdate()
    {
        Entities.With(_inputQuery).ForEach((Entity entity, ref InputData inputData, ChangeAbility changeAbility, CharacterHealth characterHealth) => // kap a hastatvum InputData script-i het
        {
            //if (characterHealth.isDisable)
            //{
            //    _moveAcion.Disable(); // kangnecvum a _moveAcion-i ashxatanqy
            //    _shootAction.Disable(); // kangnecvum a _shootAction-i ashxatanqy
            //    _runAction.Disable();
            //    _changeMaterialAction.Disable();
            //}

            //if (characterHealth.isDisable == false)
            //{
            //    _moveAcion.Enable(); // kangnecvum a _moveAcion-i ashxatanqy
            //    _shootAction.Enable(); // kangnecvum a _shootAction-i ashxatanqy
            //    _runAction.Enable();
            //    _changeMaterialAction.Enable();
            //}
            inputData.Move = _moveInput;
            inputData.Shoot = _shootInput;
            inputData.Run = _runInput;
            inputData.ChangeMat = _changeMaterialInput;
            changeAbility._changeMaterialInput = _changeMaterialInput;
        });

        Entities.With(_inputChangeMaterial).ForEach((Entity entity, ref ChangeMaterialData changeMaterialData, ref InputData inputData) => // kap a hastatvum InputData script-i het
        {
            int speedChange = _changeMaterialBool ? -1 : 1;
            if (stopChanging == false)
            {
                if (_changeMaterialInput == 1)
                {
                    _changeMaterialBool = true;
                    changeMaterialData.isDissolve += speedChange;
                    startChanging = false;
                    stopChanging = true;
                }
            }

            if (stopChanging == true)
            {
                if (_changeMaterialInput == 0)
                {
                    if (startChanging == false)
                    {
                        _changeMaterialBool = false;
                        changeMaterialData.isDissolve += speedChange;
                        startChanging = true;
                        stopChanging = false;
                    }
                }
            }
        });

        Entities.With(_inputRun).ForEach((Entity entity, ref MoveData moveData, UserInputData userInputData) => // kap a hastatvum InputData script-i het
        {
            float speedChange = _runBool ? -0.2f : 0.2f;

            moveData.Speed = userInputData.speed;

            if (stopRuning == false)
            {
                if (_runInput == 1)
                {
                    _runBool = true;
                    userInputData.speed += speedChange;
                    startRuning = false;
                    stopRuning = true;
                }
            }

            if (stopRuning == true)
            {
                if (_runInput == 0)
                {
                    if (startRuning == false)
                    {
                        _runBool = false;
                        userInputData.speed += speedChange;
                        startRuning = true;
                        stopRuning = false;
                    }
                }
            }
        });
    }
}
