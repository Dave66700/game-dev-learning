# 11. 盛最多水的容器


给定一个长度为 n 的整数数组 height 。有 n 条垂线，第 i 条线的两个端点是 (i, 0) 和 (i, height[i]) 。

<img width="466" height="585" alt="image" src="https://github.com/user-attachments/assets/a5761215-7a07-4857-b5f7-a2b2cc80c237" />


## 思考过程
### 容量公式：

容量 = 宽度*高度

高度其实其实由更矮的确定

### 初始状态：

两个指针，分别在数组的两端，宽度最大

移动具体过程：
- 如果移动较高的指针，向着双方靠近的地方，宽度减小，容器量不会增大
- 如果移动较低的指针，宽度减小，但是有可能高度增加，从而增加容量

### 算法步骤：

初始化左指针 left = 0 ，右指针 right = n-1

计算当前容量：min（height[left],height[right]) * (right - left);

更新最大容量

比较height[left]和height[right]
- height[left] < height[right]，左指针右移
- 右指针左移

最后，两指针相遇



## 第一次错误代码：

```
class Solution {
public:
    int maxArea(vector<int>& height) {
        int right = height.size() - 1;
        int left = 0;
        int maxArea =0;
        

        for(int i = 0;i<right;i++)
        {
            if(height[left]<= height[right])
            {
                left++;
            }
            else
            {
                right--;
            }
            int currentArea =(height[right] - height[left]) * 
            min(height[right],height[left]);
            maxArea = max(maxArea,currentArea);
            
        }
        return maxArea;
    }
};
```

循环条件错误：应该用 while(left < right) 而不是 for 循环

面积计算错误：宽度应该是 (right - left)，不是 (height[right] - height[left])

逻辑顺序错误：应该先计算面积，再移动指针



## 正确思路：

```
while (左指针 < 右指针) {
    1. 计算当前面积 = min(左高, 右高) × (右-左)
    2. 更新最大面积
    3. 如果左高 < 右高: 左指针++
       否则: 右指针--
}
```
## 自己更正:


```
class Solution {
public:
    int maxArea(vector<int>& height) {
        int right = height.size() - 1;
        int left = 0;
        int maxArea =0;
        

        while(left<right)
        {
            int currentArea =(right-left) * min(height[right],height[left]);
            maxArea = max(maxArea,currentArea);
            
            if(height[left]<= height[right])
            {
                left++;
            }
            else
            {
                right--;
            }
            
            
            
        }
        return maxArea;
    }
};
```
