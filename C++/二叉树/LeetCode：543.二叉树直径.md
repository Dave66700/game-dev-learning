# 543. 二叉树的直径

给你一棵二叉树的根节点，返回该树的 直径 。

二叉树的 直径 是指树中任意两个节点之间最长路径的 长度 。这条路径可能经过也可能不经过根节点 root 。

两节点之间路径的 长度 由它们之间边数表示。

<img width="487" height="561" alt="image" src="https://github.com/user-attachments/assets/dc1f1120-7b40-4ee0-bb95-0ec1fb4083ba" />

## 递归过程模拟

```
    1
   / \
  2   3
 / \
4   5
```

**1.节点4**

leftDepth = 0, rightDepth = 0

直径 = 0 + 0 = 0（更新maxDiameter）

返回深度 = max(0,0) + 1 = 1（告诉父节点：从我这里往下走，最深能走1步）

2.节点5

**3.节点2**

leftDepth = 1（来自节点4），rightDepth = 1（来自节点5）

直径 = 1 + 1 = 2（更新maxDiameter）

返回深度 = max(1,1) + 1 = 2（告诉父节点：从我这里往下走，最深能走2步）

4.节点3

返回深度为1

**5.节点1**

leftDepth = 2（来自节点2），rightDepth = 1（来自节点3）

直径 = 2 + 1 = 3（更新maxDiameter）

返回深度 = max(2,1) + 1 = 3（但实际上根节点不需要再往上返回了）

```
/**
 * Definition for a binary tree node.
 * struct TreeNode {
 *     int val;
 *     TreeNode *left;
 *     TreeNode *right;
 *     TreeNode() : val(0), left(nullptr), right(nullptr) {}
 *     TreeNode(int x) : val(x), left(nullptr), right(nullptr) {}
 *     TreeNode(int x, TreeNode *left, TreeNode *right) : val(x), left(left), right(right) {}
 * };
 */
class Solution {
private:
    int maxDiameter = 0;

    int depth(TreeNode* node)
    {
        if(node == nullptr) return 0;

        int leftDepth = depth(node->left);
        int rightDepth = depth(node->right);

        maxDiameter = max(maxDiameter,leftDepth+rightDepth);

        return max(leftDepth,rightDepth)+1;
    }
public:
    int diameterOfBinaryTree(TreeNode* root) 
    {
        maxDiameter = 0;
        depth(root);
        return maxDiameter;
    }
};


```

总结思路：

定义全局变量，写一个递归函数计算深度，递归计算左右子树，更新最大直径，返回当前节点深度，用于给父节点明确最深走到哪里。
