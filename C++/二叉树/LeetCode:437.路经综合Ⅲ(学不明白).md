```
        10
       /  \
      5    -3
     / \     \
    3   2     11
   / \   \
  3  -2   1
```

第一步：初始化

空路径-前缀和为零出现一次

第二步：DFS遍历

**先访问节点10**，路径和=：10+0=10

检查哈希表，有没有2，没有，则返回count = 0

记录当前前缀和: prefixSumCount[10] = 1

哈希表现在: {0:1, 10:1}

**接下来访问5**

当前路径和是15 = 10+5

寻找前缀和 ：15-8=7

检查哈希：有没有7，没有count依旧为零

现在看一个有结果的

**接下来访问3**

当前节点：3

路径之和 = 15+3 = 8

需要找前缀和呢：18-8=10

有没有10？ 有！(节点10的前缀和) → count = 1

## 代码学习：

### 第一部分：类定义和成员变量
- 哈希表：key=前缀和，value=这个前缀和出现的次数
- 作用：快速找到之前——是否出现过某个前缀和
- target存起来，递归不用一直传
### 第二部分：DFS
int dfs，返回以node为根的子树，有多少个路径和等于target

如果是node为空节点，那么说明没有路径

currentSUm =  currentSum+node->val

把当前节点的值加上，比如，到节点10，currentSum从0变成10

long long need = currentSum - target;

这个need字如其人，就是要找的前缀和

举例子：在节点10时，need=10-8=2，要找之前有没有前缀和为2的节点

  int count = prefixSumCount[need];

这后半部分是查哈希表，然后查出来之后，看多少个节点的前缀和等于need
  
在节点3时，need=10，查表发现节点10出现过 → count=1

 prefixSumCount[currentSum]++;

记录当前节点的前缀和：把自己记下来，给子孙节点用

count += dfs(node->left, currentSum);
count += dfs(node->right, currentSum);

去右子树继续找，把找到的路径数累加


 prefixSumCount[currentSum]--;
- 回溯：离开当前节点前，把自己从哈希表里删掉
- 作用：不影响其他分支的计算
- 例：离开节点10时，把 prefixSumCount[10] 减回0

### 第三部分：主函数
```
public:
    int pathSum(TreeNode* root, int targetSum) {
        target = targetSum;  
        
        prefixSumCount[0] = 1;         
        return dfs(root, 0);  
    }
};
```
- 把targetSUm 赋值给target是为了保存目标值
- prefixSumCOunt：初始化前缀和0，出现为一次，则说明从根节点开始路径，如果某个节点是8，那么就找到了
- 从根节点开始，当前的初始和为0
