using UnityEngine;
using Unity.Entities;

public class CharacterRunSystem : ComponentSystem
{
    private EntityQuery _runQuery;

    bool startRuning = true;
    bool stopRuning = false;
    private bool _runBool;
    private bool isBigSpeed = false;

    protected override void OnCreate()
    {
        _runQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(), ComponentType.ReadOnly<UserInputData>());
    }
    protected override void OnUpdate()
    {
        Entities.With(_runQuery).ForEach((Entity entity, UserInputData userInputData, ref MoveData moveData, ref InputData inputData) =>
        {
            float speedChange = _runBool ? -userInputData.runSpeed : userInputData.runSpeed;

            moveData.Speed = userInputData.speed;

            if (stopRuning == false && inputData.Run == 1 || userInputData.isTestRun)
            {
                _runBool = true;
                userInputData.speed += speedChange;
                startRuning = false;
                stopRuning = true;

                if (userInputData.isTestRun)
                {
                    isBigSpeed = true;
                    userInputData.isTestRun = false;
                }
            }

            if (stopRuning == true && inputData.Run == 0 && startRuning == false || isBigSpeed)
            {
                _runBool = false;
                userInputData.speed += speedChange;
                startRuning = true;
                stopRuning = false;

                isBigSpeed = false;
            }

        });
    }


}
