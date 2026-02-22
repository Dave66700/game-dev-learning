## FSM

本质： 同一时间，只允许角色处于一种状态。

## 长什么样？

曾经

```
Update()
{
    Chase();
    Attack();
}
```
现在

```
Update()
{
    currentState.Update();
}
```

## 状态

一个State的本质是：一个小脚本，专门负责一种行为

ChaseState只负责追逐玩家

AttackState只负责攻击部分

HitState只负责受击

## 为什么必须FSM

行为会指数级增长

EnemyAI 不是控制所有行为

而是本身是状态管理器

State 行为本身


FSM 是：

将角色行为拆成互斥状态，并通过状态切换控制逻辑流，使 AI 行为可扩展、可维护。（官话）
