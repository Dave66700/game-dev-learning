## 角色系统最后一块拼图：跳跃 🦘

### 物理推导：
```
从地面开始，有初速度 v₀
在最高点：v = 0，高度 = h
能量守恒：½mv₀² = mgh
推导得：v₀ = √(2gh)  ← 这是求"需要多大的初速度"
```

### 代码解析：
```
yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

```

jumpHeight：期望的跳跃高度（米）

-2f：负二（物理公式中的2倍，负号用于处理重力方向）---->这个是和公式一一对应的

gravity：重力加速度（通常为 -9.8 m/s²）

Mathf.Sqrt()：开平方根函数

yVelocity：计算出的垂直初速度


### 代码区

** S1.确认变量区域有这些：**

 public float jumpHeight = 2f;
 public float gravity = -9.8f;
 public float groundedGravity = -2f;

 private float yvelocity;

** S2.修改ApplyGravity**

⭐ 新增：跳跃输入检测
```
 if (Input.GetButtonDown("Jump"))
    {
      yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
    }
```
拆解

yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);

它直接算出：

“要跳到 jumpHeight，需要的初速度是多少”

并且这里面需要在isGrounded中，确保不会空中连跳


最后完整的ApplyGravity()
```
void ApplyGravity()
{
    if (controller.isGrounded)
    {
        if (yVelocity < 0)
            yVelocity = groundedGravity;

        // ⭐ 新增：跳跃输入检测
        if (Input.GetButtonDown("Jump"))
        {
            yVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }
    else
    {
        yVelocity += gravity * Time.deltaTime;
    }

    Vector3 gravityMove = new Vector3(0, yVelocity, 0);
    controller.Move(gravityMove * Time.deltaTime);
}

```
