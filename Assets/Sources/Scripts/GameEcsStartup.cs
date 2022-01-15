using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class GameEcsStartup : MonoBehaviour {

        [SerializeField] private GameSceneDataView sceneData;
        [SerializeField] private GameStaticData staticData;
        [SerializeField] private GameShareDataView shareData;

        private static GameStaticData _staticData;
        private static GameShareDataView _shareData;
        EcsWorld world;
        EcsSystems updateSystem;
        EcsSystems fixedUpdateSystem;
        EcsSystems lateUpdateSystem;

        #region Static Methods
        public static Color GetPelmenRandomSkinColor()
        {
            return _staticData.PelmenStaticData.colors[UnityEngine.Random.Range(0, _staticData.PelmenStaticData.colors.Length)];
        }
        public static int GetPelmenID()
        {
            int result = _shareData.PelemnCount;
            _shareData.PelemnCount++;
            return result;
        }
        public static void SelectHat(ref PelmenView pelmenView, bool chooseHat, PelmenHatType pelmenHatType)
        {
            PelmenHatData hatData = _staticData.PelmenStaticData.PelmenHats[0];
            if (chooseHat)
            {
                if(pelmenHatType == PelmenHatType.Random)
                {
                    int indexHat = UnityEngine.Random.Range(0, _staticData.PelmenStaticData.PelmenHats.Length);
                    hatData = _staticData.PelmenStaticData.PelmenHats[indexHat];
                }
                else
                {
                    foreach (var pelmenHat in _staticData.PelmenStaticData.PelmenHats)
                    {
                        if (pelmenHat.PelmenHatType == pelmenHatType)
                        {
                            hatData = pelmenHat;
                            break;
                        }
                    }
                }
            }
            else
            {
                foreach (var pelmenHat in _staticData.PelmenStaticData.PelmenHats)
                {
                    if (pelmenHat.PelmenHatType == PelmenHatType.Base)
                    {
                        hatData = pelmenHat;
                        break;
                    }
                }

            }
            if (hatData.PelmenHatType == PelmenHatType.Base)
            {
                pelmenView.HatSpriteRenderer.enabled = false;
            }
            else
            {
                pelmenView.HatSpriteRenderer.enabled = true;
                pelmenView.HatSpriteRenderer.sprite = hatData.Sprite;

            }
        }
        #endregion
        void Start () {
            _staticData = staticData;
            _shareData = shareData;
            world = new EcsWorld();
            updateSystem = new EcsSystems(world);
            fixedUpdateSystem = new EcsSystems(world);
            lateUpdateSystem = new EcsSystems(world);

            #region Editor
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(updateSystem);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(fixedUpdateSystem);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(lateUpdateSystem);
#endif
            #endregion

            updateSystem
                .Add(new SpawnEnvironmentSystem())
                .Add(new SpawnPlayerSystem())
                .Add(new SpawnNPCSystem())
                .Add(new IceCreamSystem())
                .Add(new NPCInputSystem())
                .Add(new PlayerInputSystem())
                .Add(new PelmenFaceSystem())
                .Add(new PlayerAnimationSystem());

            fixedUpdateSystem
                .Add(new MovingSystem());
            lateUpdateSystem

                .Add(new RotatingSystem());


            #region Inject and Init Systems

            updateSystem.Inject(sceneData);
            fixedUpdateSystem.Inject(sceneData);
            lateUpdateSystem.Inject(sceneData);

            updateSystem.Inject(shareData);
            fixedUpdateSystem.Inject(shareData);
            lateUpdateSystem.Inject(shareData);

            updateSystem.Inject(staticData);
            fixedUpdateSystem.Inject(staticData);
            lateUpdateSystem.Inject(staticData);

            updateSystem.Init();
            fixedUpdateSystem.Init();
            lateUpdateSystem.Init();

            #endregion
        }

        #region Updates And Destroy
        void Update()
        {
            updateSystem?.Run();
        }

        private void FixedUpdate()
        {
            fixedUpdateSystem.Run();
        }

        private void LateUpdate()
        {
            lateUpdateSystem.Run();

        }

        void OnDestroy()
        {
            if (updateSystem != null)
            {
                updateSystem.Destroy();
                updateSystem = null;
            }
            if (fixedUpdateSystem != null)
            {
                fixedUpdateSystem.Destroy();
                fixedUpdateSystem = null;
            }
            if (lateUpdateSystem != null)
            {
                lateUpdateSystem.Destroy();
                lateUpdateSystem = null;
            }
            world.Destroy();
            world = null;
        }
        #endregion
    }
}