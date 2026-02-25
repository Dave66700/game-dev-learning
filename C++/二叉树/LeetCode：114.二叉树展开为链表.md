# 114. 二叉树展开为链表

给你二叉树的根结点 root ，请你将它展开为一个单链表：

展开后的单链表应该同样使用 TreeNode ，其中 right 子指针指向链表中下一个结点，而左子指针始终为 null 。

展开后的单链表应该与二叉树 先序遍历 顺序相同。



## 思路
1.外层循环，遍历所有节点

2.判断左子树，有左子树才需要调整

3.寻找前驱节点（重要）

4.连接右子树+移动左子树

5.继续遍历

## 模拟root = [1,2,5,3,4,null,6]

```
    1
   / \
  2   5
 / \   \
3   4   6
```

执行2，知道有左子树，然后找到左子树的最后一个节点，也就是右子树的前驱节点4

最后把5、6连接到4之上

接下来把左子树2变成右子树2

```
1
 \
  2
 / \
3   4
     \
      5
       \
        6
```


再次执行一次这个逻辑

```
1
 \
  2
   \
    3
     \
      4
       \
        5
         \
          6
```

## 补充代码部分
```
class Solution {
public:
    void flatten(TreeNode* root) {
        TreeNode* current = root;          // 1. 初始化当前节点
        while (current != nullptr) {        // 2. 遍历所有节点
            if (current->left != nullptr) { // 3. 存在左子树才处理
                // 4. 找到左子树的最右节点（前驱）
                TreeNode* predecessor = current->left;
                while (predecessor->right != nullptr) {
                    predecessor = predecessor->right;
                }

                // 5. 关键步骤：将右子树挂到前驱节点上
                predecessor->right = current->right;

                // 6. 将左子树移到右边，并置空左指针
                current->right = current->left;
                current->left = nullptr;
            }
            // 7. 移动到下一个节点
            current = current->right;
        }
    }
};
```

### 第3步的重点理解：
```
TreeNode* predecessor = current->left;
while (predecessor->right != nullptr) {
    predecessor = predecessor->right;
}
```

- reeNode* predecessor = current->left;
  - current 是节点 1
  - current->left 是节点 2
  - 所以 predecessor 初始指向节点 2
 
- while (predecessor->right != nullptr) { predecessor = predecessor->right; }
  - 这是一个循环，目的是沿着右指针一直向右走，直到找到最后一个节点。
  - **第1次循环判断：**
    - predecessor 指向节点 2
    - 检查 predecessor->right：节点 2 的右孩子是节点 4（不为空）
    - 条件为真，进入循环体
    - predecessor = predecessor->right; → predecessor 现在指向节点 4
  - **第2次循环判断：**
    - predecessor 指向节点 4
    - 检查 predecessor->right：节点 4 的右孩子是 nullptr
    - 条件为假，退出循环
