# 题目：215. 数组中的第K个最大元素

**Day13-1**

解题思路：

## ①堆排序/优先队列（推荐）

## ② 快速选择算法（最优）
```
class Solution {
public:
    int findKthLargest(vector<int>& nums, int k)
{
    int left =0,right = nums.size()-1;
  while(1)
  {
    int pos = partition(nums,left,right);
    if(pos == k-1)
      return num[pos];
    if(pos>k-1)
      right = k-1;----------->出错了
    if(pos<k-1)
      left = k+1;----------->出错了
  } 
}
private:
partition(vector<int> nums,int left,int right)
{
    int pivotIndex = left +rand()% (right-left+1);

    swap(nums[pivotIndex],nums[right]);
    int pivot = nums[right];
    int i = left;
    for(int j=0;j<right;j++)---------->j = left
{
      if(nums[j]>pivot)
      {  swap(nums[i],nums[j]);
          i++;
      }
}
swap(nums[i],nums[right]);
return i;
}
};
```

代码学习

Partition 函数流程
