using System;

[Serializable]
public class EnemyPool
{
    public Enemy[] enemies;
    public float spawnRate = 1f;
    public float spawnRateMultiplier = 0.95f;

    public float waveSize = 10;
    public float waveSizeMultiplier = 1.05f;

    public float waveCooldown = 5;
    public float waveCooldownMultiplier = 0.95f;

    public void NewWave()
    {
        spawnRate *= spawnRateMultiplier;
        waveSize *= waveSizeMultiplier;
        waveCooldownMultiplier *= waveCooldownMultiplier;
    }
}
