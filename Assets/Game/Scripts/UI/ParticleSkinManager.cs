using UnityEngine;

public class ParticleSkinManager : MonoBehaviour
{
    [SerializeField] private ParticleSystem _snowParticles;
    private ISkinManager _skinManager;

    void Start()
    {
        _skinManager = SkinManager.Instance;
        UpdateParticles(_skinManager.GetSelectedSkin(SkinType.Background));

        _skinManager.SelectionSkin += OnSkinChanged;
    }

    private void OnSkinChanged(SkinData skin)
    {
        if (skin.type == SkinType.Background)
        {
            UpdateParticles(skin);
        }
    }


    private void UpdateParticles(SkinData skin)
    {
        if (skin.id == "bg_winter")
        {
            if (!_snowParticles.isPlaying) _snowParticles.Play();
        }
        else
        {
            if (_snowParticles.isPlaying) _snowParticles.Stop();
        }
    }

    private void OnDestroy()
    {
        if (_skinManager != null)
            _skinManager.SelectionSkin -= OnSkinChanged;
    }
}
