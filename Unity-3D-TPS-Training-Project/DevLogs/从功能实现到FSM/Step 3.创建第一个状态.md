# 新建第一个状态：IdleState

```
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    public EnemyIdleState(EnemyAI enemy) : base(enemy) { }

    public override void Enter()
    {
        Debug.Log("Enter Idle");
    }

    public override void Update()
    {
        // 暂时什么都不做
    }

    public override void Exit()
    {
        Debug.Log("Exit Idle");
    }
}
```

## 1.public class EnemyIdleState : EnemyState

现在这个IdleState拥有了基类的所有特性（enemy引用、Enter/Update/Exit方法）

## 2.public EnemyIdleState(EnemyAI enemy) : base(enemy) { }

(EnemyAI enemy)：构造函数要求传入一个敌人实例

: base(enemy)：调用父类（EnemyState）的构造函数，把敌人传上去

## 3.三个生命周期方法

## 在NewEnemyAI加入
```
void Start()
{
    SwitchState(new EnemyIdleState(this));
}
```

新的状态
