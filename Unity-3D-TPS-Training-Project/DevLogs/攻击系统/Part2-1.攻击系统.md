# 攻击系统



## 总览
**最小的可用攻击系统**
-  输入：玩家输入攻击键
-  检测：攻击有没有打到东西
-  目标：被打的是不是敌人
-  反馈：敌人掉血，有反应
## 为什么选择Raycast
- 基础，通用：不需要动画、不需要模型、不需要复杂碰撞
- 不用考虑投射物（子弹，有点复杂）

## 当前目标

按 鼠标左键：
- 从摄像机前方发射射线

- 如果射线碰到敌人

## 新建PlayerAttack.cs

### 基础参数
```
public float attackRange = 2f;       
public int attackDamage = 10;        
public Transform cameraTransform;

```

- 攻击范围，定义角色攻击的最大距离
- 攻击伤害
- 在第三人称游戏中，攻击方向通常基于摄像机而非角色自身。

### 这里我想要展开学习：
**角色朝向 vs 摄像机朝向的本质**

 - 角色朝向：视觉表现层
   - 这只是"看起来"的样子
   - 作用：让动画、模型、装备看起来自然转身
 - 摄像机朝向：游戏逻辑层
    -  "实际发生"的逻辑
    -  决定攻击、射击、交互的实际方向
  
### Update()函数

 ```
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }
```

- Input.GetMouseButtonDown(0) 详解

0 = 鼠标左键

1 = 鼠标右键

2 = 鼠标中键
 - 输入鼠标左键，进行攻击

### Attack（）函数

```
void Attack()
    {
        Ray ray = new Ray(cameraTransform.position, cameraTransform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, attackRange))
        {
            Debug.Log("Hit: " + hit.collider.name);
        }
    }
```

**1.rays 射线的核心**

起点：摄像机位置

方向 ： 摄像机正前方

长度 ：前者定义的攻击范围

```
摄像机------射线------敌人
     ↑               ↑
position         forward * attackRange
```

**2.RaycastHit 结构包含的信息**

包含hit世界坐标、法线方向等等（需要哪个用哪个）

**3.Physics.Raycast() 详解**
```
bool Physics.Raycast(
    Ray ray,           // 射线
    out RaycastHit hitInfo, // 碰撞信息（输出参数）
    float maxDistance  // 最大检测距离
)
```
返回true：射线击中了碰撞体。

false：未击中

放到我的Attack函数，根据false和true得到是否要执行Debug。


意思是：

**“从 ray 出发，最多 attackRange 米，看有没有撞到 Collider”**

