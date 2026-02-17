# GameOver系统目标

当玩家死亡时：

- 停止游戏逻辑
- 敌人不再攻击
- 不再生成敌人
- 玩家不能移动
- 时间暂停

## Step1 让 PlayerHealth 通知 GameManager

扣血的位置，大概增加

```
void Die()
{
    Debug.Log("Player Dead");

    GameManager.Instance.SetState(GameManager.GameState.GameOver);
}

```
带来的变化

以前：Player死亡 = 血量变成零

现在：player死亡 = 通知整个游戏

## Step2 GameManager处理gameover

```
public void SetState(GameState newState)
{
    currentState = newState;

    if (currentState == GameState.GameOver)
    {
        OnGameOver();
    }
}

```
逻辑：如果GameState到达了Gameover，那么就执行OnGO这个函数

有一句需要额外理解：currentState = newState;
- 它的意思是：把新的游戏状态，存进 currentState 这个变量里。
- 类比：现在的状态比作一个盒子，这行代码就是可以把新的状态替换
- newState 是 参数

调用的人决定它是什么

### 举个例子：

GameManager.Instance.SetState(GameManager.GameState.GameOver);

此时newState = GameOver

然后执行currentState = newState；

当前的游戏状态就正式进入了GameOver

之前是 currentState = Playing

现在是 currentState = GameOver

### 为什么不直接写GameOver？

currentState = GameState.GameOver;

当然ok

但是写成参数，就让函数变成了通用状态转换器
```
SetState(GameState.Start);
SetState(GameState.Playing);
SetState(GameState.GameOver);
```
### OnGameOver函数
```
void OnGameOver()
{
    Debug.Log("GAME OVER");

    Time.timeScale = 0f;
}

```
Time.timeScale 是什么？

Unity 的世界时间倍率：

1 = 正常
0 = 时间停止
## Step3 防止玩家还能操作
Update最上面加上
```
if (GameManager.Instance.currentState != GameManager.GameState.Playing)
    return;
```
## 敌人AI也加同保护
EnemyAI.cs 的 Update 开头
```
if (GameManager.Instance.currentState != GameManager.GameState.Playing)
    return;

```

