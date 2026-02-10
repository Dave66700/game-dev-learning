# 15. 三数之和

给你一个整数数组 nums ，判断是否存在三元组 [nums[i], nums[j], nums[k]] 

满足 i != j、i != k 且 j != k ，同时还满足 nums[i] + nums[j] + nums[k] == 0 。

请你返回所有和为 0 且不重复的三元组。

注意：答案中不可以包含重复的三元组。

<img width="454" height="454" alt="image" src="https://github.com/user-attachments/assets/cd90bff9-13d6-411c-a6c7-28f3abf9a291" />


我的想法：

暴力三重循环，直接将等于零解出

能不能固定前两个数，得到一个值，然后再让指针遍历剩下的部分？这样固定需要一个On，遍历需要一个On。

是否有更好的？

更好的思路：排序 + 双指针

- 排序数组：先对数组排序
- 固定一个数字： 遍历数组，对于每个nums[i],后面的部分寻找两个数，使得三个数为0；
- 双指针：对于固定nums[i]——双指针在[i+1，n-1]寻找
  - 计算 sum = nums[i] + nums[left] + nums[right]
  - 如果 sum < 0，说明太小了，left++
  - 如果 sum > 0，说明太大了，right--
  - 如果 sum == 0，记录结果，并跳过重复元素


使用双指针：

```
vector<vector<int>> result;

int n = nums.size();

sort(nums.begin(),nums.end());

for(int i =0;i<n-2;i++)-------->为什么要n-2，后面时会用到nums[i+2]

if(i>0 && nums[i] == nums[i-1])------------>相同的数跳过

{continue;}
//优化
if(nums[i]+nums[i+1]nums[i+2]>0)
{break;}
if(num[i]+nums[n-2]+nums[n-1]<0)
{continue;}


int left = i+1;
int right =n-1;

while(right>left)
{
  int sum = nums[i]+nums[left]+nums[right];

  if(sum<0)
  {left++;}
  else if(sum>0)
  {right--;}
  else
  {
  result.push_back(nums[i],nums[left],nums[right]);


//去重，跳过左侧相同的数
  while(left<right && nums[left] == nums[left+1]){
  left++;}
 

// 去重：跳过右侧相同的数
 while (left < right && nums[right] == nums[right - 1]) {
  right--;}
  
  left++;
  right--;

}

return result;
```
