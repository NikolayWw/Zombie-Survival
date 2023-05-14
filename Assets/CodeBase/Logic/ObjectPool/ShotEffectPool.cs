using CodeBase.Services.Factory;
using CodeBase.StaticData.Items.WeaponItems.ShotEffect;
using System.Collections.Generic;

namespace CodeBase.Logic.ObjectPool
{
    public class ShotEffectPool
    {
        private readonly IGameFactory _gameFactory;
        private readonly Dictionary<ShotEffectId, Queue<FXShotObject>> _dictionary = new Dictionary<ShotEffectId, Queue<FXShotObject>>();

        public ShotEffectPool(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public FXShotObject Get(ShotEffectId shotEffectId)
        {
            if (ShotEffectId.None == shotEffectId)
                return null;

            Queue<FXShotObject> objects = GetQueue(shotEffectId);

            if (GetPoolObject(objects, out var poolObject))
                return poolObject;

            return InitNewPoolObject(shotEffectId, objects);
        }

        private static bool GetPoolObject(Queue<FXShotObject> objects, out FXShotObject poolObject)
        {
            for (int i = 0; i < objects.Count; i++)
            {
                FXShotObject dequeue = objects.Dequeue();
                objects.Enqueue(dequeue);
                if (dequeue.IsReady)
                {
                    poolObject = dequeue;
                    poolObject.Enable();
                    return true;
                }
            }

            poolObject = null;
            return false;
        }

        private FXShotObject InitNewPoolObject(ShotEffectId shotEffectId, Queue<FXShotObject> objects)
        {
            FXShotObject poolObject = _gameFactory.CreateFXShot(shotEffectId);
            objects.Enqueue(poolObject);
            poolObject.Enable();
            return poolObject;
        }

        private Queue<FXShotObject> GetQueue(ShotEffectId shotEffectId)
        {
            if (_dictionary.TryGetValue(shotEffectId, out var objects) == false)
            {
                objects = new Queue<FXShotObject>();
                _dictionary.Add(shotEffectId, objects);
            }

            return objects;
        }
    }
}