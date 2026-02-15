# 最大子数组和

## 关键思想

每个位置，维护“以当前元素结尾的：最大子数组和”

## 动态规划（Kadane算法）

模拟执行：

nums = [-2, 1, -3, 4, -1, 2, 1, -5, 4]

第一步：初始化

- currentMax = -2
- globalMax = -2

第二步：i = 1, nums[1] = 1

- currentMax = max(-3, 1 + (-3)) = max(-3, -2) = -2
- globalMax  = max(1, -2) = 1

第三步：i = 2, nums[2] = -3

//要么保存当前的新的，要么加上旧的。看谁更大。

- currentMax = max(-3,-3+1)->-2
- globalMax  = max(1, -2)->1


