using System;

[Serializable]
public class EnemyWave
{
    public Enemy[] enemies;
    public float spawnRate = 1f;
    public float waveCooldown = 5; // Time to wait before starting new wave (starts after all enemies have been spawned)
}
