## Step1 - 创建State基类

新建一个脚本，EnemyState.cs

```
using UnityEngine;

// 所有敌人状态的父类
public abstract class EnemyState
{
    protected EnemyAI enemy;

    // 构造函数：让每个状态都知道自己属于哪个敌人
    public EnemyState(EnemyAI enemy)
    {
        this.enemy = enemy;
    }

    // 进入状态时调用
    public virtual void Enter() { }

    // 每帧执行
    public virtual void Update() { }

    // 离开状态时调用
    public virtual void Exit() { }
}
```

### 1. 类的定义

public abstract class EnemyState

abstract(抽象类)：最关键的设计决策

- 这个类不能被直接创建，只能继承
- 作用：这个定义，是一个规则和模板，强制所有具体状态，都必须遵循这一套规则
- 抽象类完美表达了这种“只定规矩，不干具体活”的设计意图。

### 2.核心成员变量

protected EnemyAI enemy;

protected：只能被子类和自身访问，外部并不能修改

为什么每个状态都要知道敌人？状态需要控制敌人行为，例如巡逻需要获取敌人的移动组件。

状态不自己创造东西，而是操作敌人身上的已有组件

### 3.构造函数

```
public class EnemyState
{
    private EnemyAI enemy;

    // 这就是构造函数
    public EnemyState(EnemyAI enemy)
    {
        this.enemy = enemy;
    }
}
```

- 什么是构造函数？
  -  构造函数是一个特殊的方法，它在**创建对象时自动调用，用来初始化这个对象。**
  -  构造函数的名字和类名一样
  -  他没有返回值（void都不写）
  -  在 new EnemyState(...) 时自动执行
- 什么是“强制依赖输入”？
  -  


