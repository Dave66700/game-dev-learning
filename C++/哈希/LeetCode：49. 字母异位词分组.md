通过例子解释

输入: strs = ["eat", "tea", "ate", "bat", "tab"]

预期输出: [["eat","tea","ate"], ["bat","tab"]]

输入部分-经过代码-输出部分



## step①：创建哈希表

键（key）：排序后的字符串（如“aet”）
值（value）：原始字符串 (如 ["eat","tea","ate"]）

unordered_map<string,vector<string>>map;
## step②：循环
for(int i =0;i<strs.size();++i)


 **获取当前字符串**

    第1次循环: i=0, strs[0]="eat"
    
    第2次循环: i=1, strs[1]="tea"
    
    第3次循环: i=2, strs[2]="ate"
   
    string& current_str = strs[i];

  **创建排序用的副本**
  
    string key = current_str;
    
 **有了副本就可以排序了**
    
    第1次: key="eat" → 排序 → "aet"
    
    第2次: key="tea" → 排序 → "aet"
    
    sort(key.begin(), key.end());

 **根据排序的结果将原始的字符串添加到对应的分组。**

     map[key].push_back(current_str);

  **这一句是相当于根据key 把字符安排到了合适的哈希表中**


## Step③创建结果容器，然后遍历哈希表，提取分组

```
for (unordered_map<string, vector<string>>::iterator it = map.begin(); 
     it != map.end(); 
     ++it)
```

unordered_map<string,vector<string>>:哈希表类型

::iterator : 类型的内部迭代器类型

it： 迭代器变量名

map.begin():返回指向哈希表第一个元素的迭代器

作用：创建一个迭代器 it，并让它指向哈希表的开头。

it! = map.end()

it：当前的迭代器

map.end()：返回指向哈希表末尾之后的迭代器

!=：不等于运算符

## 可视化过程：

```
map.begin() → 指向第一个元素 ("aet": ["eat","tea","ate"])
map.end() → 指向末尾之后的位置（不指向任何有效元素）

it = map.begin()  // it现在指向第一个元素
```

第一次循环
```
条件检查：it != map.end() ✓ (it指向第一个元素，有效)
执行循环体：result.push_back(it->second)  // 添加["eat","tea","ate"]
执行更新：++it  // it现在指向第二个元素
```
第二次循环
```
条件检查：it != map.end() ✓ (it指向第二个元素，有效)
执行循环体：result.push_back(it->second)  // 添加["bat","tab"]
执行更新：++it  // it现在指向map.end()
```
第三次循环
```
条件检查：it != map.end() ✗ (it指向map.end())
循环结束
```
