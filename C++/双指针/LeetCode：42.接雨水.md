# 42. 接雨水

给定 n 个非负整数表示每个宽度为 1 的柱子的高度图，计算按此排列的柱子，下雨之后能接多少雨水。

<img width="458" height="602" alt="image" src="https://github.com/user-attachments/assets/15cfa842-3200-4600-ad2b-b98cefb2127b" />


双指针法

初始化左指针、右指针

维护左边最大值和右边最大值

left<right是大循环的基础
  - 如果 height[left] < height[right]：
    - 判断height>=left_max，更新left_max
    - or 接雨水 left_max - height[left]
    - 最后执行left++
  - 否则 height[left] >= height[right]
    -  height[right] >= right_max,更新 right_max
    -  or接雨水 right_max - height[right]
    -  right --;



思考过程：

- 边界检查
- 初始化
  - 左右指针、左右当前最高的柱子、总雨水
  - 主循环
  - 情况1：左边柱子较矮
    - 当前左边柱子最高，更新最大
    - 当前左边柱子比之前的矮，计算雨水 
  - 情况2：右边柱子较矮
    -  当前右边柱子最高，更新最大
    -  当前右边柱子比之前的待，计算雨水
 

```
class Solution {
public:
    int trap(vector<int>& height) {
        int n = height.size();
        if (n < 3) return 0;  // 边界检查
        
        int left = 0, right = n - 1;  // 初始化左右指针
        int left_max = 0, right_max = 0;  // 左右两边当前最大值
        int result = 0;  // 总雨水量
        
        while (left < right) {  // 主循环条件
            if (height[left] < height[right]) {  // 情况1：左边柱子较矮
                if (height[left] >= left_max) {  // 子情况1.1：当前柱子是新的左边最高
                    left_max = height[left];     // 更新左边最大值
                } else {                         // 子情况1.2：当前柱子比左边最高矮
                    result += left_max - height[left];  // 计算雨水量
                }
                left++;  // 左指针右移
            } else {                           // 情况2：右边柱子较矮或相等
                if (height[right] >= right_max) {  // 子情况2.1：当前柱子是新的右边最高
                    right_max = height[right];     // 更新右边最大值
                } else {                           // 子情况2.2：当前柱子比右边最高矮
                    result += right_max - height[right];  // 计算雨水量
                }
                right--;  // 右指针左移
            }
        }
        
        return result;  // 返回总雨水量
    }
};
```

