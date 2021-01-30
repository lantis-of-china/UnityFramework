using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChouPaiPokerController : MonoBehaviour {
    public bool HVexchange;//横竖交换
    private const float X_RAIS_PERCENT_LIMIT = 0.6f;
    private const float Y_RAIS_PERCENT_LIMIT = 0.4f;
    private const float ROTATE_ANGLE_LIMIT = 50f;
    private const float MOVE_SPEED_FACTOR = 0.5f;
    private const float ROTATE_SPEED_FACTOR = 0.15f;

    private float _xRaisMoveLimit;
    private float _yRaisMoveLimit;

    private Transform _pokerBackTran;
    private Transform _leftMaskTran;
    private Transform _rightMaskTran;

    private Vector2 _originPostion;
    private Vector3 _originEulerAngle;

    private ChuoPaiMoveChecker _checker;

    public Image _imgCard;
    public Image _imgCardBack;

    private Vector3 _recordStartPos;
	private Vector3 _recordStarAngles;

    private void Start() {
        //Init();
    }

    public void Init() {
        RegistEvent();
        InitUI();

        _recordStartPos = _pokerBackTran.transform.localPosition;
		_recordStarAngles = _pokerBackTran.transform.localEulerAngles;
    }

    public void ResetToBegin() {
        _checker.Reset();
        _checker.enabled = true;
        _pokerBackTran.gameObject.SetActive(true);
        _leftMaskTran.gameObject.SetActive(true);
        _rightMaskTran.gameObject.SetActive(true);
        _pokerBackTran.transform.localPosition = _recordStartPos;
		_pokerBackTran.transform.localEulerAngles = _recordStarAngles;
    }

    public void EndAndOpenCard() {
        EndOperate();
    }

    private void OnMove(Vector3 position, EChouPaiType type) {
        if (type == EChouPaiType.Vectical) {
            SetMaskActive(HVexchange);
        }
        else {
            SetMaskActive(!HVexchange);
        }
        if (HVexchange) {
            float x = position.x;
            position.x = -position.y;
            position.y = x;
        }
        _pokerBackTran.localPosition += position * MOVE_SPEED_FACTOR;
        CheckToEndMove();
    }

    private void OnRotate(Vector3 eulerAngle) {
        SetMaskActive(true);
        _pokerBackTran.localEulerAngles += eulerAngle * ROTATE_SPEED_FACTOR;
        CheckToEndRotate();
    }

    private void ResetPokerBack() {
        SetMaskActive(true);
        _pokerBackTran.localPosition = _originPostion;
        _pokerBackTran.localEulerAngles = _originEulerAngle;
    }

    private void CheckToEndMove() {
        if (Mathf.Abs(_pokerBackTran.localPosition.x - _originPostion.x) >= _xRaisMoveLimit) {
            EndOperate();
            return;
        }
        if (Mathf.Abs(_pokerBackTran.localPosition.y - _originPostion.y) >= _yRaisMoveLimit) {
            EndOperate();
        }
    }

    private void CheckToEndRotate() {
        float angle = Mathf.Abs(_pokerBackTran.localEulerAngles.z - _originEulerAngle.z);
        if (angle > 180) {
            angle = 360 - angle;
        }
        if (angle >= ROTATE_ANGLE_LIMIT) {
            EndOperate();
        }
    }

    public Action finishCall;
    private void EndOperate() {
        _checker.Reset();
        _checker.enabled = false;
        _pokerBackTran.gameObject.SetActive(false);
        _leftMaskTran.gameObject.SetActive(false);
        _rightMaskTran.gameObject.SetActive(false);

        if (finishCall != null)
        {
            finishCall();
        }
    }

    private void InitUI() {
        _pokerBackTran = transform.Find("Image_PokerBack");
        _leftMaskTran = transform.Find("LeftMask");
        _rightMaskTran = transform.Find("RightMask");

        _originPostion = _pokerBackTran.localPosition;
        _originEulerAngle = _pokerBackTran.localEulerAngles;

        _xRaisMoveLimit = _pokerBackTran.GetComponent<RectTransform>().sizeDelta.x * X_RAIS_PERCENT_LIMIT;
        _yRaisMoveLimit = _pokerBackTran.GetComponent<RectTransform>().sizeDelta.y * Y_RAIS_PERCENT_LIMIT;
    }

    private void RegistEvent() {
        _checker = gameObject.AddComponent<ChuoPaiMoveChecker>();
        _checker.Init();
        _checker.OnMove = OnMove;
        _checker.OnRotate = OnRotate;
        _checker.OnRelease = ResetPokerBack;
    }

    private void SetMaskActive(bool active) {
        _leftMaskTran.gameObject.SetActive(active);
        _rightMaskTran.gameObject.SetActive(active);
    }
}
