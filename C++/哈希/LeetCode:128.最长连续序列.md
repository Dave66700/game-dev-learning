# 题目：128. 最长连续序列

给定一个未排序的整数数组 nums ，找出数字连续的最长序列（不要求序列元素在原数组中连续）的长度。

请你设计并实现时间复杂度为 O(n) 的算法解决此问题

<img width="423" height="305" alt="image" src="https://github.com/user-attachments/assets/1215c2b7-2a2e-4aa5-8c98-e32f1b160cd8" />

## 具体算法步骤

1. 去重+构建哈希
2. 遍历集合，判断是不是起点
  - 如果num-1存在于集合，则说明num不是起点
  - num-1不存在于集合，num是起点
3. 看num+1、num+2是不是存在
4. 更新最长长度


## 基础知识补充

**①去重**
去除重复的元素，保留唯一的元素。

unordered_set<int> numSet(nums.begin(), nums.end());

两个参数分别表示起始迭代器和结束迭代器

unordered_set 自动去重

例子
```
vector<int> nums = {1, 2, 2, 3, 3, 3};
unordered_set<int> numSet(nums.begin(), nums.end());
```

numSet = {1, 2, 3}

**②numSet.find()**

find() 是 unordered_set 这个容器的成员函数，是哈希表自带的功能。

- find() 函数的功能：在集合中查找元素
- 参数：要查找的值
- 返回值：迭代器（找到返回指向该元素的迭代器，没找到返回end()）

只有unordered_set 才有成员函数，其他容器没有find方法

例子：
```
unordered_set<int> numSet = {1, 2, 3};

auto it1 = numSet.find(2);  // 找到2，返回指向2的迭代器
auto it2 = numSet.find(4);  // 没找到4，返回numSet.end()
```
**③numSet.find(num - 1) != numSet.end() 到底是什么意思？**

- end() 的作用：
  - end() 返回一个"特殊位置"的迭代器
  - 它不指向任何实际元素，只表示"末尾之后"
  - [1] → [2] → [3] → [end]
- 检查元素**是否存在**的标准写法

**④for循环语法糖**
基本写法
```
for (元素类型 变量名 : 容器) {
    // 使用变量名访问当前元素
}
```
## 代码部分
```
class Solution {
public:
    int longestConsecutive(vector<int>& nums) {
    if(nums.empty())
    return 0;
    
    unordered_set
        
    }
};

```
