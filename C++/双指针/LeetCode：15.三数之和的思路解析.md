
class Solution {
public:
    vector<vector<int>> threeSum(vector<int>& nums) 
    {
        先用sort排序
        
        去重检查，例如避免重复的固定数字，跳过第二个-1

<img width="765" height="624" alt="image" src="https://github.com/user-attachments/assets/434eea0f-32a3-424d-882b-b0fdc422d255" />


优化的部分：
        
第一个：最小的三数之和都大于零，那么不可能有解
        
第二个：当前数加最大的两个数还小于0，那就不用判断了，当前数太小，直接i++
        
双指针搜索：

双指针循环

存储结果

去重处理

继续搜索
              
    }
    返回最后的数组
};



