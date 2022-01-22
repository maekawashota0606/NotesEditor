using UnityEngine;

public class EditCameraController : SingletonMonoBehaviour<EditCameraController>
{
    [SerializeField]
    private Camera _camera = null;
    [SerializeField]
    private Vector3 _scrollRatio = new Vector3(7.5f, 2.5f, 2.5f);
    [SerializeField]
    private float _maxExpantion = -5;
    [SerializeField]
    private float _maxShrink = -25;

    public void Move(Vector3 move)
    {
        Vector3 pos = Multiply(_scrollRatio, move);
        _camera.gameObject.transform.position += pos;

        if (_camera.gameObject.transform.position.x < 0)
            _camera.transform.position += Vector3.right * (0 - _camera.transform.position.x);

        // ŠgkãŒÀ
        if (_maxExpantion < _camera.gameObject.transform.position.z)
            _camera.gameObject.transform.position += Vector3.forward * (_maxExpantion - _camera.gameObject.transform.position.z);
        else if (_camera.gameObject.transform.position.z < _maxShrink)
            _camera.gameObject.transform.position += Vector3.forward * (_maxShrink - _camera.gameObject.transform.position.z);
    }

    public Vector3 Multiply(Vector3 v1, Vector3 v2)
    {
        return new Vector3(v1.x * v2.x, v1.y * v2.y, v1.z * v2.z);
    }
}
