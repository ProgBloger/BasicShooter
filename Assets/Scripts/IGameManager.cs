using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager
{
    public ManagerStatus status{ get;}

    public void Startup(NetworkService service);
}
