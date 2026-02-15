# Hit Feedback

## 实现的功能：

当子弹击中敌人或玩家受到攻击时，游戏会短暂地放慢时间，增强打击感和视觉反馈

### 命中反馈（hitstop）

当攻击真正命中那一帧：
- 世界瞬间慢下来
- 攻击有重量
- 原理来自：短暂修改 Time.timeScale

### 原理理解

Unity有一个全局时间倍率

Time.timeScale

Time.timeScale =1f;——>默认状态

Time.timeScale = 0.1f;——>整个游戏会接近暂停


## Step1：HitStop脚本

### 单例模式实现
```
public static HitStop instance;

private void Awake()
{
    instance = this;
}
```

### 什么是单例模式？

单例模式是一种设计模式，确保一个类只有一个实例，并提供一个全局访问点。


通俗理解：

政府：一个国家只有一个中央政府

### 为什么需要单例模式：

- 在游戏开发中的常见需求

- 游戏管理器：只需要一个GameManager
- 资源管理器：集中加载和管理资源
- 网络连接：只需要一个网络连接实例
- 音频管理器：统一控制所有音效

### 对于单例模式理解

- 确保全局只有一个hitstop实例
- 静态引用：其他脚本可以通过 HitStop.instance.Stop() 直接调用
- Awake方法：在游戏对象初始化时设置实例引用

### 为什么要这样写单例模式？

- 为什么是Static?
  - 让变量属于类本身而不是某个具体对象
  - 实现"全局唯一访问点"

- 为什么在Awake里面写？
  - 最早执行：确保其他脚本在 Start 中就能使用
  - 避免空引用：防止其他脚本访问时 instance 还没赋值
 
- 总结
  - static：创建全局访问点
  - Awake：尽早建立链接
  - Instance = this：将具体对象注册为全局代表
 

- 我这个类只有一个实例，为了方便大家使用，我把自己的地址放在一个大家都知道的地方

## 公共方法 - Stop

```
public void Stop(float duration, float slowScale)
{
    StartCoroutine(HitStopCoroutine(duration, slowScale));
}
```
两个参数：
- duration：停顿效果的持续时间
- slowscale：时间缩放值（0-1之间，0=完全停止，0.2=20%速度）
- 协程帮助处理时间缩放


## 核心协程

```
IEnumerator HitStopCoroutine(float duration, float slowScale)
{
    // 保存原始时间缩放值
    float originalTimeScale = Time.timeScale;

    // 设置时间缩放（放慢游戏）
    Time.timeScale = slowScale;
    
    // 等待指定的实际时间（不受Time.timeScale影响）
    yield return new WaitForSecondsRealtime(duration);

    // 恢复原始时间缩放
    Time.timeScale = originalTimeScale;
}
```

- Time.timeScale
  - Unity的时间缩放系统
  - 0.5半速
  - 0完全暂停
 
-  WaitForSecondsRealtime
  -  不受时间缩放影响：即使Time.timeScale=0，这个等待也会按实际时间执行（realtime）
  -  确保停顿效果持续指定的真实时间，而不是游戏时间

### 为什么要使用：WaitForSecondsRealtime？

因为：如果 timeScale = 0.1

普通：WaitForSeconds()

也会变得很慢，而Realtime无视时间的缩放。


## Step 2 - 挂载

创建一个空物体

挂载我的HitStop

## Step3 - 在真正命中的时候触发

**找到造成伤害的地方**
也就是在Attckplayer里面

下面添加

<img width="714" height="185" alt="image" src="https://github.com/user-attachments/assets/1dbeb5a1-1e2c-47c4-866e-848ae36d0ad1" />


HitStop.instance.Stop(0.08f, 0.05f);


### 在哪里添加停顿更好？

HitStop 应该写在“攻击发起者”的命中逻辑里

绝对不能那个写在takeDamage里面

### 问题：失去控制权

因为takedamage的指责应该只有——扣血

不知道是谁攻击的，也不知道什么攻击

怎么做手感更好？

- 玩家攻击 = 主动行为
- 敌人攻击 = 被动受击

游戏设计里：

- 主动反馈永远强于被动反馈
