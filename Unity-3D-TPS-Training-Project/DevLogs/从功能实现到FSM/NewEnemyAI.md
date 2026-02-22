# EnemyAI要升职

## 1.定义当前状态

public EnemyState currentState;

- 存储当前正在执行的状态

## 2.状态切换函数 --修改 EnemyAI

```
public void SwitchState(EnemyState newState)
{
    // 第一步：如果当前有状态，让它退出
    if (currentState != null)
    {
        currentState.Exit();  // 调用旧状态的Exit方法
    }

    // 第二步：切换到新状态
    currentState = newState;   // 把currentState指向新状态

    // 第三步：如果新状态不为空，让它进入
    if (currentState != null)
    {
        currentState.Enter();  // 调用新状态的Enter方法
    }
}
```

**旧的状态->切换->新的状态**

具体过程：

**①假定，敌人当下巡逻状态 到 追逐状态**

当前内存：
```
currentState → [PatrolState对象] (正在巡逻)
newState     → [ChaseState对象] (准备切换)
```
**②切换指针指向**

currentState = newState;   // 把currentState指向新状态

执行后内存变化：

```
之前： currentState → [PatrolState对象]
之后： currentState → [ChaseState对象]  ← 指针改变了！
      newState     → [ChaseState对象]  (newState没变)
```

**③检查新状态并进入**
```
ChaseState对象初始化完成，开始执行追逐逻辑
currentState → [ChaseState对象] (正在追逐)
```


## 3.新的update部分
```
void Update()
{
    if (Gamemanager.Instance.currentState != Gamemanager.GameState.Playing)
        return;

    if (currentState != null)
        currentState.Update();
}
```
- 只有游戏处于"Playing"状态时，敌人才允许行动。
- Gamemanager.Instance.currentState全局 层面
- 如果不为空，就调用这个状态的Update()方法
- enemy.currentState 个体层面

优势，为什么这么设计？

- 优势1
  - 全局控制力：游戏暂停时，一行代码就能定住所有敌人
  - Gamemanager.Instance.currentState = GameState.Paused
- 优势2
  ·- 关注点分离
  - GameManager只是关心游戏是否正在进行
  - 每个敌人：只关心自己在干什么，巡逻、攻击


- 优势3
  - 避免全局检查。减少if判断什么的。
