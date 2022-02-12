using Leopotam.Ecs;
using Client;
sealed class SetColorSystem : IEcsRunSystem
{
    // auto-injected fields.
    readonly EcsWorld world = null;
    readonly EcsFilter<SetColorComponent> filter = null;
    void IEcsRunSystem.Run()
    {
        foreach (var i in filter)
        {
            ref var com = ref filter.Get1(i);
            foreach (var spriteRenderer in com.SpriteRenderers)
            {
                spriteRenderer.color = com.NeedColor;
            }
            filter.GetEntity(i).Destroy();
        }
    }
}