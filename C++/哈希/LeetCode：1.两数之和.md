# 两数之和

给定一个整数数组 nums 和一个整数目标值 target，请你在该数组中找出 
和为目标值 target 的那 两个 整数，并返回它们的数组下标。

最直接的方法——双重循环：时间复杂度 O(n²)

哈希表 来把查找时间降到 O(1)

## 思路：

1. 遍历数组，对于当前元素，求补数
2. 在已经构建的哈希表，查找是不是存在compliment
3. 如果存在，两数之和等于target，返回compliment下标
4. 如果不存在就把这个数存到hash表里面，后续继续查找

```
class Solution {
public:
    vector<int> twoSum(vector<int>& nums, int target) 
    {
        unordered_map<int,int>hash_map;
        for(int i =0,i<nums.size();i++)
        {
            int compliment = target - nums[i];
            if(hash_map.find(compliment) != hash_map.end())
            return{hash_map[compliment],i};---->等价的：return vector<int>{hash_map[complement], i};
        }
        hash_map[nums[i]]=i;
        return{};
    }
};
```


## 代码关键点
**1. 哈希表定义**
unordered_map<int, int> hash_map;

 - 第一个int：键（key），存储数组元素的值
 - 第二个int：值（value），存储该元素在数组中的下标
 - 形成映射：数组值 -> 数组下标

**2. 核心查找逻辑**
if (hash_map.find(complement) != hash_map.end())
- 解释：在哈希表中查找complement
- find(complement)：返回指向键为complement的元素的迭代器
- hash_map.end()：返回指向哈希表末尾的迭代器（表示"未找到"）
- !=：如果find返回的不是end()，说明找到了complement
   
**3. 返回语句**
return {hash_map[complement], i};
- {}：C++11列表初始化语法
- 等价于：return vector<int>{hash_map[complement], i};
- 创建一个临时vector<int>，包含两个元素并返回

**4. 存储语句**
  hash_map[nums[i]] = i;
- 存储当前元素到哈希表
-  nums[i]：作为键（数组元素的值）
-  i：作为值（该元素的下标）
-  这样后续元素可以快速查找自己的补数

## 一些新的知识：

1.hash_map.find(key)

- find() 是 unordered_map 的成员函数

- 它接收一个键作为参数

- 返回值是一个迭代器（iterator）

2.返回值的两种情况

- 键存在，返回对应的迭代器
- 键不存在，返回特殊迭代器

3. hash_map.end()

- 这是一个尾后迭代器，指向容器中最后一个元素之后的位置
- 它不指向任何有效元素，只用作"不存在"的标志

4.翻译if (hash_map.find(complement) != hash_map.end())

如果哈希表找到了compliment，不等于end()空状态，从而意味着找到了

没找到，则会将这个部分加到哈希的最后

两个哈希表的插入操作。



**1.operator[]特性**
```
hash_map[key] = value;
```
这个操作实际上做了两件事

如果 key 不存在：创建新键值对 {key, value}

如果 key 已存在：更新该键对应的值为新值

**2.为什么要用insert**
```
// 用 insert 也可以，但更复杂
hash_map.insert({nums[i], i});
// 或者
hash_map.emplace(nums[i], i);

```

但 insert 有个问题：如果键已存在，它不会更新值。

operator[] 则总是确保键对应的值是我们想要的。


