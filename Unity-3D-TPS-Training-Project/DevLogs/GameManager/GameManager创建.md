##  Step 1

创建GameManager.cs

## Step 2 游戏状态（核心思想）

```
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Start,
        Playing,
        GameOver
    }

    public GameState currentState;
}

```
重点

**1.public class GameManager**
在游戏当中，他通常负责
- 管理游戏流程
- 控制开始/进行中/结束
- 全局状态控制

是指一种中央控制脚本

2.enum 是什么

- 我现在不是在存变量，而是定义游戏当前处于什么模式？
- bool写法是不足的
  - 如果只有bool isPlayin 那么暂停在哪里体现。
 
- enum 就是让游戏流程离散化
- public GameState currentState; 就是在记录，游戏现在在那个阶段
- start → Playing → GameOver（像是状态机）

## Step3 - 让游戏启动进入Playing

```
void Start()
{
    SetState(GameState.Playing);
}

```

写一个新函数
```
void SetState(GameState newState)
{
    currentState = newState;
}
```

为什么不直接赋值？

currentState = GameState.Playing;

**故意不用**

切换状态 = 触发一堆事

比如说：
- 停止生成敌人
- 锁定玩家输入
- 打开UI

必须走一个“入口函数”

这就是：状态切换入口。

## Step 4 挂载

创建空物体，然后GameManager，把脚本托上去

可以用Inspector检查

## 完成的部分：
游戏全局控制中心（雏形）
- EnemySpawner要询问
- Player要询问
- UI要询问

下一步：
GameManager Singleton


