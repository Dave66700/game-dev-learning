手感终结感阶段

目前：

血量≤0，直接就将敌人给destroy了

目标：

敌人被打死→ 飞出去/倒下 → 过一会儿再消失

## Death Impact（死亡冲击）

死亡的时候实现的内容
- 停止敌人AI
- 物理接管
- 给一个大冲击力
- 延迟销毁

### Step1

修改EnemyHealth函数

把原来的：

Destroy(gameObject);

改成：

**StartCoroutine(DeathSequence());**

一句话概括角色死亡的过程：死亡不是瞬间消失，而是**有动画，有延迟，有序列的完整过程**。

对比：

不用协程：瞬间完成，没有过程感
```
void Die()
{
    Debug.Log("玩家死亡");
    gameObject.SetActive(false); // 瞬间消失
    GameManager.Instance.GameOver(); // 瞬间游戏结束
}
```

用协程：有序列、时许的死亡过程
```
void Die()
{
    StartCoroutine(DeathSequence()); // 启动死亡序列
}
```


### 新增死亡流程

代码部分：
```
IEnumerator DeathSequence()
{
    // 1. 关闭AI
    EnemyAI ai = GetComponent<EnemyAI>();
    if (ai != null)
        ai.enabled = false;

    // 2. 解除刚体限制，让它能倒
    if (rb != null)
    {
        rb.constraints = RigidbodyConstraints.None;

        // 3. 向后+向上冲击
        Vector3 deathForce = (transform.forward + Vector3.up) * hitForce * 2f;
        rb.AddForce(deathForce, ForceMode.Impulse);
        rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
    }

    // 4. 等几秒再删除
    yield return new WaitForSeconds(3f);

    Destroy(gameObject);
}

```

这一部分学习

- 第一步：关闭AI
  -    先从Enemy AI获取，然后用ai调用这个功能关闭AI，也就是enabled = false
  -    否则尸体还会追

- 第二步 解出刚体限制
  - constraints =  RigidbodyConstraints.None;
  - 为什么要判断rb 不为空
  - 
- 第三步 物理反馈的被击飞
  - Vector3 deathForce = (transform.forward + Vector3.up) * hitForce * 2f;
    - ①计算死亡冲击力：前二者是向量的加法，表明尸体飞出的方向
    - 相加结果 = (0, 1, 1)——标准化后方向：大约45度斜向上前方
    - 乘力度：hitforce*2f
  - rb.AddForce(**deathForce**, ForceMode.Impulse);
    - ②施加冲击力（第一个变量用到的就是上一步算出来的）
    - 死亡效果用 Impulse 最合适，瞬间施加全部力
    - Random.insideUnitSphere * 5f
  - ③计算随机扭矩
    - 随机方向
    - 长度0-5之间的扭矩向量
    - rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
  - ④施加随机旋转力
    - AddTorque 的作用： (x, y, z) = 绕X轴旋转x，绕Y轴旋转y，绕Z轴旋转z
- 第四步 等几秒之后在删除
  - yield return new WaitForSeconds(3f);


### 拓展一个内容

Random.insideUnitSphere

 返回单位球体内的一个随机点
 
 范围：x,y,z都在-1到1之间，长度<=1

示例可能值：
 (0.3, -0.7, 0.1)  长度≈0.77
 
(-0.5, 0.2, -0.8) 长度≈0.96

（0.1, 0.0, 0.4)   长度≈0.41
