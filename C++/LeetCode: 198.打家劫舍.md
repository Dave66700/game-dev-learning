## 核心思路

本质上是一个序列决策问题，每一件房子：可以选择偷或者不偷。有一个核心限制，不能同时连续偷两间房子。

## 核心思路：

现在你在第i间房子，有两个策略。

- 偷第i间，那么就不能偷第i-1间。获得的最大金额是（i）+（i-2
  间为止的最大金额）
- 不偷i间，那么能获得的，就是第i-1间的最大金额。

## 如何表示呢？

用一个数组dp来记录状态：

dp[i]表示： 考虑偷到第i间房子，能偷盗的最高金额。

-  dp[i] 并不一定代表“我偷了第 i 间房子”，它只代表“考虑到第 i 间为止的最佳方案金额”。

现在用dp[i] 表示收益大的那个。

**dp[i] = max(dp[i-1],nums[i]+dp[i-2])**

## 初始化

dp[0]：只有一间房子，那必须偷！dp[0] = nums[0]

dp[1]：有两间房子，选金额大的那间偷。dp[1] = max(nums[0], nums[1])


```cpp
class Solution {
public:
    int rob(vector<int>& nums)
    {
      int n = nums.size();
      if(n == 0) return 0;
      if(n == 1) return nums[0];
      if(n == 2) return max(nums[0],nums[1]);

      vector<int>dp(n,0);
      dp[0] = nums[0];
      dp[1] = max(nums[0],nums[1]);
      
      for(int j = 2;j<n;j++)
      {
          dp[j] = max(dp[j-1],dp[j-2]+nums[j]);
      }
        return dp[n-1];
    }
};

```
## 现在掌握的DP思路
从这几道题，已经掌握了动态规划的核心模式：

爬楼梯：基础递推，dp[i] = dp[i-1] + dp[i-2]

杨辉三角：二维DP，row[j] = 上一行[j-1] + 上一行[j]

打家劫舍：带选择的DP，dp[j] = max(不选当前, 选当前+前前状态)
