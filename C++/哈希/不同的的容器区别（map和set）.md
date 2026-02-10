## unordered_map 和 unordered_set

**1. unordered_set - 集合（只有键）**
```
// 只存储值，没有键值对的概念
unordered_set<int> mySet = {1, 2, 3, 2, 1};
```
内部：{1，2，3}——> 自动去重的

只能检查某个值是否存在
不能存储额外的信息，下标计数什么都没有




**2. nordered_map - 映射（键值对**）
```
unordered_map<string, int> studentScores;

//插入键值对：
studentScores["Alice"] = 95;
studentScores["Bob"] = 88;
```


题目中的区别

- 最长连续序列（Longest Consecutive Sequence）→ 用 unordered_set
  - 只需要检查数字是否存在，不需要额外信息
  - unordered_set<int> numSet(nums.begin(), nums.end());
- 字母异位词分组（Group Anagrams）→ 用 unordered_map
    - 键: 排序后的字符串 "aet"
    - 值: 原始字符串列表 ["eat", "tea", "ate"]
- 两数之和
    - 查找时：不仅要知道补数是否存在，还要知道它的下标
    - unordered_map<int, int> map;  键: 数组值，值: 数组下标
