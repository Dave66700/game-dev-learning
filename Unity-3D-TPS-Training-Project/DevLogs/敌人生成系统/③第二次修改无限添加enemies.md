
第一次修改之后，发现 会产生的问题是：迅速的无限产生的enemies，

虽然有击中特效，但是EnemyAI是失效的，所以不会行动

这是我的分析：

Spawner 给的是 ai.player，但 EnemyAI 里用的是 Player（大写）

但是有一句EnemyAI.Player == null;

playerHealth = player.GetComponent<PlayerHealth>();

其实应该就有问题了。

修改方案：

Step1

将player全改成小写。EnemyAI和Spawner对齐

Step2

不要再Start拿到PlayerHealth



