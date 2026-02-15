## 第一步：准备准心UI

Hierarchy 右键 → UI → Image

重命名：Crosshair

锚点设为 居中

Width / Height 设为 20 × 20

颜色先白色

## 第二步：明确功能

脚本会让准星（Crosshair）在瞄准敌人时变成红色，瞄准其他物体或空白处时保持白色。

### 变量声明部分
```
public Camera playerCamera;      
public float checkDistance = 100f; 

private Image crosshairImage;      

```

**分别表示：**
- 玩家摄像机，用于发射射线
- 射线检测距离，默认100单位
- 准星的UI Image组件

### start方法

```
void Start()
{
    crosshairImage = GetComponent<Image>();
}
```

- 获取同一个挂载在GameObject上的image组件
- image组件是一个UI元素，屏幕中心是准心

### Update方法

```
void Update()
{
    CheckAim();
}
```
- 每帧调用CheckAim()方法，持续检测瞄准状态


### CheckAim方法（核心）

```
void CheckAim()
{
    // 从屏幕中心发射射线
    Ray ray = playerCamera.ScreenPointToRay(
        new Vector3(Screen.width / 2f, Screen.height / 2f)
    );

    RaycastHit hit;
    
    if (Physics.Raycast(ray, out hit, checkDistance))
    {
        // 射线击中物体时的处理
        if (hit.collider.CompareTag("Enemy"))
        {
            crosshairImage.color = Color.red;  // 瞄准敌人，准星变红
        }
        else
        {
            crosshairImage.color = Color.white; // 瞄准其他物体，准星白色
        }
    }
    else
    {
        crosshairImage.color = Color.white; // 没有击中任何物体，准星白色
    }
}
```

这里想要再说说**ScreenPointToRay函数**

- 用于将屏幕上的一个点转换为一条从摄像机位置发射的射线。
- 输入：接收一个屏幕坐标点（以像素为单位）
- 输出：返回一条射线（Ray）
 - 起点：摄像机的位置
 - 方向：从摄像机穿过指定屏幕点的方向

在代码中的作用
-  准星固定在屏幕中心
-  需要通过屏幕中心点来判断玩家正在瞄准什么


**Physics.Raycast 详解**

Physics.Raycast 是 Unity 中用于进行射线检测的核心方法，用于检测射线是否与场景中的碰撞器相交。

类型+参数
- Ray	要发射的射线（包含起点和方向）
- out RaycastHit	存储碰撞信息的输出参数
- float	射线检测的最大距离

**RaycastHit 解释**

- 是什么？
- RaycastHit 是 Unity 中的一个结构体（struct），专门用来存储射线击中物体时的详细信息。
- 可以把它理解为一个"容器"，当射线击中物体时，所有相关的碰撞信息都会被装进这个容器里。

```
// 1. 声明变量（创建容器）
RaycastHit hit;  // 此时hit是空的，默认值

// 2. 使用变量（填充容器）
if (Physics.Raycast(ray, out hit, distance))  // out关键字让hit被赋值
{
    // 现在hit包含了击中信息
    Debug.Log(hit.collider.name);  // 可以使用hit的信息
}
```

out关键字的作用：
- out 关键字表示这个参数是用来输出数据的，而不是输入
- 调用Physics.Raycast时，你给了它一个空盒子(hit)
-  如果射线击中了物体，Raycast方法会把信息装进这个盒子里
-  然后你就可以从盒子里取出这些信息使用

Physics.Raycast(ray, out hit, distance);

out表示：这个方法会往hit里写入数据

**逻辑解读：**

先判断射线有没有击中，没击中，那么我直接让color改为white

如果击中了，碰到的是tag为Enemy的才执行准心变红

否则击中什么都不会改变准心状态
