using UnityEngine;

//Author: Molly R�le
namespace EventCallbacks
{
    public class DebugListener : MonoBehaviour
    {
        private void OnEnable() => EventSystem<DebugEvent>.RegisterListener(OnUnitDebug);
        private void OnDisable() => EventSystem<DebugEvent>.UnRegisterListener(OnUnitDebug);


        //Might wanna change method name
        void OnUnitDebug(DebugEvent unitDebug)
        {
            Debug.Log(unitDebug.EventDescription);
        }
    }
}