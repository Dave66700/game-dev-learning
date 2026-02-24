# C++动态内存管理深度讲解
## ——从内存布局到游戏开发中的内存陷阱

在游戏开发中，内存管理直接决定了游戏的性能和稳定性。

## 一、内存布局

五个区域

栈（局部变量，函数参数。向下增长）、堆（动态分配内存。向上生长）、

全局/静态变量、常量（字符串）、机器码

从栈-代码区，地址由高到低

2.为什么需要动态内存

游戏场景里面，敌人数量是动态的

int enemyCount = getEnemyCountFromLevel();

栈上的数组，大小必须编译时确定

堆上动态分配，运行时确定大小

## 二、深入new 和 delete

1.new表达式做了什么？

player* p = new Player（"Hero",1）;

- 分配内存，在堆上分配足够内存

- 构造对象，在分配好的内存调用Player函数

- 返回指针，返回该内存指针

2.delete表达式做了什么

- 调用析构函数，清理资源
- 释放内存
- 指针悬空（p依然在，但是内存无效）

3.内存布局
<img width="492" height="522" alt="image" src="https://github.com/user-attachments/assets/8cdc2d98-1e66-42be-a7c2-cf11228242ef" />


## 三、常见陷阱

- 内存泄漏
  - 处理逻辑，忘记delete，每次执行都会泄露内存
- 悬空指针
  - 对于delete指针，要e = nullptr 
- 双重释放
  - 不能delete两次
 
## 四、游戏开发的实际应用

### 1.对象池模式（避免了频繁的new/delete）

在游戏中，频繁创建销毁对象（子弹、粒子）会导致内存碎片和性能问题

对象池，本质是预先创建好的仓库

说说子弹系统

**不好的方式**
```
void shoot() {
    Bullet* b = new Bullet();  // 分配内存 + 构造对象
    // 子弹飞行...
    delete b;                  // 析构 + 释放内存
}
```

最大的问题：打100发子弹就要new/delete100回

**对象池的方式**

```
// 对象池方式：子弹是"循环利用"的
class BulletPool {
    Bullet bullets[100];       // 提前造好100颗子弹
    bool used[100];            // 哪些正在使用
    
    Bullet* get() {
        for (int i = 0; i < 100; i++) {
            if (!used[i]) {
                used[i] = true;
                return &bullets[i];  // 拿一个现成的
            }
        }
        return nullptr;  // 子弹用完了（弹夹空了）
    }
    
    void back(Bullet* b) {
        // 把子弹还回去，别人还能用
    }
};
```

**为什么new/delete不好？**

- new/delete，要申请，要把内存还给系统
- 对象池，内存直接批好了，直接用就可以了

对比：

|概念	| 对象池|
|----|-----|
|预先创建	|提前new好一堆对象|	
|复用	|用完重置状态再给别人用	|
减少开销|	避免频繁new/delete

### 2.内存对齐与缓存性能

**2-1 内存对齐**

1、本质

内存对齐 = CPU读取数据的停车位规则

不可以压线随便停

2、cpu读取内存的规则

```
struct Player {
    int id;        // 4字节
    char level;    // 1字节
    // 按理说总共5字节就够了，但实际...
};
```

- 实际内存布局
- [id(4字节)] [level(1字节)] [空闲(3字节)]
- 这就是"对齐"：为了让下一个变量在4字节边界上

3、为什么对齐

- 不对齐会导致读一个数据的时候可能访问两次

**2-2 缓存性能**

1、本质理解

经常用的数据挨着放，CPU能一次性预读到缓存	

避免CPU"等数据"，提高运行速度

## 深入理解 new/delete的重载

Unity、Unreal 等引擎通过重载 new/delete 实现：

内存池管理

内存泄漏跟踪

内存使用统计

碎片整理

为什么游戏引擎要重载？

根本原因：游戏是“实时”的，不能卡

展开：
1.性能优化

默认的new，是系统调用的比较慢
重载的new，是指针移动，从预先申请的大内存里面获取

2.内存跟踪

引擎可以记录每个new的分配

```
void* operator new(size_t size, const char* file, int line) {
    void* ptr = malloc(size);
    MemoryTracker::record(ptr, size, file, line);  // 谁？在哪？申请了多少？
    return ptr;
}
```

第三行，可以看到会有一个记录，记录了谁泄露内存

3.内存对齐

重载之后的new，会保证整齐，恰好符合CPU

```
// 游戏中的数据需要特殊对齐（比如16字节）
struct Matrix4x4 {  // 4x4矩阵，常用于3D变换
    float m[16];    // SSE指令要求16字节对齐
};

// 重载new保证对齐
void* operator new(size_t size) {
    return aligned_alloc(16, size);  // 分配在16的倍数地址上
}

```

4.内存池

### 例子：Unreal Engine
```
class UObject {
    // 重载new运算符
    void* operator new(size_t Size) {
        // 使用引擎的内存分配器
        return FMemory::Malloc(Size);
    }
    
    void operator delete(void* Ptr) {
        FMemory::Free(Ptr);
    }
};
```

FMemory::Malloc做了什么？

如果是小内存（< 16KB）：用专用内存池

如果是大内存：用平台分配器

记录分配信息用于调试

保证内存对齐
