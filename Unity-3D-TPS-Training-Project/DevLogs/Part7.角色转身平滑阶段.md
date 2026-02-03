## 现在的问题：

角色瞬间转向 → 很生硬，不像游戏角色

### 下一步目标：

角色朝移动方向平滑旋转

## 原理

角色朝移动方向转身，而不是瞬间贴过去
### 数学原理：
```
Quaternion.Slerp(currentRotation, targetRotation, speed * Time.deltaTime)

```

随时间平滑插值：Speed * Time.deltaTime 创建了一个与帧时间相关的插值因子

Slerp = 球面线性插值，专门用来平滑旋转 3D 向量

### 具体改动步骤

**① Move() 函数里计算目标方向**
```
Vector3 move = forward * v + right * h;

if(move.magnitude >= 0.1f) // 避免输入太小导致抖动
{
    // 角色朝向目标方向
    Quaternion targetRotation = Quaternion.LookRotation(move);
    transform.rotation = Quaternion.Slerp(
        transform.rotation, 
        targetRotation, 
        rotationSpeed * Time.deltaTime
    );
}

```

### 我的学习内容：

**A.Vector3 move = forward * v + right * h;**

- forward：指角色的前方方向
- right：指角色的右方方向
- v：垂直输入值（ W/S 键，范围 -1 到 1）
- h：水平输入值（ A/D 键，范围 -1 到 1）

作用：将输入值转换为世界空间的移动方向向量

**B.move.magnitude：移动向量的长度**

-  ≥ 0.1f：设定一个死区阈值

- 目的：防止摇杆轻微偏移或按键轻微触发时角色抖动

- if(move.magnitude >= 0.1f) // 避免输入太小导致抖动


**C.Quaternion targetRotation = Quaternion.LookRotation(move);**

计算目标旋转
  
- 计算"应该"朝向的理想方向

- 特点：这是瞬间计算出的"目标值"

  - 示例：如果玩家按"W+D"向右前移动，这里会计算出45度方向

  
**D.transform.rotation = Quaternion.Slerp(
    transform.rotation, //当前朝向
    targetRotation, //理想目标
    rotationSpeed * Time.deltaTime //速度因子
)**;

平滑旋转到目标方向

- 让当前朝向逐渐接近目标朝向

- 特点：不是瞬间转向，而是有过渡动画


**② 新增变量**

public float rotationSpeed = 10f; // 角色旋转速度

- 参数控制角色旋转的平滑程度和速度


