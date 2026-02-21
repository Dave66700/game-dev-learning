## Step 1 修改EnemyAI添加状态机

敌人可能存在的所有状态:
```
public enum EnemyState
{
    Idle,
    Chase,
    Attack,
    Hit,
    Dead
}
```

| 状态     | 含义      |
| ------ | ------- |
| Idle   | 待机 / 巡逻 |
| Chase  | 追玩家     |
| Attack | 攻击      |
| Hit    | 被打中硬直   |
| Dead   | 死亡      |

同一时间只能处于一个状态

## 修改Update部分

### ①检查：游戏是否正在进行
```
if (GameManager.Instance.currentState != GameManager.GameState.Playing)
    return;
```
游戏不是Playing状态，敌人停止思考

### ②检查：敌人是否已经死亡
```
if (currentState == EnemyState.Dead)
    return;
```
死了就别再执行AI，否则会有尸体继续追AI

### ③计算与玩家距离（AI关于是否要追玩家的依据）

```
float distance =
    Vector3.Distance(Player.position, transform.position);
```
### ④核心：状态机switch
```
switch(currentState)
```
根据敌人当前的状态执行不同的逻辑

这是有限状态机FSM

- Chase状态
  - 判断是否进入攻击范围
  - 进入范围，执行Attack
```
case EnemyState.Chase:
if (distance < attackRange) 
urrentState = EnemyState.Attack;
else ChasePlayer();
break;
```
- Attack状态
 ```
case EnemyState.Attack:
if (distance > attackRange)
currentState = EnemyState.Chase;
else AttackPlayer();
break;
 ```
- Hit状态
```
  ase EnemyState.Hit:
// 受击状态期间，什么都不做
 break;
```

