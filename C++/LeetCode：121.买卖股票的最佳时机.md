## 思路分析：

给定数组prices，其中prices[i]是第i天的股票价格。

你最多只能完成一笔交易（买入一次、卖出一次）设计算法，算出最大利润。

**核心思路：**

维护一个变量：min_price，表示当前为止历史最低的价格

维护一个变量：max_price，表示当前最大利润，当前价格减去历史最低。

**关键：**

遍历的过程当中，先判断min_price是否为最低，如果是最低，那么说明当前是最低价格，否则要更新的。

如果不更新，则计算当前价格减去min_price，与历史记录当中的max_price 进行比较，如果利润高于旧的，则更新。

为什么只需要遍历一次？

我的认识：因为在遍历的过程中，只需要找到最低价格，每次用当天的金额减去最低价格，求利润，保持最大的部分即可


**边界：**

第一，如果天数小于两天，无从谈起利润。

第二，如果每天价格都小于前一天，无从谈利润。


## 代码部分

```
class Solution {
public:
    int maxProfit(vector<int>& prices) {
if(price.size()<2) return 0;

int min_price = prices[0];
int max_profit = 0;

for(int i = 0;i<prices.size();i++)
{
if(min_price > prices[i])
  {min_price = price[i];}

else
  {
    max_profit = max(max_profit,price[i] - min_price);
  }

}
        return max_profit;
    }
};


```
