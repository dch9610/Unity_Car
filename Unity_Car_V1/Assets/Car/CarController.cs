using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    // 축 이름에 대한 상수 선/
    private const string HORIZONTAL = "Horizontal";
    private const string VERTICAL = "Vertical";

    // 실제 입력을 저장하기 위해 
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;

    [SerializeField] private float motorForce;
    [SerializeField] private float breakForce;
    [SerializeField] private float maxSteerAngle;

    // 휠콜라이더를 사용하기 위해 참조
    [SerializeField] private WheelCollider frontLeftwheelCollider;
    [SerializeField] private WheelCollider frontRightwheelCollider;
    [SerializeField] private WheelCollider backLeftwheelCollider;
    [SerializeField] private WheelCollider backRightwheelCollider;

    [SerializeField] private Transform frontLeftwheelTransform;
    [SerializeField] private Transform frontRightwheelTransform;
    [SerializeField] private Transform backLeftwheelTransform;
    [SerializeField] private Transform backRightwheelTransform;

    public void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();

    }

    private void GetInput()
    {
        // 축 이름을 매개 변수로 사용
        horizontalInput = Input.GetAxis(HORIZONTAL);
        verticalInput = Input.GetAxis(VERTICAL);
        isBreaking = Input.GetKey(KeyCode.Space);
    }

    // 스페이스 바를 누르면 핸들 모터 방식 
    private void HandleMotor()
    {
        // 앞바퀴에만 힘을 가함
        backLeftwheelCollider.motorTorque = verticalInput * motorForce;
        backRightwheelCollider.motorTorque = verticalInput * motorForce;
        currentbreakForce = isBreaking ? breakForce : 0f;
        if (isBreaking)
        {
            ApplyBreaking();
        }
    }

    
    private void ApplyBreaking()
    {
        // 브레이크 토크를 변경하여 제동력을 할
        frontLeftwheelCollider.brakeTorque = currentbreakForce;
        frontRightwheelCollider.brakeTorque = currentbreakForce;
        backLeftwheelCollider.brakeTorque = currentbreakForce;
        backRightwheelCollider.brakeTorque = currentbreakForce;

    }

    // 스티어링 각동 
    private void HandleSteering() {

        // 스티얼이 각도와 직렬화 가능한 변수 사용
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftwheelCollider.steerAngle = currentSteerAngle;
        frontRightwheelCollider.steerAngle = currentSteerAngle;

    }

    // 휠 업데이트
    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftwheelCollider, frontLeftwheelTransform);
        UpdateSingleWheel(frontRightwheelCollider, frontRightwheelTransform);
        UpdateSingleWheel(backLeftwheelCollider, backLeftwheelTransform);
        UpdateSingleWheel(backRightwheelCollider, backRightwheelTransform);
    }


    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        // 적용할 위치와 회전을 가져오는 단일 휠 방법
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

}
