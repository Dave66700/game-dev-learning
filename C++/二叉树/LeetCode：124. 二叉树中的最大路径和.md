


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
    int maxSum = INT_MIN; // 全局变量，记录最大路径和

    // 递归函数：返回从当前节点向下的最大单侧贡献
    int maxGain(TreeNode* node) {
        if (node == nullptr) {
            return 0;
        }

        // 1. 递归计算左右子树的最大贡献
        // 如果子树贡献为负，则舍弃，取0
        int leftGain = max(maxGain(node->left), 0);
        int rightGain = max(maxGain(node->right), 0);

        // 2. 计算经过当前节点的最大路径和
        int currentPathSum = node->val + leftGain + rightGain;

        // 3. 更新全局最大路径和
        maxSum = max(maxSum, currentPathSum);

        // 4. 返回当前节点的最大单侧贡献给父节点
        return node->val + max(leftGain, rightGain);
    }

public:
    int maxPathSum(TreeNode* root) {
        maxGain(root);
        return maxSum;
    }
};

````
