# AttackState攻击状态

## 再一次

我们要实现：

Chase->距离够近->Attack

Attack->距离边远->Chase

与此同时：NewEnemyAI没有攻击逻辑

### 旧逻辑：

```
EnemyAI.Update()
    if(distance < detect)
        Chase()

    if(distance < attack)
        Attack()
```

最大的问题：原本会

每帧重复判断

行为互相覆盖

后期爆炸

**而FSM只有一个状态在运行。**

## 创建AttackState

```
using UnityEngine;

public class EnemyAttackState : EnemyState
{
    float attackTimer;

    public EnemyAttackState(EnemyAI_FSM enemy) : base(enemy) { }

    public override void Enter()
    {
        Debug.Log("Enter Attack");
        attackTimer = 0f;
    }

    public override void Update()
    {
        if (enemy.player == null) return;

        float distance =
            Vector3.Distance(enemy.transform.position,
                             enemy.player.position);

        // ===== 如果玩家跑远 → 回追击 =====
        if (distance > enemy.attackRange)
        {
            enemy.SwitchState(new EnemyChaseState(enemy));
            return;
        }

        // ===== 始终朝向玩家 =====
        enemy.transform.LookAt(enemy.player);

        // ===== 攻击冷却 =====
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            AttackPlayer();
            attackTimer = enemy.attackCooldown;
        }
    }

    void AttackPlayer()
    {
        if (enemy.playerHealth == null)
            enemy.playerHealth =
                enemy.player.GetComponent<PlayerHealth>();

        Debug.Log("FSM Enemy Attack!");

        enemy.playerHealth.TakeDamage(enemy.damage);
    }

    public override void Exit()
    {
        Debug.Log("Exit Attack");
    }
}
```

### 总观

这个状态继承自 EnemyState，专门处理敌人的攻击行为。

### 逐行

public EnemyAttackState(NewEnemyAI enemy) : base(enemy) { }

构造函数接收NewEnemyAI 类型的敌人，并通过 base(enemy) 传给父类

2.Enter()-进入攻击状态

```
public override void Enter()
{
  Debug
  attackTimer =0f;
}
```

其他状态，切换到攻击状态可以调用

计时器会归零，意味着进入状态后，立刻能攻击一次

debug是为了调试

3.Update() - 每帧执行的攻击逻辑
```
public override void Update()
    {
        if (enemy.player == null) return;

        float distance =
            Vector3.Distance(enemy.transform.position,
                             enemy.player.position);

        // ===== 如果玩家跑远 → 回追击 =====
        if (distance > enemy.attackRange)
        {
            enemy.SwitchState(new EnemyChaseState(enemy));
            return;
        }

        // ===== 始终朝向玩家 =====
        enemy.transform.LookAt(enemy.player);

        // ===== 攻击冷却 =====
        attackTimer -= Time.deltaTime;

        if (attackTimer <= 0f)
        {
            AttackPlayer();
            attackTimer = enemy.attackCooldown;
        }
    }

    void AttackPlayer()
    {
        if (enemy.playerHealth == null)
            enemy.playerHealth =
                enemy.player.GetComponent<PlayerHealth>();

        Debug.Log("FSM Enemy Attack!");

        enemy.playerHealth.TakeDamage(enemy.damage);
    }
```

1.玩家存在性检查
- 玩家不存在，直接返回，不执行后续代码
2.距离判断
- 抛出攻击范围，然后就要切换了哟
- switchState是我自己写的代码，先exit再切换，最后enter
- 里面的参数是new一个不同的状态

3.面向玩家
- LookAt
4.攻击冷却

5.AttackPlayer() 函数
- 如果没获取玩家组件，就获取
- 调用TakeDamage函数
- debug调试

6.Exit（）离开攻击状态

### 流程

假设一个敌人的数据：

攻击范围：2米

攻击间隔：1.5秒

伤害值：10

玩家进入攻击范围 → 切换到 Attack 状态

Enter() 重置计时器 → 立即攻击一次

玩家受伤，掉10血

计时器开始倒计时1.5秒

如果玩家一直没跑出范围：

每帧面向玩家

1.5秒后再次攻击

如果玩家跑出2米外：

立即切回 Chase 状态去追

# 在ChaseState添加切换
Update中更改
```
float distance =
    Vector3.Distance(enemy.transform.position,
                     enemy.player.position);

if (distance <= enemy.attackRange)
{
    enemy.SwitchState(new EnemyAttackState(enemy));
    return;
}
```

距离小于攻击距离，我就要切换状态咯。开始Attack状态

补充**return;**的作用

对比有return区别

有return
追逐距离够了之后，直接切换到攻击状态，下一帧，敌人在攻击状态，执行攻击逻辑

无return

敌人追逐，距离够了 → 切换到攻击状态，切换后还继续执行追逐代码，敌人还在移动

总结return作用

"既然我要切换到新状态了，当前状态的工作就到此为止，不要再做任何事"
