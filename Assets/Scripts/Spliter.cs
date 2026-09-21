using UnityEngine;

public class Spliter : MonoBehaviour
{
    [SerializeField] private Raycaster _raycaster;
    [SerializeField] private Exploder _exploder;
    [SerializeField] private Spawner _spawner;
    [SerializeField] private int _separator = 2;

    public void OnDetected(Cube clickedCube)
    {
        _spawner.Spawn(clickedCube,Divide(clickedCube));
    }

    public Vector3 Divide(Cube clickedCube)
    {
        Vector3 Scale = clickedCube.transform.localScale / _separator;
        return Scale;
    }

    public void Push(Vector3 positionPush,Cube cube)
    {
        _exploder.ApplyKnockback(positionPush,cube.Rigidbody);
    }

    public void BlowUp(Vector3 explodePosition, float multiplieExplode)
    {
        _exploder.Explode(explodePosition, multiplieExplode);
    }

    private void OnEnable()
    {
        _raycaster.Detecting += OnDetected;
        _spawner.Spawned += Push;
        _spawner.BlowUped += BlowUp;
    }

    private void OnDisable()
    {
        _raycaster.Detecting -= OnDetected;
        _spawner.Spawned -= Push;
        _spawner.BlowUped -= BlowUp;
    }
}
