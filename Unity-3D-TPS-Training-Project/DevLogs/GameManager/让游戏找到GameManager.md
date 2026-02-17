# 目标：让整个游戏都能找到 GameManager

现在的问题：

目前情况，Enemy找不到GameManager、PlayerAttack找不到GameManager

容易出的问题：

- 速度极慢，每帧都要GameObject.Find()
- 名字改了就崩溃


解决：Singleton（单例模式）

修改GameManager
```
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState
    {
        Start,
        Playing,
        GameOver
    }

    public GameState currentState;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        SetState(GameState.Playing);
    }

    public void SetState(GameState newState)
    {
        currentState = newState;
    }
}

```

修改究竟有什么用：

**可用的游戏架构**

三个重点的关键概念：

- 单例（singleton）
- 生命周期（Awake/Start）
- 状态切换入口（State Control）

## 1.public static GameManager Instance;

功能：
- 创建一个全局唯一入口
- 这一句意思是任何脚本都可以直接访问GameManager
- 为什么是叫Singleton
  - 游戏里唯一的GameManager
  - 游戏的总控制室
 
## 2.Awake（）
```
void Awake()
{
    Instance = this;
}
```

- 单例必须在Awake里面建立
- Unity执行顺序
  -  Awake->Start->Update
 
- 为什么不在Start()？
  - 别的脚本可能在start中访问
  - 晚一点赋值，instance还是null
  - 此时，因为别的脚本的 Start 可能更早执行

## 3. enum GameState
```
public enum GameState
{
    Start,
    Playing,
    GameOver
}
```
我在做：游戏状态机（Finite State Machine）

游戏的任意时刻只能属于一个状态。

## 4.currentState

当前游戏阶段。

Inspector 里还能实时看到，非常方便 Debug。

## 5.现在的流程

```
加载场景
 ↓
Awake → 建立 Instance
 ↓
Start → 游戏进入 Playing
```

测试是否成功

验证，打开PlayerAttack。
