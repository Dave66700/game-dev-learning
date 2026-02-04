准心

射线是从屏幕中心发出去的

关键认知：


射线是从：

cameraTransform.forward

发出的。

也就是屏幕正中心

所以准心就是一个：

UI 图片放在屏幕中心

它不是3D物体，是UI层。

Step1：
①创建UI准心

再Hierarchy里，右键→ UI → Canvas

unity自动生成
Canvas

EventSystem

②在Canvas下创建image


右键 Canvas → UI → Image

③ 设置锚点到正中心

选中 Crosshair，在 Inspector 里：

点击 Rect Transform 左上角的小方块（锚点预设）

按住 Alt + Shift

选正中间那个

这样它就永远在屏幕中心。

④稍后再换成十字准星

Step2：

画出射线调试线

Debug.DrawRay(cameraTransform.position, cameraTransform.forward * attackRange, Color.red, 1f);

关于射线参数
```
Debug.DrawRay(Vector3 start,      // 射线起点
              Vector3 dir,        // 方向向量（包含长度信息）
              Color color,        // 射线颜色
              float duration = 0f // 显示持续时间（秒）
             );

```


完整Attack
```
void Attack()
{
    Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
    RaycastHit hit;

    Debug.DrawRay(cameraTransform.position, cameraTransform.forward * attackRange, Color.red, 1f);

    if (Physics.Raycast(ray, out hit, attackRange))
    {
        Debug.Log("Hit: " + hit.collider.name);
    }
}

```
