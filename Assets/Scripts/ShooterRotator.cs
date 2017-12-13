using UnityEngine;

public class ShooterRotator : MonoBehaviour {

    private enum RotateState
    {
        Idle,Vertical,Horizontal,Ready
    }

    private RotateState state = RotateState.Idle;       //RotateState 타입의 state 변수 선언 및 초기화.

    public float verticalRotateSpeed = 360f;

    public float horizontalalRotateSpeed = 360f;

    public BallShooter ballShooter;


    void Update()
    {
        switch (state)
        {
            case RotateState.Idle:
                if (Input.GetButtonDown("Fire1"))
                {
                    state = RotateState.Horizontal;
                }
                break;
            case RotateState.Horizontal:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(0, horizontalalRotateSpeed * Time.deltaTime, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Vertical;
                }
                break;
            case RotateState.Vertical:
                if (Input.GetButton("Fire1"))
                {
                    transform.Rotate(new Vector3(-verticalRotateSpeed * Time.deltaTime, 0, 0));
                }
                else if (Input.GetButtonUp("Fire1"))
                {
                    state = RotateState.Ready;
                    ballShooter.enabled = true;
                }
                break;
            case RotateState.Ready:
                break;
        }
    }

    private void OnEnable()     //다음 라운드를 위해 리셋
    {
        transform.rotation = Quaternion.identity;
        state = RotateState.Idle;
        ballShooter.enabled = false;
    }
}
