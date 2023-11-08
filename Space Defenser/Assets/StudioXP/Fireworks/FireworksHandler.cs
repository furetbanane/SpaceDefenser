using UnityEngine;
// ReSharper disable CheckNamespace

public class FireworksHandler : MonoBehaviour
{
    private Vector2 _v2PosInit;
    private ParticleSystem _ps;

    private void Awake() 
    {
        _ps = GetComponent<ParticleSystem>();
        _v2PosInit = transform.position; 
    }

    public void ShootRedFirework()
    {
        transform.position = _v2PosInit + new Vector2 (Random.Range(-3,3),Random.Range(-3,3));
        var main = _ps.main;
        main.startColor = new Color (1,0,0,1);
        _ps.Play();
    }

    public void ShootGreenFirework()
    {
        transform.position = _v2PosInit + new Vector2 (Random.Range(-3,3),Random.Range(-3,3));
        var main = _ps.main;
        main.startColor = new Color (0,1,0,1);
        _ps.Play();
    }

    public void ShootBlueFirework()
    {
        transform.position = _v2PosInit + new Vector2 (Random.Range(-3,3),Random.Range(-3,3));
        var main = _ps.main;
        main.startColor = new Color (0,0,1,1);
        _ps.Play();
    }

    public void ShootRainbowFirework()
    {
        transform.position = _v2PosInit + new Vector2 (Random.Range(-3,3),Random.Range(-3,3));
        var main = _ps.main;
        main.startColor = new Color (Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),1);
        _ps.Play();
    }
}
