## 与完全平方数对比：完全相同。

核心思路完全一致：

- 完全平方数：dp[i] = min(dp[i], dp[i - j*j] + 1)，其中 j*j 是平方数。

- 零钱兑换：dp[i] = min(dp[i], dp[i - coin] + 1)，其中 coin 是硬币面额。

- 定义：dp[i] 表示凑出最少金币数量
- 状态转移分析
  - dp[i] min(dp[i],(dp[i - 所有可能硬币] + 1)
 
例子推演（coins = [1, 2, 5], amount = 11）i = 11	
- dp[10]+1=3
- dp[9]+1=3
- dp[6]+1=3

## 代码理解：
vector<int> dp(amount + 1, amount + 1);

初始化数组的时候，大小为amount + 1，需要从dp[0]到dp[n]

初始值设为 amount +1 代表无穷大 ，因为最多用amount个1元金币

        dp[0] = 0; // 凑出金额0需要0个硬币

  for（int i = 1;i <=amount; i++）
  
  {
  
     // 4. 内层循环：尝试每一种硬币
     
        for(int j = 0;j<coin.size();j++)
        
        int coin = coins[j];------>这里就是当前硬币的金额
        
        if(coin<=i)-------->金额大小肯定要符合常理
        
        dp[i] = min(dp[i],dp[i - coin]+1);
        
  }
  
  return dp[amount]>amount? -1:dp[amount];
  
  
  这一句话是用来判断的，如果说，dp[amount]>amount 初始值是最大的叫做：dp[amount] == amount+1;
