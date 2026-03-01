## 215. 数组中的第K个最大元素

给定整数数组 nums 和整数 k，请返回数组中第 k 个最大的元素。

请注意，你需要找的是数组排序后的第 k 个最大的元素，而不是第 k 个不同的元素。

你必须设计并实现时间复杂度为 O(n) 的算法解决此问题。

## 拓展最小堆
```
priority_queue<int, vector<int>, greater<int>> minHeap;
```
**priority_queue 是什么？**

priority_queue 是 C++ 标准模板库（STL）中的优先队列，它是一种特殊的队列，不是先进先出，**而是优先级高的先出**。


比较函数的作用：

less<int>：数字大的优先级高 → 大顶堆

greater<int>：数字小的优先级高 → 小顶堆

```
大顶堆 (less<int>):
     10
    /  \
   8    5
  / \  /
 3  7 1

小顶堆 (greater<int>):
     1
    / \
   3   5
  / \  \
 7  8  10
```

代码
```
        // 2. 遍历数组
        for (int num : nums) {
            // 3. 如果堆还没满（元素个数 < k）
            if (minHeap.size() < k) {
                minHeap.push(num);  // 直接放入
            } 
            // 4. 如果堆已满，且当前数字比堆顶大
            else if (num > minHeap.top()) {
                minHeap.pop();       // 移除堆顶（第k大的候选）
                minHeap.push(num);   // 加入新的更大的数
            }
        }
        
        // 5. 堆顶就是第k大的元素
        return minHeap.top();
    }
};
```

## 为什么可以得到第k大？

<img width="1045" height="546" alt="image" src="https://github.com/user-attachments/assets/cef9a652-b7fc-4226-80c3-26dc75c30fbd" />


## 模拟执行

以 nums = [3,2,1,5,6,4], k = 2 为例：

|步骤|	当前数字|	堆状态（最小堆）|	说明|
|---|---|---|---|
|1	|3	[3]	|堆未满，直接加入|
|2	|2	[2,3]	|堆未满，直接加入（堆顶2）|
|3	|1	[2,3]|1 < 堆顶2，不操作|
|4	|5	[3,5]	|5 > 堆顶2，弹出2，加入5|
|5	|6	[5,6]	|6 > 堆顶3，弹出3，加入6|
|6	|4	[5,6]	|4 < 堆顶5，不操作|

最终堆 = [5,6]，堆顶 = 5，确实是第2大的元素 
