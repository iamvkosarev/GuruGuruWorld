using Leopotam.Ecs;
using UnityEngine;

namespace Client {
    sealed class GunUserSystem : IEcsRunSystem {
        // auto-injected fields.
        readonly EcsWorld world = null;
        readonly EcsFilter<GunUserComponent, MovingComponent> filter = null;

        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }
        void IEcsRunSystem.Run () {
            foreach (var i in filter)
            {
                ref var userCom = ref filter.Get1(i);
                ref var moverCom = ref filter.Get2(i);

                if (userCom.DropGun && userCom.UsingGunView != null)
                {
                    userCom.UsingGunView.IsUsing = false;
                    userCom.UsingGunView.transform.parent = null;
                    userCom.UsingGunView = null;
                }

                var colliders = Physics2D.OverlapCircleAll(moverCom.Transform.position, 0.005f);
                foreach (var collider in colliders)
                {
                    if (collider.gameObject.TryGetComponent<GunView>(out var gun))
                    {
                        if (!gun.IsUsing && userCom.UseGun && userCom.UsingGunView == null)
                        {
                            gun.IsUsing = true;
                            foreach (var MovingPart in moverCom.MovingParts)
                            {
                                MovingPart.localPosition = new Vector3();
                            }
                            moverCom.Transform.position = gun.transform.position + gun.UsingPoint.localPosition;
                            gun.transform.parent = moverCom.RotatingParts[0].transform;
                            userCom.UsingGunView = gun;
                        }
                    }
                }
                userCom.DropGun = false;
                userCom.UseGun = false;


                if (userCom.UsingGunView != null)
                {
                    
                    if (Input.GetMouseButton(0))
                    {
                        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(userCom.UsingGunView.transform.position);

                        //Get the Screen position of the mouse
                        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

                        //Get the angle between the points
                        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

                        //Ta Daaa
                        userCom.UsingGunView.transform.localScale = new Vector3(-userCom.UsingGunView.transform.parent.localScale.x, userCom.UsingGunView.transform.parent.localScale.y, userCom.UsingGunView.transform.parent.localScale.z);
                        userCom.UsingGunView.transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
                        userCom.UsingGunView.IsShooting = true;
                    }
                    else
                    {

                        userCom.UsingGunView.IsShooting = false;
                    }
                }
            }
        }
    }
}