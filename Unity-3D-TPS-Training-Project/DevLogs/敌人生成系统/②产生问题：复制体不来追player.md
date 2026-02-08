为什么旧的Enemy会追逐player而新生成的复制体不会追逐？

问题现象：

新生成的敌人：

出现了

但是不追玩家或者不攻击

根本原因：


EnemyAI 里有一句：

public Transform player;


并且原来的 Enemy 是在场景里手动拖了 Player 的引用的。
但是：

通过 EnemySpawner 动态生成的 Prefab，Player 引用是空的！

解决方案

在EnemySpawner修改

原来的函数：

```
void SpawnEnemy()
{
    Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
    randomPos.y = 1f;

    Instantiate(enemyPrefab, randomPos, Quaternion.identity);
    currentEnemies++;
}
```

new：

```
void SpawnEnemy()
{
    Vector3 randomPos = transform.position + Random.insideUnitSphere * spawnRadius;
    randomPos.y = 1f;

    GameObject enemyObj = Instantiate(enemyPrefab, randomPos, Quaternion.identity);
    EnemyAI ai = enemyObj.GetComponent<EnemyAI>();
    if (ai != null)
        ai.player = GameObject.FindGameObjectWithTag("Player").transform;

    currentEnemies++;
}

```

理解：

GameObject enemyObj = Instantiate(enemyPrefab, randomPos, Quaternion.identity);

enemyObj：保存新创建的敌人对象的引用

关键部分：

EnemyAI ai = enemyObj.GetComponent<EnemyAI>();

从刚创建的敌人对象上获取 EnemyAI 脚本组件。这个组件应该包含敌人的人工智能逻辑（如移动、攻击等）。

设置玩家目标

if (ai != null)
    ai.player = GameObject.FindGameObjectWithTag("Player").transform;


if (ai != null)：安全检查，确保对象确实有 EnemyAI 组件

GameObject.FindGameObjectWithTag("Player")：在场景中查找带有 "Player" 标签的游戏对象

.transform：获取该对象的Transform组件（包含位置、旋转、缩放信息）

ai.player = ...：将找到的玩家Transform赋值给AI脚本的player变量，让敌人知道要追踪谁
