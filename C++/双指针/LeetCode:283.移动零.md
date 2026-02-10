# 283.移动零


思路分析


<img width="470" height="555" alt="image" src="https://github.com/user-attachments/assets/cc4de762-e76a-4688-af48-b9f8488a2b19" />


分为快慢指针

快指针用来遍历

慢指针用来指向下一个非零元素应该放置的位置。

遍历结束之后，在慢指针后面添加0即可

```
class Solution {
public:
    void moveZeroes(vector<int>& nums) {
        int n = nums.size();
        int slow = 0;  // 指向当前可放置非零元素的位置

        // 第一次遍历：将所有非零元素移到前面
        for (int fast = 0; fast < n; ++fast) {
            if (nums[fast] != 0) {
                nums[slow] = nums[fast];
                ++slow;
            }
        }

        // 第二次遍历：将剩余位置补零
        for (int i = slow; i < n; ++i) {
            nums[i] = 0;
        }
    }
};
```
