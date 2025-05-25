using UnityEngine;

public class WaveObjectPool : ObjectPool<WaveTag>
{
    private Enemy[] _enemies;
    
    public void SpawnWave(Transform position)
    {
        WaveTag wave = GetObject();
        wave.transform.position = position.position;

        wave.AllEnemiesDestroyed += PutObject;
    }

    protected override void PutObject(WaveTag obj)
    {
        base.PutObject(obj);

        obj.AllEnemiesDestroyed -= PutObject;
    }
}