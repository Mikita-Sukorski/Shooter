using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour {
    [SerializeField]
    Behaviour[] _componentsToDisable;

    void Start()
    {
        if(!isLocalPlayer)
        {
            for(int i = 0; i < _componentsToDisable.Length; i++)
            {
                _componentsToDisable[i].enabled = false;
            }
        }
    }
}
