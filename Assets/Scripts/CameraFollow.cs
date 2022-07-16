using System;
using Player;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //public float horizontalLimit;
    //public float verticalLimit;

    public Transform _player;

    private void Start()
    {
        //_player = PlayerEntity.Instance.gameObject.transform;
    }

    private void Update()
    {
        if (_player == null) return;
        var playerPosition = _player.position;
        var cameraTransform = transform;
        cameraTransform.position = new Vector3(
            playerPosition.x,
            playerPosition.y,
            cameraTransform.position.z);
    }
}