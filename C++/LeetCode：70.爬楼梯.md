## 问题描述

你正在爬楼梯，需要 n 步才能到达楼顶。
每次你可以爬 1 步或 2 步。
问：有多少种不同的方法可以爬到楼顶


## 思路分析

我感觉这一类题目是固定思路.

但是依旧要分析一下：

- dp[i] 表示爬到第i阶楼梯的方法；

- 要达到第i阶，有两种可能
  - 从i-1阶段爬1步
  - 从i-2阶段爬2步
  - dp[i] = dp[i-1] + dp[i-2](关键导出)
 
  最后一步这个决策是怎么导出来的？

## 推导过程:

**Step1:最后一步的决策**

第i阶楼梯的时候，思考你是怎么上来的？

只有两种可能性：

从i-1阶爬1步上来
从i-2阶爬2步上来

**Step2：数学**

定义

假定dp[i] = 到达第i阶段的总方法数量

分类计数的原理

根据两种可能性，分成两类

最后一步爬一阶

如果最后是爬一阶，那么在这之前，一定在第i-1阶。

如果最后是爬两阶，那么在这之前，你一定在第 i-2 阶。

加法原理



而这两种爬法是**互斥的**（最后一步要么是1阶段，要么是2阶段，不能同时发生），所以总方法数量是两者之和

```
dp[i] = dp[i-1] + dp[i-2]

```
**Step3 : 需要记住的**

推理的时候，是从后向前推理，写代码则是从前向后
从第一节楼梯开始，第二节楼梯开始

int first =1；
int second =2；



## 代码实现
```
if(n<=2) return n;

int first = 1;
int second = 2;

for(int i = 3; i<=n; i++)
{
  int third = first + second;
   first =second;
   second =third;

}
return second;
```

**标准答案**
```

class Solution {
public:
    int climbStairs(int n) {
        // 处理小规模情况
        if (n <= 2) return n; // 对于小情况直接返回
        // n=1: 只有一种方法（爬1步）
        // n=2: 有两种方法（1+1 或 2）
        
        // 初始化滚动变量
        int first = 1;  // dp[1]：爬到第1阶的方法数
        int second = 2; // dp[2]：爬到第2阶的方法数
        
        // 从第3阶开始计算
        for (int i = 3; i <= n; i++) {
            // 计算dp[i] = dp[i-1] + dp[i-2]
            int third = first + second;
            
            // 更新滚动变量，为下一次循环做准备
            first = second;   // 原来的second变成新的first（dp[i-1]）
            second = third;   // 新计算的third变成新的second（dp[i]）
        }
        
        return second; // 循环结束时，second就是dp[n]
    }
};
```
