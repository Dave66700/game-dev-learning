## 加入重力系统（角色不再漂浮）

### 变量区会用到的部分

public float gravity = -9.8f;
public float groundedGravity = -2f;
public float jumpHeight = 2f;   
private float yVelocity;

我的理解：
- groundedGravity = -2f;
  - 防止接地检测的开关抖动
  - 垂直速度的平滑过渡
  - 更好地处理斜坡和不平地面


### ApplyGravity() 改成：
```
void ApplyGravity()
{
    if (controller.isGrounded && yVelocity < 0)
    {
        yVelocity = groundedGravity; // 贴地
    }
    else
    {
        yVelocity += gravity * Time.deltaTime;
    }

    Vector3 gravityMove = new Vector3(0, yVelocity, 0);
    controller.Move(gravityMove * Time.deltaTime);
}

```

