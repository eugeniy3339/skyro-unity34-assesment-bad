using UnityEngine;

public static class ParticlesManager
{
    public static GameObject SpawnParticles(GameObject particlesPrefab, Vector2 particlesPosition, Vector2 particlesUpVector)
    {
        if (particlesPrefab == null) return null;
        GameObject particles = GameObject.Instantiate(particlesPrefab);
        particles.transform.position = particlesPosition;
        particles.transform.up = particlesUpVector;

        ParticleSystem particleSystem = particles.GetComponent<ParticleSystem>();
        particleSystem?.Play();
        float particlesLifeTime = particleSystem != null ? particleSystem.startLifetime : 5f;

        GameObject.Destroy(particles, particlesLifeTime);
        return particles;
    }
}