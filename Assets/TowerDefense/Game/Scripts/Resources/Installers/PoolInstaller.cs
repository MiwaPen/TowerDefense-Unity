using UnityEngine;
using Zenject;

public class PoolInstaller : MonoInstaller {
    [SerializeField] private ObjectPooler pool;
    public override void InstallBindings() {
        Container.Bind<ObjectPooler>().FromInstance(pool)
            .AsSingle()
            .NonLazy();
        Container.QueueForInject(pool);
    }
}