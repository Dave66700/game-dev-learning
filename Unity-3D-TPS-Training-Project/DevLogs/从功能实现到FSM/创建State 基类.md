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

## ⑤abstract

这个是抽象类，只能被继承，就像模板一样

## ②protected EnemyAI enemy

每个状态都能访问，只能被子类和自身访问，外部不能直接修改

设计思想：状态不自己创造东西，而是操作敌人身上的已有组件

## ③ Enter / Update / Exit

FSM的灵魂，三个方法定义了状态的完整生命周期

- public virtual void Enter() { }
  - virtual：允许子类重写，但不是必须重写
  - 什么时候调用，状态机切换到这个状态，立刻调用
  - 作用进入前动作、入场动画、重置变量
- public virtual void Update() { }
  - 状态处于激活状态时，每帧调用
  - 执行状态的逻辑，巡逻、追逐、攻击
- public virtual void Exit() { }
  -  当状态机离开这个状态时，调用一次
  -  做清理工作、停止动画、释放资源
