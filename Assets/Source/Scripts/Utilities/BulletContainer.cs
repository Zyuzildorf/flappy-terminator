using UnityEngine;

namespace Source.Scripts.Utilities
{
    public static class BulletContainer
    {
        private static Transform _bulletContainer;

        public static Transform Container
        {
            get
            {
                if (_bulletContainer == null)
                {
                    GameObject container = new GameObject("BulletsContainer");
                    _bulletContainer = container.transform;
                }
                return _bulletContainer;
            }
        }
    }
}