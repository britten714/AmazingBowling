using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour {

    public enum State
    {
        Idle, Ready, Tracking
    }

    private State state
    {
        set                             //누군가가 = 을 통해서 값(value)을 전달할 때 set 안에 있는 처리들이 실행된다는 뜻. 예를 들어, 아래 Awake()에서 State.Idle이란 값을 전달해왔다. 이렇게 property를 쓰면 밖에서는 변수처럼 쉽게 쓰고 내부에서는 특정 처리를 끼워넣을 수 있어서 좋음. 함수로 대체할 수도 있지만, 밖에서 편하게 쓰기 위함.  
        {
            switch (value)
            {
                case State.Idle:
                    targetZoomSize = roundReadyZoomSize;
                    break;
                case State.Ready:
                    targetZoomSize = readyShotZoomSize;
                    break;
                case State.Tracking:
                    targetZoomSize = trackingZoomSize;
                    break;
            }
        }
    }

    private Transform target;
    public float smoothTime = 0.2f;
    private Vector3 lastMovingVelocity;     //스무스댐핑할 때 마지막순간에 얼마의 속도로 이동할 건지. 스무스댐핑 메서드를 쓸때 필요하다. 
    private Vector3 targetPosition;

    private Camera cam;
    private float targetZoomSize = 5f;

    private const float roundReadyZoomSize = 14.5f;
    private const float readyShotZoomSize = 5f;
    private const float trackingZoomSize = 10f;

    private float lastZoomSpeed;
    

    void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        state = State.Idle;
    }

    private void Move()
    {
        targetPosition = target.transform.position;

        Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, targetPosition, ref lastMovingVelocity, smoothTime);     //ref의 의미는 변수가 함수 내부에 들어갔을 때 변경된 값을 그대로 챙겨서 나온다는 것. 

        transform.position = targetPosition;          //위에줄 코드 안 쓰고 이렇게 하면 카메라가 타겟에 딱 붙는다. 

    }

    private void Zoom()
    {
        float smoothZoomSize = Mathf.SmoothDamp(cam.orthographicSize, targetZoomSize, ref lastZoomSpeed, smoothTime);
        cam.orthographicSize = smoothZoomSize;
    }

    private void FixedUpdate()      //Update()는 화면이 1번 갱신될 때마다 실행되는 것이고, FixedUpdate()는 정해진 간격을 지킨다. 
    {
        if (target != null)
        {
            Move();
            Zoom();
        }
    }

    public void Reset()
    {
        state = State.Idle;
    }

    public void SetTarget(Transform newTarget, State newState)
    {
        target = newTarget;
        state = newState;
    }
}
