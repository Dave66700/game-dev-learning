~~ ps：这个题目好奇怪恶心... ~~

## 函数：buildTreeHelper

TreeNode* buildTreeHelper(int preLeft, int preRight, int inLeft, int inRight)

 - preLeft,preRight：当前子树在先序数组中的范围 [preLeft, preRight]
 - nLeft, inRight：当前子树在中序数组中的范围 [inLeft, inRight]

函数分部详解
## 第一部分
递归终止条件：if (preLeft > preRight || inLeft > inRight) return nullptr;

作用：当数组范围无效时，返回空指针。

## 第二部分：确定根节点
```
int rootVal = preorder[preLeft];
TreeNode* root = new TreeNode(rootVal);
```

作用：从先序数组的第一个元素取出根节点的值，并创建节点。

为什么是preLeft？

先序遍历一定是根-左-右，所以第一个就是根------------------------

## 第三部分 中序遍历根节点位置

通过哈希表快速找到根节点在中序数组中的索引。
为什么要这样？

- 找到根在中序的位置后，就能知道：
  - 左边 [inLeft, rootIndex-1] 是左子树

  - 右边 [rootIndex+1, inRight] 是右子树

## 第四部分 计算左子树大小

int leftSubtreeSize = rootIndexInInorder - inLeft;

左子树节点数 = 根在中序的位置 - 当前中序范围的起点

## 第五部分 构建左子树

```
root->left = buildTreeHelper(
    preLeft + 1,              // 左子树先序起点
    preLeft + leftSubtreeSize, // 左子树先序终点
    inLeft,                    // 左子树中序起点
    rootIndexInInorder - 1     // 左子树中序终点
);
```
## 第六部分 构建右子树

```
root->right = buildTreeHelper(
    preLeft + leftSubtreeSize + 1, // 右子树先序起点
    preRight,                        // 右子树先序终点
    rootIndexInInorder + 1,          // 右子树中序起点
    inRight                           // 右子树中序终点
);
```

最后返回根节点

## all
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
    // 哈希表，存储中序序列中元素值到索引的映射，便于快速查找
    unordered_map<int, int> inorderIndexMap;
    // 先序序列的引用，方便递归函数使用
    vector<int> preorder;

public:
    TreeNode* buildTree(vector<int>& preorder, vector<int>& inorder) {
        this->preorder = preorder;
        // 构建哈希表
        for (int i = 0; i < inorder.size(); i++) {
            inorderIndexMap[inorder[i]] = i;
        }
        // 调用递归函数，初始范围是整个数组
        return buildTreeHelper(0, preorder.size() - 1, 0, inorder.size() - 1);
    }

private:
    /**
     * 递归辅助函数
     * @param preLeft 当前子树在先序数组中的左边界索引
     * @param preRight 当前子树在先序数组中的右边界索引
     * @param inLeft 当前子树在中序数组中的左边界索引
     * @param inRight 当前子树在中序数组中的右边界索引
     * @return 构建完成的子树根节点
     */
    TreeNode* buildTreeHelper(int preLeft, int preRight, int inLeft, int inRight) {
        // 递归终止条件：没有节点可以构建了
        if (preLeft > preRight || inLeft > inRight) return nullptr;

        // 1. 先序数组的第一个节点就是当前子树的根节点
        int rootVal = preorder[preLeft];
        TreeNode* root = new TreeNode(rootVal);

        // 2. 在中序数组中找到根节点的位置
        int rootIndexInInorder = inorderIndexMap[rootVal];

        // 3. 计算左子树的节点数量
        int leftSubtreeSize = rootIndexInInorder - inLeft;

        // 4. 递归构建左子树
        // 左子树的先序范围: 从 preLeft+1 开始，共 leftSubtreeSize 个元素
        // 左子树的中序范围: 从 inLeft 到 rootIndexInInorder - 1
        root->left = buildTreeHelper(preLeft + 1, preLeft + leftSubtreeSize,
                                     inLeft, rootIndexInInorder - 1);

        // 5. 递归构建右子树
        // 右子树的先序范围: 从 preLeft + leftSubtreeSize + 1 到 preRight
        // 右子树的中序范围: 从 rootIndexInInorder + 1 到 inRight
        root->right = buildTreeHelper(preLeft + leftSubtreeSize + 1, preRight,
                                      rootIndexInInorder + 1, inRight);

        return root;
    }
};
```
