using UnityEngine;
using Zenject;

public class PouchInstaller : MonoInstaller
{
    [SerializeField] private Pouch pouch;
    
    public override void InstallBindings()
    {
        Container.Bind<Pouch>().FromInstance(pouch).AsCached();
    }
}