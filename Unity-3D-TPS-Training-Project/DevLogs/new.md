## 敌人拥有生命系统


**目标：**

攻击 → 敌人掉血 → 血量归零 → 敌人消失


### Step1：
新建脚本：EnemyHealth.cs

```
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log(gameObject.name + " HP: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " died.");
        Destroy(gameObject);
    }
}

```

我的解释：

- 这个和我做的2D游戏关于Enemy血量很相似

**第一部分：变量**

```
public int maxHealth = 50;
private int currentHealth;

```

- 公共的最大生命，可以更改

- 当前的生命不可以随意更改，需要用函数和方法

** 第二部分：Start（）**

```
void Start()
    {
        // 将当前生命值设置为最大生命值
        currentHealth = maxHealth;
    }
```

初始生命，就是游戏开始的时候设置start。

第三部分： TakeDamage（）函数（受伤函数）



覆盖逻辑：

受到伤害之后，扣除血量，更新当前血量

输出：在TCL输出扣血的事实

如果血量被扣到≤0，那么就让他Die（）

第四部分：Die()函数

输出：某某死亡

然后还有Destroy函数


### Step2

在场景里那个 Cube：

选中 Cube

Add Component → EnemyHealth

它现在正式成为“敌人”。

### Step 3
让攻击系统真正造成伤害

回到 PlayerAttack.cs，修改 Attack()


在射线判断的地方加上这一部分：

```
    EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
    if (enemy != null)
    {
        enemy.TakeDamage(attackDamage);
    }
```

代码的解读


EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();

hit.collider

这是射线击中的碰撞体（Collider）

可能是敌人的碰撞体，也可能是墙、地面等其他物体

与此同时，用GetComponent，尝试获得这个碰撞体的EnemyHealth组件
如果获取到了组件：
返回组件引用
否则，只能给enemy一个null

接下来就要判断了。如果获取到组件，也就是 进入了if语句，
于是对enemy进行伤害
