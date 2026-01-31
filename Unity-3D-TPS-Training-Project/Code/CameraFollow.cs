using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("跟随目标")]
    public Transform target;       // CameraTarget，角色头部

    [Header("位置设置")]
    public float distance = 4f;    // 摄像机后方距离
    public float height = 1.5f;    // 摄像机高度
    public float smoothSpeed = 5f; // 平滑跟随系数

    [Header("防穿墙设置")]
    public float collisionOffset = 0.2f; // 离墙留一点空隙

    void LateUpdate()
    {
        if (target == null) return;

        // 1️ 计算摄像机理想位置（角色后方 + 抬高）
        Vector3 desiredPosition = target.position 
                                  - target.forward * distance 
                                  + Vector3.up * height;

        // 2️ 检测角色和理想位置之间是否有障碍物
        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit))
        {
            // 如果撞到东西，就把摄像机放到撞击点前一点
            desiredPosition = hit.point + hit.normal * collisionOffset;
        }

        // 3️ 平滑移动摄像机
        Vector3 smoothedPosition = Vector3.Lerp(
            transform.position,
            desiredPosition,
            smoothSpeed * Time.deltaTime);

        transform.position = smoothedPosition;

        // 4️ 让摄像机始终看向角色头部
        transform.LookAt(target);
    }
}
