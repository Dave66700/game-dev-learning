 # 找到字符串中所有字母异位词

给定两个字符串 s 和 p，找到 s 中所有 p 的 异位词 的子串，返回这些子串的起始索引。不考虑答案输出的顺序。

<img width="411" height="328" alt="image" src="https://github.com/user-attachments/assets/5002a66c-e356-4a25-8679-7b849d4e7e69" />


## 我的思路：

- 长度和p相等 left-> left+2(示例一)
- vector<int> result;
- unoredred_set;

## 统计p中每个字符的出现次数
```
   vector<int> p_cnt(26, 0);
        for (char c : p) {
            p_cnt[c - 'a']++;
        }
```
这是一个关键代码

## 逻辑理解：

vector<int> p_cnt(26, 0);
- 创建一个长度为26的整型向量。所有元素初始化为0
- 26对应的是26个英文字母
- p_cnt[0] 对应a，p_cnt[1]对应b...

for(char c : p)
遍历p中的每个字符

p_cnt[c - 'a']++;
c - 'a' 将字符转换为索引（'a' → 0，'b' → 1，...，'z' → 25）

对应位置的字母数量加一



我想要重新调整一下变量名，帮助我理解。
```
class Solution {
public:
    vector<int> findAnagrams(string s, string p) 
    {
        vector<int> result;
        int sLen = s.length(), pLen = p.length();

        if(sLen < pLen) return {};  // 边界条件

        // 频率数组
        vector<int> pFreq(26, 0), windowFreq(26, 0);
        
        // 统计p的频率
        for(char ch : p) pFreq[ch - 'a']++;
        
        // 初始化第一个窗口
        for(int i = 0; i < pLen; i++) 
            windowFreq[s[i] - 'a']++;
        
        // 检查第一个窗口
        if(pFreq == windowFreq) result.push_back(0);
        
        // 滑动窗口
        for(int right = pLen; right < sLen; right++) {
            int left = right - pLen;  // 明确左边界
            windowFreq[s[right] - 'a']++;  // 右边进
            windowFreq[s[left] - 'a']--;   // 左边出
            
            if(pFreq == windowFreq) 
                result.push_back(left + 1);  // left+1是新窗口起点
        }
        
        return result;
    }
};
```
