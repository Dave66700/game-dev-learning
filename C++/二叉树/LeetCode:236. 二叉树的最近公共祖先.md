236. 二叉树的最近公共祖先
给定一个二叉树, 找到该树中两个指定节点的最近公共祖先。

百度百科中最近公共祖先的定义为：“对于有根树 T 的两个节点 p、q，最近公共祖先表示为一个节点 x，

满足 x 是 p、q 的祖先且 x 的深度尽可能大（一个节点也可以是它自己的祖先）。”


<img width="432" height="790" alt="image" src="https://github.com/user-attachments/assets/bb85e730-1df5-497a-a4da-b5dc5bc15c5f" />

```
/**
 * Definition for a binary tree node.
 * struct TreeNode {
 *     int val;
 *     TreeNode *left;
 *     TreeNode *right;
 *     TreeNode(int x) : val(x), left(NULL), right(NULL) {}
 * };
 */
class Solution {
public:
    TreeNode* lowestCommonAncestor(TreeNode* root, TreeNode* p, TreeNode* q) {
        // 1. 递归终止条件：如果当前节点为空，或者等于p或q，直接返回当前节点
        if (root == nullptr || root == p || root == q) {
            return root;
        }

        // 2. 在左子树中查找p和q
        TreeNode* left = lowestCommonAncestor(root->left, p, q);
        // 3. 在右子树中查找p和q
        TreeNode* right = lowestCommonAncestor(root->right, p, q);

        // 4. 核心判断逻辑
        // 如果左子树和右子树都找到了（即都不为空），说明p和q分别在当前节点的两侧
        if (left != nullptr && right != nullptr) {
            return root; // 当前节点就是最近公共祖先
        }

        // 5. 如果只有一边找到了，返回找到的那一边的结果
        // （意味着两个节点都在左子树或都在右子树）
        if (left != nullptr) {
            return left;
        }
        if (right != nullptr) {
            return right;
        }

        // 6. 左右都没找到（理论上不会发生，因为p和q一定存在）
        return nullptr;
    }
};
```
