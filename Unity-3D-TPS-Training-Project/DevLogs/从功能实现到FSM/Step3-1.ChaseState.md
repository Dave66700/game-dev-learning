# ChaseState

EnemyAI自己不怎么会写行为了。但是敌人可移动

旧版本里面追击逻辑
```
transform.LookAt(player);
transform.position += transform.forward * moveSpeed * Time.deltaTime;
```
## Step1 新的部分
```
using UnityEngine;

public class EnemyChaseState : EnemyState
{
    public EnemyChaseState(EnemyAI_FSM enemy) : base(enemy) { }

    public override void Enter()
    {
        Debug.Log("Enter Chase");
    }

    public override void Update()
    {
        if (enemy.player == null) return;

        float distance =
            Vector3.Distance(enemy.transform.position,
                             enemy.player.position);

        // ===== 朝向玩家 =====
        enemy.transform.LookAt(enemy.player);

        // ===== 向前移动 =====
        enemy.transform.position +=
            enemy.transform.forward *
            enemy.moveSpeed *
            Time.deltaTime;

        // （先不切状态，下一步再加）
    }

    public override void Exit()
    {
        Debug.Log("Exit Chase");
    }
}
```


结构：
- public EnemyChaseState(EnemyAI_FSM enemy) : base(enemy) {}
  - 三个重写的方法
  - override void Enter、Update、Exit（）
  - 这里需要说明的是，方法命名暗示了行为Enter和Exit就说明，只执行一次，分别暗示进入状态执行一次，离开状态执行一次

①Enter方法
```
public override void Enter()
{
    Debug.Log("Enter Chase");  // 在控制台输出提示
}
```
- 敌人开始追逐玩家时执行
- 仅一次执行
②Update方法
```
public override void Update()
{
    if (enemy.player == null) return;  // 安全判断：如果没有玩家就退出

    // 1. 计算距离
    float distance = Vector3.Distance(
        enemy.transform.position,  // 敌人位置
        enemy.player.position      // 玩家位置
    );

    // 2. 朝向玩家
    enemy.transform.LookAt(enemy.player);  // 让敌人始终面朝玩家

    // 3. 向前移动
    enemy.transform.position +=
        enemy.transform.forward *     // 向前方向
        enemy.moveSpeed *              // 移动速度
        Time.deltaTime;                 // 每帧时间（使移动与帧率无关）
}
```

思路很好理解，但是新的有一个点需要加深：之前都是

transform.LookAt(player);

现在是：

enemy.transform.LookAt(enemy.player);

有了Enemy之后，我就知道具体是什么过程里，指定了是谁的东西

旧的版本，敌人自己管自己，不用加前缀
我的新方法：状态类遥控敌人，必须加 enemy. 告诉电脑"我要遥控的是这个敌人，不是我自己

关于这里为什么使用enemy.xxx

其实我没有学到很明白——先记住吧

<img width="662" height="388" alt="image" src="https://github.com/user-attachments/assets/35c1b00f-18b6-4452-9d8b-dfbbe85f9164" />


## Step2 让FSM开始进入Chase

Void Start内改成SwitchState(new EnemyChaseState(this));

## Step3 Inspector检验

挂载、运行测试。

## 总结：

以前：
```
EnemyAI
 ├─ if
 ├─ if
 ├─ if
 ├─ attack
 ├─ chase
 ├─ death
```

现在：
```
EnemyAI（只负责切换）
        ↓
   当前 State 决定行为
```

③Exit方法
