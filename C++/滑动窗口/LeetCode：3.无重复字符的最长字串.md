# 3. 无重复字符的最长子串

给定一个字符串 s ，请你找出其中不含有重复字符的 最长 子串 的长度。

<img width="474" height="408" alt="image" src="https://github.com/user-attachments/assets/9422dadf-778e-4f12-a4c2-d1a386038bf3" />



我的思考：
我的想法，能否写一个unordered_map。用键值对，记录已经出现过的的部分，然后可以找到无重复的最长子串。

结果真正的思路分析，还真的有哈希表


## 滑动窗口法

左指针：表示窗口的左边界

右指针：表示窗口的右边界（可以用for循环的i表示）

用一个哈希表（字典）记录每个字符最后一次出现的下标。

具体过程：

- 遍历字符串，每次right向右移动1
- 如果当前字符s[right]再哈希表已经存在，并且上一次出现的位置在left右边，也就是在窗口内，那么就要
- 移动left到上一次出现上一次出现的位置+1，保证窗口内无重复。
- 更新哈希表的位置
- 计算当前窗口的长度、更新最大长度

## 代码学习

- unordered_map<char,int> lastPos
    - 存储每个字符出现的最后位置
    - 键：字符
    - 值：下标
- left =0；
  - 左边界，窗口内保证没有重复
  - 随时更新：if (lastPos.find(c) != lastPos.end() && lastPos[c] >= left)
  - 字符 c 之前出现过，并且上一次出现位置在当前窗口内
- right更新，右边界依次扩展
- 窗口长度：right - left +1
## 代码实现
```
class Solution {
public:
    int lengthOfLongestSubstring(string s) 
    {
        unordered_map<char,int> lastPos;
        int left =0;
        int maxLen = 0;

        for(int right = 0;right<s.size();right++)
        {
            char c = s[right];

            if(lastPos.find(c)!=lastPos.end() && lastPos[c]<right)
            {
                left = lastPos[c] +1;
            }
            lastPos[c] = right;

            maxLen = max(maxLen,right-left+1);
        }
        return maxLen;
    }
};
```
