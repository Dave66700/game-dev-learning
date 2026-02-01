

## 公交车站比喻
先用换乘公交车的例子去理解这个内容

每个车站（数组位置）都有一个数字，表示从这站最多能坐几站车

你要从第一站坐到最后一站

问：最少要换乘几次公交车？

## 具体例子
nums = [2, 3, 1, 1, 4]
你要从站0坐到站4

**第一趟车（初始状态）：**

jumps = 0（还没换乘过）

cur_end = 0（当前车从站0出发）

farthest = 0

**第一站（站0）：**

站牌写着：这站的车最远能到站2

farthest = max(0, 0+2) = 2

到站了（i == cur_end）：这是当前车的终点站！

必须换车：jumps = 1，下一趟车最远到站2（cur_end = 2）

**第二站（站1）：**

站牌写着：这站的车最远能到站4

farthest = max(2, 1+3) = 4（哇！比之前更远！）

还没到当前车的终点（i != cur_end），继续坐

**第三站（站2）：**

站牌写着：这站的车最远能到站3

farthest = max(4, 2+1) = 4（还是4最远）

到站了（i == cur_end）：又到终点了！

必须换车：jumps = 2，下一趟车最远到站4（cur_end = 4）

**检查：**

当前车最远能到站4，而终点就是站4

游戏结束！ 总共换乘2次



## 核心思路

### BFS层序遍历思想

可以把问题看作在图上进行BFS

- 每一层表示当前跳跃能到达的位置范围
- 下一层，表示当前层所有位置能达到的最远距离
- 目标找到到达终点最少层数

### 贪心策略

- jumps 当前跳跃次数
- cur_end 当前跳跃能达到的最远位置
- farthest 下一次跳跃到达最远位置

### 遍历数组

- 更新farthest = max(fathest, i + nums[i])
- 当到达 cur_end 时，说明需要进行下一次跳跃：(这里就可以理解为这辆车到站了，一定要换车)

  - jumps++

  - cur_end = farthest

  - 如果 cur_end >= n-1 提前结束
 ## 具体操作过程
 ```
 一步步执行过程
初始状态：

位置:    0    1    2    3    4
数值:    2    3    1    1    4

jumps = 0, cur_end = 0, farthest = 0
第1步：i = 0

farthest = max(0, 0 + nums[0]) = max(0, 0+2) = 2
当前位置 i=0 能跳到最远位置：i + nums[i] = 0+2 = 2

farthest 更新为 2


if (i == cur_end)  // 0 == 0 ✓
到达当前跳跃的边界


jumps++           // jumps = 1
cur_end = farthest // cur_end = 2

当前状态：jumps=1, cur_end=2, farthest=2
表示：已经跳了1次，这次跳跃能到的最远位置是2
第2步：i = 1

farthest = max(2, 1 + nums[1]) = max(2, 1+3) = 4
当前位置 i=1 能跳到最远位置：1+3 = 4

farthest 更新为 4（发现更远的地方！）


if (i == cur_end)  // 1 == 2 ✗
还没到当前跳跃的边界，继续探索


当前状态：jumps=1, cur_end=2, farthest=4
表示：还在第1次跳跃范围内，但发现了一个能到4的好位置
第3步：i = 2

farthest = max(4, 2 + nums[2]) = max(4, 2+1) = 4
当前位置 i=2 能跳到最远位置：2+1 = 3

farthest 保持 4（3比4小）


if (i == cur_end)  // 2 == 2 ✓
到达当前跳跃的边界！


jumps++           // jumps = 2
cur_end = farthest // cur_end = 4

当前状态：jumps=2, cur_end=4, farthest=4
表示：已经跳了2次，这次跳跃能到的最远位置是4（终点！）

if (cur_end >= n - 1)  // 4 >= 4 ✓
{
    break;  // 提前结束，已经可以到达终点
}
结果
return jumps = 2
```

## 我认为很重要的复习的部分

- i == cur_end 不是意味着"到达可以跳转的部分"，
- 而是表示 "已经走完了当前这一次跳跃的所有可能选择，必须进行下一次跳跃了"。

- 
## 算法步骤

```
class Solution {
public:
    int jump(vector<int>& nums)
{
        int n =nums.size();
        if(n<=1)  return 0;
        int jump =0;
        int cur_end =0;
        int farthest = 0;
      for(int i=0;i<n-1;i++)
      {
          ---->这句很重要farthest = max(farthest, i + nums[i]);
          if(i == cur_end)
          {
            jump++;
            cur_end = fathest;
          }
        if(cur_end>= n-1)
          break;
      }
}
    
};
```
