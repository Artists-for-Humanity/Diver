using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;
    [SerializeField] float _flipYRotationTime = 0.5f;
    private Coroutine _turnCoroutine;
    private PlayerMove _player;
    private bool _isFacingRight;
    // Start is called before the first frame update
    private void Awake(){
        _player = _playerTransform.gameObject.GetComponent<PlayerMove>();
        if(_player.playerDirection == 'r'){
            _isFacingRight = true;
        } else {
            _isFacingRight = false;
        }
        
    }
    // Update is called once per frame
    void Update(){
        transform.position = _playerTransform.position;
    }
    public void CallTurn(){
        _turnCoroutine = StartCoroutine(FlipYLerp());
    }
    private IEnumerator FlipYLerp(){
        float startRotation = _playerTransform.localEulerAngles.y;
        float endRotationAmount = DetermineEndRotation();
        float yRotation = 0f;
        float elapsedTime = 0f;
        while(elapsedTime < _flipYRotationTime){
            elapsedTime += Time.deltaTime;
            yRotation = Mathf.Lerp(startRotation, endRotationAmount, (elapsedTime / _flipYRotationTime));
            transform.rotation = Quaternion.Euler(0f, yRotation, 0f);
            yield return null;
        }
    }
    private float DetermineEndRotation(){
        _isFacingRight = !_isFacingRight;
        if(_isFacingRight){
            return 180f;
        } else {
            return 0f;
        }
    }
}
