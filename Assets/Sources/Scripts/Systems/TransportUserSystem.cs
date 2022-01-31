using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class TransportUserSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<TransportUserComponent, MovingComponent> filter = null;
        
        void IEcsRunSystem.Run () {
            foreach (var i in filter)
            {
                ref var userCom = ref filter.Get1(i);
                ref var moverCom = ref filter.Get2(i);

                if (userCom.LeaveTranspor && userCom.UsingTransport != null)
                {
                    moverCom.MovingStaticData = userCom.OwnMovingStaticData;
                    userCom.UsingTransport.IsUsing = false;
                    userCom.UsingTransport.transform.parent = null;
                    userCom.UsingTransport = null;
                }

                var colliders = Physics2D.OverlapCircleAll(moverCom.Transform.position, 0.005f);
                foreach (var collider in colliders)
                {
                    if (collider.gameObject.TryGetComponent<TransportView>(out var transport))
                    {
                        if (!transport.IsUsing && userCom.UseTranspor && userCom.UsingTransport == null)
                        {
                            transport.IsUsing = true;
                            foreach (var MovingPart in moverCom.MovingParts)
                            {
                                MovingPart.localPosition = new Vector3();
                            }
                            moverCom.Transform.position = transport.transform.position + transport.UsingPoint.localPosition;
                            transport.transform.parent = moverCom.Transform;
                            userCom.OwnMovingStaticData = moverCom.MovingStaticData;
                            moverCom.MovingStaticData = transport.MovingStaticData;
                            userCom.UsingTransport = transport;
                        }
                    }
                }
                userCom.LeaveTranspor = false;
                userCom.UseTranspor = false;


                if(userCom.UsingTransport != null)
                {
                    userCom.UsingTransport.Direction = moverCom.Direction;
                }
            }
        }
    }
}