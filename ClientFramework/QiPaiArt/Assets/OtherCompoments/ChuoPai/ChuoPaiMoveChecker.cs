
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//public enum EChouPaiArea {
//    Vectical = 0,
//    Left = 1,
//    Right = 2,
//}

public enum EChouPaiType {
    None,
    Vectical,
    Horizontal,
    Rotate,
    HorizontalOrVectical,
}

public class ChuoPaiMoveChecker : MonoBehaviour {
    public System.Action<Vector3, EChouPaiType> OnMove;
    public System.Action<Vector3> OnRotate;
    public System.Action OnRelease;

    private ChouPaiArea _chouPaiArea;
    private readonly ChouPaiData _chouPaiData = new ChouPaiData();

    private Vector2 _movedVecter;
    private EChouPaiType _chouPaiType;
    private EChouPaiType ChouPaiType {
        get {
            if (_chouPaiData.TouchCount==0) {
                _chouPaiType = EChouPaiType.None;
                return _chouPaiType;
            }
            if (IsOperateEnsure()) {
                return _chouPaiType;
            }

            if (_chouPaiData.TouchCount == 2) {
                _chouPaiType = EChouPaiType.Rotate;
                return _chouPaiType;
            }

            if (_chouPaiData.TouchCount == 1) {
                if (_movedVecter == Vector2.zero) {
                    _chouPaiType = EChouPaiType.HorizontalOrVectical;
                    return _chouPaiType;
                }
                if (_chouPaiType == EChouPaiType.HorizontalOrVectical) {
                    float x = Mathf.Abs(_movedVecter.x);
                    float y = Mathf.Abs(_movedVecter.y);
                    if (x > y && x > 10) {
                        _chouPaiType = EChouPaiType.Horizontal;
                        return _chouPaiType;
                    }
                    if (y > x && y > 10) {
                        _chouPaiType = EChouPaiType.Vectical;
                        return _chouPaiType;
                    }
                }
            
            }
            return _chouPaiType;
        }
    }

    public void Init() {
        InitCompents();
        RegistInputEvent();
    }

    public void Reset() {
        _chouPaiData.Reste(0);
        _chouPaiData.Reste(1);
    }

    public void SetDragEnable(bool isEnable) {
        _chouPaiArea.gameObject.SetActive(isEnable);
    }



    private void OnBeginDrag(int pointerId, Vector2 pos) {
        _chouPaiData.OnPointerDown();
    }

    private void OnPointerUp(int pointerId, Vector2 pos) {
        _chouPaiData.Reste(pointerId);
        if (_chouPaiData.TouchCount==0) {
            _movedVecter=Vector2.zero;;
        }

        if (ChouPaiType != EChouPaiType.None) {
            return;
        }
        if (OnRelease != null) {
            OnRelease();
        }
    }

    private void OnDrag(int pointerId, Vector2 pos) {
        SetChouPaiDatasOnDrag(pointerId, pos);
    }

    private void RegistInputEvent() {
      
            _chouPaiArea.BeginDragEvent = OnBeginDrag;
            _chouPaiArea.PointerUpEvent = OnPointerUp;
            _chouPaiArea.DragEvent = OnDrag;

    }

    private void InitCompents() {
        _chouPaiArea = transform.Find("DragArea").gameObject.AddComponent<ChouPaiArea>();

    }

    private void SetChouPaiDatasOnDrag(int pointerId, Vector2 pos) {
        _chouPaiData.OnDrag(pointerId,pos);
        _movedVecter += pos;
    }


    private void LateUpdate() {
        if (ChouPaiType == EChouPaiType.None) {
            return;
        }
        if (ChouPaiType == EChouPaiType.HorizontalOrVectical) {
            return;
        }
        if (ChouPaiType == EChouPaiType.Vectical) {
            Vector2 pos = Vector2.zero;
            pos.y = _chouPaiData.GetMovedPostion(0).y;
            _chouPaiData.UpdateOldPostion(0);
            OnMove(pos, EChouPaiType.Vectical);
            return;
        }
        if (ChouPaiType == EChouPaiType.Horizontal) {
            Vector2 pos = Vector2.zero;
            pos.x = _chouPaiData.GetMovedPostion(0).x;
            _chouPaiData.UpdateOldPostion(0);
            OnMove(pos, EChouPaiType.Horizontal);
            return;
        }

        if (ChouPaiType == EChouPaiType.Rotate) {
            float angle = GetTwoFingerOperateAngle();
            if (angle == 90) {//两指操作时，有一个手指没操作则判断为无效操作
                return;
            }
            if (angle < 0||angle >= 90) {
                CheckToRotate();
            }
            _chouPaiData.UpdateOldPostion(0);
            _chouPaiData.UpdateOldPostion(1);
        }
    }

    private void CheckToRotate() {
        float leftMove =_chouPaiData.GetMovedPostion(0).magnitude;
        float rightMove = _chouPaiData.GetMovedPostion(1).magnitude;
        float xMoveDistance = Mathf.Max(Mathf.Max(leftMove, rightMove));

        if (_chouPaiData.GetMovedPostion(0).x > 0 && _chouPaiData.GetMovedPostion(1).x < 0) {
            xMoveDistance = -xMoveDistance;
        }
        Vector3 eulerAngle=Vector3.zero;
        eulerAngle.z = xMoveDistance;
        if (OnRotate != null) {
            OnRotate(eulerAngle);
        }
    }


    private float GetTwoFingerOperateAngle() {
        float angle = Vector2.Angle(_chouPaiData.GetMovedPostion(0), _chouPaiData.GetMovedPostion(1));
        if (angle < -90) {
            angle += 180;
        }
        return angle;
    }

    private bool IsOperateEnsure() {
        if (_chouPaiType == EChouPaiType.None) {
            return false;
        }
        if (_chouPaiType == EChouPaiType.HorizontalOrVectical) {
            return false;
        }
        return true;
    }
}



public class ChouPaiData {
    public int TouchCount;
    private Vector2 DeltaPostion1;
    private Vector2 DeltaPostion2;
    private Vector2[] _deltaPostion=new Vector2[2];
    public Vector2 GetMovedPostion(int pointerId) {
        return _deltaPostion[pointerId];
    }

    public void OnPointerDown() {
        TouchCount++;
    }

    public void OnDrag(int pointerId,Vector3 pos) {
        _deltaPostion[pointerId] = pos;
    }

    public void UpdateOldPostion(int pointerId) {
        _deltaPostion[pointerId] = Vector2.zero;
    }

    public void Reste(int pointerId) {
        TouchCount--;
        _deltaPostion[pointerId] = Vector2.zero;
    }
}