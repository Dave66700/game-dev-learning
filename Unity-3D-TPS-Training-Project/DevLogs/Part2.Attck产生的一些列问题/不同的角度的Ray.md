## 先说说为什么再调试的时候需要两种Ray代码

版本一和版本二起点相同（都是摄像机位置）

方向可能略有不同（取决于摄像机设置）

两者都不是TPS的真实弹道（真实弹道从枪口出发）

画出来是为了调试，实际游戏不需要画前两种

## 最初只有：
```
Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f, Screen.height/2f));
Debug.DrawRay(ray.origin, ray.direction * attackRange, Color.red, 2f);
```

- 这条线代表：从摄像机到屏幕中心点的方向

- 结合第一行，我知道的是起点：摄像机位置

- 方向：从摄像机到屏幕中心点的方向

### 第一部分：

- Camera.main	场景中标记为"MainCamera"的摄像机
- ScreenPointToRay()：屏幕坐标转换为世界空间射线的方法
- new Vector3(Screen.width/2f, Screen.height/2f)：屏幕中心点的像素坐标

### 第二部分：

- ray.origin	射线的起点，等于 Camera.main.transform.position
- ray.direction	射线方向，通过屏幕坐标计算得出
- 红
- 2s显示

## 另一种表示方式
```
Debug.DrawRay(cameraTransform.position, cameraTransform.forward * attackRange, Color.red, 1f);
```

它代表的是：摄像机物体自身的“正前方”

- 起点：摄像机位置

- 方向：摄像机的正前方向量

**简单说：两种ray都是"找到瞄准点的方法"，而不是"实际的射击弹道"。在TPS中，实际的射击应该从枪口出发！**

至于最后那个正确的射线，完整的部分最后再补充吧嘻嘻。


但是这两种方式**再选用的时候**，还是不一样滴！

🅰 Transform.forward 方式

优点：

简单

性能微高

适合无准星 / 固定视角游戏

缺点：

如果准星不在正中心 → 打不准

UI 准星移动、狙击镜、偏移摄像机都会失效

🅱 ScreenPointToRay 方式（游戏标准）

优点：

子弹永远打“准星位置”

支持：

动态准星

狙击镜

偏移镜头

武器抖动

缺点：

多一步数学转换（但几乎无成本）
