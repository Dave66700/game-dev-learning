给Camera写 “跟随脚本”

①变量
```
public Transform target;
public float distance = 4f;
public float height = 1.5f;
public float smoothSpeed =5f;
```
② LateUpdate（摄像机必须用这个）

```
void LateUpdate()
{
  
}


```

为什么要用LateUpdate？

因为摄像机要等角色移动完再更新，否则会抖。

**计算摄像机应该去的位置**

```
Vector3 desiredPosition = target.position 
                          - target.forward * distance 
                          + Vector3.up * height;
```
计算过程：
- 先找到目标位置，也就是玩家角色（对应着： target.position）
- -target.forward：是一个负方向向量（0，0，-1），负责确定方向 然后乘距离 distance
-  Vector3.up（0，1，0）,负责确定向上的方向然后乘距离

拓展：不同的游戏参数需求不同

```
动作游戏（如《鬼泣》）
distance = 4.0f;  // 中等距离，看动作细节
height = 1.8f;    // 较高，强调角色帅气姿势

射击游戏（如《战争机器》）
distance = 2.5f;  // 较近，专注瞄准
height = 1.0f;    // 较低，过肩视角
```


**摄像机平滑跟随**

Vector3.Lerp()
线性插值
```
数学公式：
Lerp(A, B, t) = A + (B - A) * t

其中：
A: 起始值
B: 结束值
t: 插值系数 (0到1之间)

效果：从A慢慢过渡到B

```

代码实现线性插值
```
Vector3 smoothedPosition = Vector3.Lerp(
    transform.position,     
    desiredPosition,        
    smoothSpeed * Time.deltaTime); 

```

参数解释

- transform.position
  - 摄像机当前的位置
  - 每次Update/LateUpdate都会变化

- desiredPosition
  - 理想位置

- smoothSpeed * Time.deltaTime
  - 关键部分：平滑速度
  - smoothSpeed: 平滑系数（通常5-20）
  - Time.deltaTime: 帧时间，使运动与帧率无

 ```
transform.LookAt(target);

```

效果：让当前物体的Z轴（forward/前方）指向target

就像：你瞬间转头看着某个人

## Unity里面的设置

选 Main Camera：

在 CameraFollow 脚本里：

Target  → 拖 CameraTarget 进去
  

