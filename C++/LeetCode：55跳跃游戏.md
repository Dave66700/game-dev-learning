## 题目分析：

给定一个非负整数数组nums，最初位于数组的第一个下标。数组的元素表示，该位置可以跳跃的最大长度。

判断是不是可以达到最后一个下标

 ## 算法步骤

i> max_reach 则说明到达不了i，直接返回false

每次循环 max_reach = max(i+nums[i],max_reach)

进行更新

当max_reach>=n-1 则说明一定可以到达目标，从而提前返回true


## 核心思路

遍历数组，对于每个位置i

如果，位置i超过了之前能达到的最远距离，说明无法到达当前位置，直接返回false

否则，更新max_reach

最大可达距离==最后一个下标，返回true

## 为什么这里贪心算法有效？

因为具体是怎么跳的我们不关心，我们只关心，最远能到达哪里。

### 边界

数组长度为1，直接返回true

如果第一个位置为零，且数组长度>1，则返回false

代码实现

```
class Solution {
public:
    bool canJump(vector<int>& nums) 
    {
        int max_reach = 0+nums[0];
        int n = nums.size();

        for(int i =0;i<n;i++)
        { 
          if(i>max_reach)
          {return false;}
          max_reach = max(i+nums(i),max_reach)---------->封号，数组的用法nums[i]

          if(maxe_reach>n-1)-------------------> max笔误
          return true;
        }
    }
};
```
